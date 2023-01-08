using Khattana.Common;
using Khattana.Display;
using Khattana.Model;
using Khattana.Secrecy;
using Model.Admin.Model;
using Model.Deal.Model;
using Model.OilDirtStuff.Model;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using WebCam_Capture;
using WinFom.Admin.Database;
using WinFom.Admin.Forms;
using WinFom.Common.Forms;
using System.Collections.Generic;
using Model.Financials.Model;
using WinFom.Common.Reports.Model;
using WinFom.Common.Reports.XtraReports;
using DevExpress.XtraReports.UI;
using Model.Deal.Common;
using Model.ReadyStuff.Model;
using System.Configuration;

namespace WinFom.Common.Model
{
    public class Helper
    {
        private static Random rnd = new Random();
        public static string Unum
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(rnd.Next(1000, 9999).ToString().PadLeft(4, '0'));
                sb.Append(rnd.Next(99999).ToString().PadLeft(5, '0'));
                sb.Append(rnd.Next(9999).ToString().PadLeft(4, '0'));
                sb.Append(rnd.Next(9999).ToString().PadLeft(4, '0'));

                return sb.ToString();
            }
        }

        public static bool ReverseTransaction(DayBook daybook)
        {
            try
            {
                DateTime transDate = DateTime.Now;
                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            GeneralAccount debitAccount = null;
                            GeneralAccount creditAccount = null;
                            decimal amount = daybook.Amount;
                            decimal debitAmount = amount, creditAmount = amount;

                            if (!string.IsNullOrEmpty(daybook.CreditAccountId))
                            {
                                debitAccount = db.Accounts.Find(daybook.CreditAccountId) as GeneralAccount;
                                if(debitAccount.AccountNature != AccountNature.Debit)
                                {
                                    debitAmount = -debitAmount;
                                }
                            }
                            if (!string.IsNullOrEmpty(daybook.DebitAccountId))
                            {
                                creditAccount = db.Accounts.Find(daybook.DebitAccountId) as GeneralAccount;
                                if(creditAccount.AccountNature != AccountNature.Credit)
                                {
                                    creditAmount = -creditAmount;
                                }
                            }

                            
                           

                            string msg = string.Format("Reverse financial transaction amount ({0}). Previous daybook Id ({1}), previous description ({2})",
                                daybook.Amount.ToString("n2"), daybook.Id, daybook.Description);

                            DayBook daybook2 = new DayBook
                            {
                                Id = 0,
                                Amount = amount,
                                CanRollBack = false,      
                                Date = transDate,
                                Description = msg,
                                InDate = DateTime.Now.Date                 
                            };
                            if(debitAccount != null)
                            {
                                daybook2.DebitAccountId = debitAccount.Id;
                            }
                            if(creditAccount != null)
                            {
                                daybook2.CreditAccountId = creditAccount.Id;
                            }

                            daybook2 = db.DayBooks.Add(daybook2);
                            db.SaveChanges();

                            #region "Financials"



                            #region "Debit transaction"
                            AccountTransaction debitItemTrans = null;
                            if (debitAccount != null)
                            {
                                debitItemTrans = new AccountTransaction
                                {
                                    Id = 0,
                                    Account = null,
                                    AccountTransactionType = AccountTransactionType.Debit,
                                    Balance = debitAmount,
                                    CreditAmount = 0,
                                    Date = transDate,
                                    DayBookId = daybook2.Id,
                                    DebitAmount = amount,
                                    GeneralAccountId = debitAccount.Id,
                                    Description = msg
                                };

                                var debitDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == debitAccount.Id).AsParallel()
                                    .ToList().Where(a => a.Date.Date >= AppSet.StartDate.Date && a.Date.Date <= AppSet.EndDate.Date)
                                    .OrderByDescending(a => a.Id).FirstOrDefault();

                                if (debitDbEntry != null)
                                {
                                    debitItemTrans.Balance += debitDbEntry.Balance;
                                }

                                debitItemTrans = db.AccountTransactions.Add(debitItemTrans);
                                db.SaveChanges();

                            }

                            #endregion

                            #region  "Credit transaction"
                            AccountTransaction creditItemTrans = null;
                            if (creditAccount != null)
                            {
                                creditItemTrans = new AccountTransaction
                                {
                                    Id = 0,
                                    Account = null,
                                    AccountTransactionType = AccountTransactionType.Credit,
                                    Balance = creditAmount,
                                    CreditAmount = amount,
                                    Date = transDate,
                                    DayBookId = daybook2.Id,
                                    DebitAmount = 0,
                                    GeneralAccountId = creditAccount.Id,
                                    Description = msg
                                };

                                var creditDbEntry = db.AccountTransactions.Where(a => a.GeneralAccountId == creditAccount.Id).AsParallel()
                                    .ToList().Where(a => a.Date.Date >= AppSet.StartDate.Date && a.Date.Date <= AppSet.EndDate.Date)
                                    .OrderByDescending(a => a.Id).FirstOrDefault();

                                if (creditDbEntry != null)
                                {
                                    creditItemTrans.Balance += creditDbEntry.Balance;
                                }

                                creditItemTrans = db.AccountTransactions.Add(creditItemTrans);
                                db.SaveChanges();
                            }
                            
                            #endregion

                            var daybookdb = db.DayBooks.Find(daybook2.Id);
                            if (debitItemTrans != null)
                            {
                                daybookdb.DebitTrace = string.Format("({0}). Trans Id: {1}", debitAccount.Title, debitItemTrans.Id);
                            }
                            else
                            {
                                daybookdb.DebitTrace = "N/A";
                            }

                            if(creditItemTrans != null)
                            {
                                daybookdb.CreditTrace = string.Format("({0}). Trans Id: {1}", creditAccount.Title, creditItemTrans.Id);
                            }
                            else
                            {
                                daybookdb.CreditTrace = "N/A";
                            }

                            

                            db.Entry(daybookdb).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            #endregion


                            var db2 = db.DayBooks.Find(daybook.Id);
                            db2.IsReversed = true;
                            db.Entry(db2).State = EntityState.Modified;
                            db.SaveChanges();

                            trans.Commit();
                            if(AppSet.PrintFinancialTransactions)
                            {
                                PrintTransactions(new List<DayBook> { daybookdb });
                            }
                        }
                        catch (Exception exp2)
                        {
                            trans.Rollback();
                            throw exp2;
                        }
                    }
                        

                    return true;
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
                return false;
            }
        }
        public static void PrintTransactions(List<DayBook> daybooks)
        {
            try
            {
                List<DaybookVM> daybookVM = new List<DaybookVM>();
                foreach (var item in daybooks)
                {
                    DaybookVM vm = new DaybookVM
                    {
                        Amount = item.Amount,
                        CreditTrace = item.CreditTrace,
                        Date = item.Date.ToString(),
                        DebitTrace = item.DebitTrace,
                        Description = item.Description,
                        Id = item.Id
                    };
                    daybookVM.Add(vm);
                }

                List<FCompRVM> fcompRVM = new List<FCompRVM> { SingleTon.LoginForm.FCompRVM };
                FTransReport rep = new FTransReport();

                DetailReportBand band1 = rep.Bands["DetailReport"] as DetailReportBand;
                DetailReportBand band2 = rep.Bands["DetailReport1"] as DetailReportBand;
                band1.DataSource = fcompRVM;
                band2.DataSource = daybookVM;

                rep.ShowPrintStatusDialog = false;
                rep.ShowPrintMarginsWarning = false;
                rep.Print(SingleTon.LoginForm.AppSett.ThermalPrinter);
            }
            catch (Exception)
            {


            }
        }
        public static string EAN13
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(rnd.Next(1000, 9999).ToString().PadLeft(4, '0'));
                sb.Append(rnd.Next(9999).ToString().PadLeft(4, '0'));
                sb.Append(rnd.Next(9999).ToString().PadLeft(4, '0'));

                return sb.ToString();
            }
        }
        public static void UpdateDealStatus(int schId)
        {
            try
            {
                using (Context db = new Context())
                {
                    var schedule = db.DealSchedules.Find(schId);
                    var deal = db.AppDeals.Find(schedule.AppDealId);
                    deal.DealSchedules = db.DealSchedules.Where(a => a.AppDealId == deal.Id).ToList();

                    int totalSchedules = deal.DealSchedules.Count;
                    int completed = deal.DealSchedules.Count(a => a.IsArrived);
                    int cancelled = deal.DealSchedules.Count(a => a.Status == ScheduleStatus.Cancelled);
                    if (completed == 0)
                    {
                        deal.DealStatus = AppDealStatus.Scheduled;
                    }
                    else if (completed == totalSchedules)
                    {
                        deal.DealStatus = AppDealStatus.Completed;
                    }
                    
                    else
                    {
                        deal.DealStatus = AppDealStatus.Partial;
                    }
                    if (cancelled == totalSchedules)
                    {
                        deal.DealStatus = AppDealStatus.Cancelled;
                    }
                    db.Entry(deal).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception exp)
            {

                throw exp;
            }
        }
        public static void UpdateReadyDealStatus(int id)
        {
            try
            {
                using (Context db = new Context())
                {
                    var schedule = db.ReadySchedules.Find(id);
                    var deal = db.ReadyDeals.Find(schedule.ReadyDealId);
                    deal.ReadySchedules = db.ReadySchedules.Where(a => a.ReadyDealId == deal.Id).ToList();

                    int totalSchedules = deal.ReadySchedules.Count;
                    int completed = deal.ReadySchedules.Count(a => a.IsCompleted);
                    int cancelled = deal.ReadySchedules.Count(a => a.ScheduleType == ReadyScheduleType.Cancelled);

                    if (completed == 0)
                    {
                        deal.DealStatus = AppDealStatus.Scheduled;
                    }
                    else if (completed == totalSchedules)
                    {
                        deal.DealStatus = AppDealStatus.Completed;
                    }
                    else
                    {
                        deal.DealStatus = AppDealStatus.Partial;
                    }
                    if(cancelled == totalSchedules)
                    {
                        deal.DealStatus = AppDealStatus.Cancelled;
                    }
                    db.Entry(deal).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception exp)
            {

                throw exp;
            }
        }
        public static bool ConfirmAdminPassword()
        {
            try
            {
                AppSettings AppSett = null;
                using (Context db = new Context())
                {
                    AppSett = db.AppSettings.First();
                }
                if (!SingleTon.LoginForm.RememberMeAdmin)
                {
                    string adminPass = AppSett.MasterPassword;

                    SecurityConfirmForm form = new SecurityConfirmForm(Khattana.Model.PasswordType.Admin, adminPass);
                    form.ShowDialog();

                    SecurityConfirmResponse response = form.Response;
                    if (response.IsValid == null)
                    {
                        return false;
                    }
                    if (!response.IsValid.Value)
                    {
                        throw new Exception("Invalid password");
                    }

                    SingleTon.LoginForm.RememberMeAdmin = response.RememberMe;
                    return true;
                }
                return true;
            }
            catch (Exception exp)
            {

                throw exp;
            }

        }

        public static bool ConfirmUserPassword()
        {
            try
            {
                AppSettings AppSett = null;
                using (Context db = new Context())
                {
                    AppSett = db.AppSettings.First();
                }
                if (!SingleTon.LoginForm.RememberMeUser)
                {
                    string userPass = AppSett.SecurityPassword;

                    SecurityConfirmForm form = new SecurityConfirmForm(PasswordType.User, userPass);
                    form.ShowDialog();

                    SecurityConfirmResponse response = form.Response;
                    if (response.IsValid == null)
                    {
                        return false;
                    }
                    if (!response.IsValid.Value)
                    {
                        throw new Exception("Invalid password");
                    }

                    SingleTon.LoginForm.RememberMeUser = response.RememberMe;
                    return true;
                }
                return true;
            }
            catch (Exception exp)
            {

                throw exp;
            }

        }
        public static void UpdateOilDirtDealStatus(int id)
        {
            try
            {
                using (Context db = new Context())
                {
                    var schedule = db.OilDirtSchedules.Find(id);
                    var deal = db.OilDirtDeals.Find(schedule.OilDirtDealId);
                    deal.Schedules = db.OilDirtSchedules.Where(a => a.OilDirtDealId == deal.Id).ToList();

                    int totalSchedules = deal.Schedules.Count;
                    int completed = deal.Schedules.Count(a => a.Status == OilDirtScheduleStatus.Completed);
                    int cancelled = deal.Schedules.Count(a => a.Status == OilDirtScheduleStatus.Cancelled);
                    if (completed == 0)
                    {
                        deal.Status = OilDirtStatus.Scheduled;
                    }
                    else if (completed == totalSchedules)
                    {
                        deal.Status = OilDirtStatus.Completed;
                    }
                    else
                    {
                        deal.Status = OilDirtStatus.Partial;
                    }
                    if(cancelled == totalSchedules)
                    {
                        deal.Status = OilDirtStatus.Cancelled;
                    }
                    db.Entry(deal).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception exp)
            {

                throw exp;
            }
        }
        public static AppSettings AppSet
        {
            get
            {
                using (Context db = new Context())
                {
                    return db.AppSettings.First();
                }
            }
        }
        public static bool IsDateInRange(DateTime date)
        {
            try
            {
                using (Context db = new Context())
                {
                    var set = db.AppSettings.First();

                    return (set.StartDate >= date.Date && set.EndDate <= date.Date);

                }
            }
            catch (Exception exp)
            {

                throw exp;
            }
        }

        public static string ToXML<T>(T options)
        {
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(options.GetType());
            serializer.Serialize(stringwriter, options);
            return stringwriter.ToString();
        }

        public static T LoadFromXMLString<T>(string xmlText)
        {
            var stringReader = new System.IO.StringReader(xmlText);
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(stringReader);
        }

        public static SecOptMod GetSecOpsObj()
        {
            try
            {
                SecOptMod mod = null;
                string path = @"C:\GBS\gbss.txt";
                string dirName = @"C:\GBS";
                string gbs = @"TMURzXQ0i+cgzx+V5MOS6eIJI0K6IsRiIWpwlTzVz+HzikPy0NRgTxDw/YD0hm6pKzSGvJqoKKIYU8FdFqcZRtXWe/N9s0Sx0oquJx7/CFCYBH59CD26vCwO542NiNNE+DL+9+BMyLDZ89E3ZJP/CCRyH2BhqMYxDmTOunpxeaPVJ2QswHIPNcTbewKOxGbbcyk1zjzMsyh8Ot1V+ncWI04DYHLybECOKxOiXAOpdMPl9HlJHqqaMW4awZab6NrIctebseAtD+HJSwyUf2dA3nmb5qbXHzglbomrjcDAIl1gBwb+31NxM+mR8f47GEb1HOCny3zIrH4f3XTdniCJet/ushAXkUNx1B6F/9nOsHlyEQhE8wj07mXzOwNawArgg72Nqb2rqLEOBDcAT+zrjIdpehI6KbmIGufT5qRledmCo6wbkhvLoaL7z8IcStV3v3/3PZHcMR3Uj9l0RAdjqUcc9sWzX7ViCOgu//TWYXJYemsgXsujOxkK0QQCdvbraAb0wcTJ5dvlRLK+Q+qsfM8nKvTM8tY+j6ErR9LJTjQ6/k0pR//DWlYhztRmNhNF4tkgvFAdjLMPwqCIkHxH5mePy3vXCLt++OFipcsWPli2g7V3LJoVUIuWHdRBpQXYD9GRorFC8wC9xZ6DV6bQqR9xI9gWpCj3NQj/65xr9Ddmz4RKMOQLowjTsUOJPPy/Hfi5SnJ0+AX119NTjSya4pfNqsA7qod3g2XBIfH+FIM+BekVOL7P1d3iiXtGyjbfJ0TR9FiU48q/6JzX6KOc6J17VslAZBwv6iGCrVbxZUr0rMecRvj+fcEUIrgyc6+k53RjkN6WP58/jmSI3/WxhitJ75beLo0l26vtSGiq6RZ8DB1QqzxZ5cI66rpqbYVQKZM/DEWSdZSdIg8TrDDNiFuZfGa6UD/pgHDnq2FkCc0I6Pgw3gi2CKML411HOcPmCyd2jSbTnDLhyVJjnUSZqttjvK7z+On9EfDPFC6in36WR3m6moqXU/UP8+m3aTqAeEy/T4O8Ac8vtN2bcB2Wsl2GGIvUSTA+nyBD/8AqBEM4vDZaoQoFMOpJ7Jrw97XA1CCEVQuq0zjYbnYTLgIDldxHQ0wm6jMk3itEFhzYM8pzFuCWKfhHTYB13KsYavRDDIVlXMJAC7Cy9hzDPCseeAt9ZnMC6xkiHzs/eumk37LeXXvnJTz2DaeyIxhNc/QVs/41WIRn4RGbnTJDBiXmVmd8JJ+V+KoBMWWnYkpas7/OPl5GPN63MB5SxcZ8x4zgcVzuPGKIOSK3hTbxE51vi0FZM90MyJdMM2NIS111eSb62vcH9Wrj0Om9c7fY0+JNWAkLaCnPc6MuZM1uUhdXUkLfnQCPZZbDW6h751Nf0Cb+XGGXnDezWWwXHE3aT18KSyzI7ZlioSzvN2MGjWmKTqfWPDcTRvylidHSuaahodw=";
                if (!Directory.Exists(dirName))
                {
                    Directory.CreateDirectory(dirName);
                }
                if (!File.Exists(path))
                {
                    File.WriteAllText(path, gbs);
                }

                if (!File.Exists(path))
                {
                    throw new Exception(@"C:\GBS\gbss.txt file is not found. Please contact to administrator");
                }

                string data = File.ReadAllText(path);
                data = MsrCipher.Decrypt(data);

                mod = Gujjar.LoadFromXMLString<SecOptMod>(data);
                return mod;
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
                Application.Exit();
                return null;
            }
        }

        private static bool okRes = false;
        private static void IsOk()
        {
            try
            {

                using (Context db = new Context())
                {
                    var obj = db.Anattakh.FirstOrDefault();
                    if (obj != null)
                    {
                        string dateEnd = MsrCipher.Decrypt(obj.ChalBasKerYar);
                        string dateSt = MsrCipher.Decrypt(obj.RabRakha);

                        DateTime dtEnd = Convert.ToDateTime(dateEnd);
                        DateTime lastLog = Convert.ToDateTime(dateSt).AddMinutes(-10);
                        DateTime noDt = DateTime.Now;

                        //if (lastLog > noDt)
                        //{
                        //    throw new Exception(string.Format("Last login time ({0}), now login time ({1}). Please correct time", lastLog, noDt));
                        //}

                        if (dtEnd.Date < noDt)
                        {
                            var dbE = db.Anattakh.First();
                            db.Anattakh.Remove(dbE);
                            db.SaveChanges();
                            throw new Exception("Trial period has been ended");
                        }

                        //var db2 = db.Anattakh.First();
                        //db2.RabRakha = MsrCipher.Encrypt(noDt.ToString());
                        //db.Entry(db2).State = System.Data.Entity.EntityState.Modified;
                        //db.SaveChanges();

                        okRes = true;
                    }
                    else
                    {
                        SecOptMod secMod = Helper.GetSecOpsObj();
                        string mid = Lisence.GetMachineId();
                        var obj4 = db.KeyInfo.Find(mid);

                        if (obj4 == null)
                        {
                            throw new Exception("There is no license information in your system.");
                        }

                        DateTime dtN = DateTime.Now;
                        DateTime lastLog = Convert.ToDateTime(MsrCipher.Decrypt(obj4.WahJi));

                        if (dtN < lastLog.AddMinutes(-10))
                        {
                            throw new Exception(string.Format("Last login time ({0}), now login time ({1}). Please correct time", lastLog, dtN));
                        }

                        string key = MsrCipher.Decrypt(obj4.Key);


                        if (!Lisence.IsValidKey(key, secMod.secretPhrase))
                        {
                            var obj6 = db.KeyInfo.Find(mid);
                            db.KeyInfo.Remove(obj6);
                            db.SaveChanges();

                            Gujjar.ErrMsg("Invalid Lisence Key");
                            DialogResult res = Gujjar.ConfirmYesNo("Do you want to insert lisence key?");
                            if (res == DialogResult.No)
                            {
                                throw new Exception("Thanks you");
                            }
                            KeyForm form = new KeyForm();
                            form.ShowDialog();
                            if (!form.IsDone)
                            {
                                okRes = false;
                                return;
                            }
                            else
                            {
                                okRes = true;
                                return;
                            }
                        }
                        string isValid2 = MsrCipher.Decrypt(obj4.IsValid);
                        if (!isValid2.Equals(secMod.aaho))
                        {
                            throw new Exception("Error: 1185. Please contact to administrator");
                        }

                        var dbObj = db.KeyInfo.Find(mid);
                        dbObj.WahJi = MsrCipher.Encrypt(dtN.ToString());

                        db.Entry(dbObj).State = EntityState.Modified;
                        db.SaveChanges();
                        okRes = true;
                    }
                }
            }
            catch (Exception exp)
            {
                okRes = false;
                Gujjar.ErrMsg(exp);

            }
        }
        public static void IsOkApplied()
        {
            try
            {
                WaitForm wait = new WaitForm(IsOk);
                wait.ShowDialog();

                if (!okRes)
                {
                    throw new Exception("Error code 406. Contact to administrator");
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
                Application.Exit();
            }
        }

        public static UserOps GetUserOps()
        {
            return SingleTon.LoginForm.UserOptions;
        }
        public static string Legends()
        {
            return ConfigurationManager.AppSettings["Pwd2"];
        }
    }

    class WebCam
    {
        private WebCamCapture webcam;
        private System.Windows.Forms.PictureBox _FrameImage;
        private int FrameNumber = 30;
        public void InitializeWebCam(ref System.Windows.Forms.PictureBox ImageControl)
        {
            webcam = new WebCamCapture();
            webcam.FrameNumber = ((ulong)(0ul));
            webcam.TimeToCapture_milliseconds = FrameNumber;
            webcam.ImageCaptured += new WebCamCapture.WebCamEventHandler(webcam_ImageCaptured);
            _FrameImage = ImageControl;
        }

        void webcam_ImageCaptured(object source, WebcamEventArgs e)
        {
            _FrameImage.Image = e.WebCamImage;
        }

        public void Start()
        {
            webcam.TimeToCapture_milliseconds = FrameNumber;
            webcam.Start(0);
        }

        public void Stop()
        {
            webcam.Stop();
        }

        public void Continue()
        {
            // change the capture time frame
            webcam.TimeToCapture_milliseconds = FrameNumber;

            // resume the video capture from the stop
            webcam.Start(this.webcam.FrameNumber);
        }

        public void ResolutionSetting()
        {
            webcam.Config();
        }

        public void AdvanceSetting()
        {
            webcam.Config2();
        }

    }
}
