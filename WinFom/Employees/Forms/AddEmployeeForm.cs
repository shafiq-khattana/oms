using Khattana.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFom.Admin.Database;
using Model.Deal.Model;
using Model.ReadyStuff.Model;
using WinFom.Common.Forms;
using Model.Admin.Model;
using WinFom.Common.Model;
using Khattana.BioMetric.SecuGenFingerPrintNonAuto;
using System.Configuration;
using Model.Employees.Model;
using Model.Financials.Model;
using Model.Retail.Model;
using System.Diagnostics;

namespace WinFom.Employees.Forms
{
    public partial class AddEmployeeForm : Form
    {
        WebCam webcam = null;
        public  Employee employee = null;
        private byte[] data1 = null;
        private byte[] data2 = null;

        private int tn1 = 0;
        private int tn2 = 0;
        private string password = "";
        private EmployeeType empType = EmployeeType.FactoryWorker;
        public int EmployeeId = 0;
        public AddEmployeeForm(EmployeeType empoyeeType)
        {
            InitializeComponent();
            empType = empoyeeType;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                password = ConfigurationManager.AppSettings["biopass"];
                bsPics.Value = pPics.Enabled = pThumbs.Enabled = bsThumbs.Value = false;
                Gujjar.TB4(pMain);
                webcam = new WebCam();
                webcam.InitializeWebCam(ref pictureBox1);
                bsPics_Click(null, null);
                bsThumbs_Click(null, null);
                Gujjar.NumbersOnly(tbSalary);
                bswCustomer.Value = true;
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnCamStart_Click(object sender, EventArgs e)
        {
            webcam.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webcam.Stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = pictureBox1.Image;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dial = new OpenFileDialog();
                dial.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                if (dial.ShowDialog() == DialogResult.OK)
                {
                    pictureBox2.Image = Image.FromFile(dial.FileName);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btncap1_Click(object sender, EventArgs e)
        {
            try
            {
                BOps bops = new BOps(password);
                data1 = bops.CaptureImage(thumbp1, thumb1, ref tn1, password);
                if (data1 != null)
                {
                    Gujjar.InfoMsg(string.Format("Finger print image is captured with quality {0}", tn1));
                   
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btncap2_Click(object sender, EventArgs e)
        {
            try
            {
                BOps bops = new BOps(password);
                data2 = bops.CaptureImage(thumbp2, thumb2, ref tn2, password);
                if (data2 != null)
                {
                    Gujjar.InfoMsg(string.Format("Finger print image is captured with quality {0}", tn2));
                    
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void bsThumbs_Click(object sender, EventArgs e)
        {
            if(bsThumbs.Value)
            {
                pThumbs.Enabled = true;
                thumb1.Image = null;
                thumb2.Image = null;
            }
            else
            {
                thumb2.Image = thumb1.Image = Properties.Resources.fingerprint_reader;
                pThumbs.Enabled = false;
            }
        }

        private void bsPics_Click(object sender, EventArgs e)
        {
            if(bsPics.Value)
            {
                pictureBox2.Image = null;
                pPics.Enabled = true;
            }
            else
            {
                pPics.Enabled = false;
                pictureBox2.Image = Properties.Resources.userimage;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] thumbData = null;
                byte[] tpicByte = null;
                byte[] selectorPic = null;
                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all textfields");
                }
                if(bsThumbs.Value)
                {
                    if(thumb1.Image == null || thumb2.Image == null)
                    {
                        throw new Exception("Please provide thumb impression through bio metric device");
                    }
                    BOps bops = new BOps(password);
                    int sn = 0;
                    if (!bops.IsMatched(data1, data2, ref sn, password))
                    {
                        throw new Exception("Your thumb impression doesn't match.");
                    }
                    thumbData = data1;
                    Image tpic = thumb1.Image;
                    if (tn2 > tn1)
                    {
                        thumbData = data2;
                        tpic = thumb2.Image;
                    }
                    tpicByte = Gujjar.GetByteArrayFromImage(tpic);
                }
                if(bsPics.Value)
                {
                    if(pictureBox2.Image == null)
                    {
                        throw new Exception("Please provide selector picture");
                    }
                    selectorPic = Gujjar.GetByteArrayFromImage(pictureBox2.Image);
                }

                Employee employee2 = new Employee
                {
                    Address = tbAddress.Text,
                    CNIC = tbCNIC.Text,
                    Contact = tbContact.Text,
                    DateAdded = dtp.Value.Date,
                    Extra = tbMisc.Text,
                    IsActive = true,
                    DealSchedules = null,
                    Id = 0,
                    IsAvailable = true,
                    Name = tbCompany.Text,
                    PicData = selectorPic,
                    Remarks = tbRemarks.Text,
                    ThumbData = thumbData,
                    ThumbPicData = tpicByte,
                    Account = null,
                    AttendaceRecords = null,
                    CredityEntries = null,
                    Salary = tbSalary.Text.ToDecimal(),
                    EmployeeType = empType,
                    Designation = tbDesignation.Text,
                    DateEnded = DateTime.Now
                };
                Customer customer = new Customer
                {
                    Address = tbAddress.Text,
                    ApplyCardDiscount = false,
                    CardEndDate = DateTime.Now,
                    DateAdded = DateTime.Now,
                    CardNo = "N/A",
                    CardStartDate = DateTime.Now,
                    CNIC = tbCNIC.Text,
                    Contact = tbContact.Text,
                    IsActive = true,
                    CustomerCategory = null,
                    CustomerCategoryId = 1,
                    Id = 0,
                    DiscountPercentage = 0,
                    Name = string.Format("{0} (Employee)", tbCompany.Text),
                    Points = 0,
                    Remarks = tbRemarks.Text,
                    SaleOrders = null,
                    IsEmployee = true
                };
                using (Context db = new Context())
                {
                    

                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var obj = db.Employees.FirstOrDefault(a => a.CNIC.Equals(employee2.CNIC));
                            if (obj != null)
                            {
                                throw new Exception("Employee already added in database");
                            }

                            DialogResult res = Gujjar.ConfirmYesNo("Please confirm..!! All information is correct?");
                            if (res == DialogResult.No)
                                return;

                            string empTitle = "";
                            if(empType == EmployeeType.FactoryWorker)
                            {
                                empTitle = string.Format("{0} (Employee) account", tbCompany.Text);
                            }
                            else
                            {
                                empTitle = string.Format("{0} (Selector) account", tbCompany.Text);
                            }
                            GeneralAccount employeeAccount = new GeneralAccount
                            {
                                Id = Guid.NewGuid().ToString(),
                                AccountNature = AccountNature.Credit,
                                AccountNo = "N/A",
                                AccountTransactions = null,
                                Address = "N/A",
                                SubHeadAccount = null,
                                Balance = 0,
                                BankName = "N/A",
                                Brokers = null,
                                SubHeadAccountId = Properties.Resources.Employees,
                                ExplicitilyCreated = false,
                                Description = "Employee account",
                                Title = empTitle

                            };
                            employeeAccount = db.Accounts.Add(employeeAccount) as GeneralAccount;
                            db.SaveChanges();

                            employee2.GeneralAccountId = employeeAccount.Id;

                            employee = db.Employees.Add(employee2);
                            db.SaveChanges();

                            if(bswCustomer.Value)
                            {
                                Debug.WriteLine("true");
                                customer.GeneralAccountId = employeeAccount.Id;
                                customer.EmpId = employee.Id;

                                db.Customers.Add(customer);
                                SingleTon.LoginForm.AppCustomers.Add(customer);
                            }
                            
                            db.SaveChanges();
                            trans.Commit();

                            Gujjar.InfoMsg("Employee is added in database successfully");
                            EmployeeId = employee.Id;
                            Close();
                        }
                        catch (Exception exp2)
                        {
                            trans.Rollback();
                            throw exp2;
                        }
                    }

                       
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
