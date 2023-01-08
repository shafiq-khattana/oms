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
using Model.General.Model;
using WinFom.GeneralUI.Reports.Model;
using WinFom.GeneralUI.Reports.XtraReports;
using Zen.Barcode;
using DevExpress.XtraReports.UI;
using System.Threading;

namespace WinFom.GeneralUI.Forms
{
    public partial class AddGeneralReceiptForm : Form
    {
        private AppSettings AppSett = Helper.AppSet;
        public bool IsDone { get; set; }
        AppUser appUser = SingleTon.LoginForm.appUser;
        public AddGeneralReceiptForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                numUpDown.Enabled = true;
                bswPrinter.Value = true;
                Gujjar.TB4(pMain);
                dtp.MinDate = AppSett.StartDate;
                dtp.MaxDate = AppSett.EndDate;
                
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bswPrinter_Click(object sender, EventArgs e)
        {
            numUpDown.Enabled = bswPrinter.Value;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please add some description");
                }

                DialogResult res = Gujjar.ConfirmYesNo("Are you sured please..!!");
                if (res == DialogResult.No)
                    return;

                using (Context db = new Context())
                {
                    GeneralReceipt receipt = new GeneralReceipt
                    {
                        Amount = 0,
                        Dated = dtp.Value,
                        Description = tbDescription.Text,
                        Id = 0,
                        Unum = Helper.Unum
                    };
                    db.GeneralReceipts.Add(receipt);
                    db.SaveChanges();
                    if(bswPrinter.Value)
                    {
                        for (int i = 0; i < numUpDown.Value; i++)
                        {
                            PrintReceipt(receipt);
                        }
                    }
                    IsDone = true;
                    Gujjar.InfoMsg("Receipt is added in system");
                    Close();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        List<GeneralReceiptVM> generalReceiptVMList = null;
        List<GRCompRVM> grcomprvmList = null;
        private void PrintReceipt(GeneralReceipt receipt)
        {
            try
            {
                if(generalReceiptVMList != null )
                {
                    generalReceiptVMList.Clear();
                    generalReceiptVMList = null;
                }
                if(grcomprvmList != null)
                {
                    grcomprvmList.Clear();
                    grcomprvmList = null;
                }
                generalReceiptVMList = new List<GeneralReceiptVM>();
                grcomprvmList = new List<GRCompRVM>();

                GRCompRVM grvm = new GRCompRVM
                {
                    Address = AppSett.Address,
                    Barcode = null,
                    Logo = AppSett.Logo,
                    Name = AppSett.Name,
                    Phone = AppSett.PhoneNo,
                    Qrcode = null,
                    RepDate = dtp.Value.ToString(),
                    RepTitle = "General Receipt"
                };
                Code128BarcodeDraw draw = BarcodeDrawFactory.Code128WithChecksum;
                var image1 = draw.Draw(receipt.Unum, 50);
                grvm.Barcode = Gujjar.GetByteArrayFromImage(image1);

                CodeQrBarcodeDraw draw2 = BarcodeDrawFactory.CodeQr;
                var image2 = draw2.Draw("GBS Solutions, Burewala. 0333-9372084", 50);
                grvm.Qrcode = Gujjar.GetByteArrayFromImage(image2);
                grcomprvmList.Add(grvm);

                GeneralReceiptVM gvm = new GeneralReceiptVM
                {
                    Dated = receipt.Dated.ToString(),
                    Description = receipt.Description,
                    Id = receipt.Id
                };
                generalReceiptVMList.Add(gvm);

                GRReport rep = new GRReport();

                DetailReportBand band1 = rep.Bands["DetailReport"] as DetailReportBand;
                DetailReportBand band2 = rep.Bands["DetailReport1"] as DetailReportBand;

                band1.DataSource = grcomprvmList;
                band2.DataSource = generalReceiptVMList;

                var op = rep.Parameters["Op"];
                op.Visible = false;
                op.Value = appUser.Id;

                var lengendsParam = rep.Parameters["Legends"];
                lengendsParam.Visible = false;
                lengendsParam.Value = Helper.Legends();

                rep.ShowPrintMarginsWarning = false;
                rep.ShowPrintStatusDialog = false;

                
                
                rep.Print(AppSett.ThermalPrinter);
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void bunifuSwitch1_Click(object sender, EventArgs e)
        {
            
                
        }

        private void tbDescription_Leave(object sender, EventArgs e)
        {
            //InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
            //tbDescription.RightToLeft = System.Windows.Forms.RightToLeft.No;
        }
    }
}
