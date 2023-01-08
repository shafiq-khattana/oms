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
using Model.Repair.Model;
using WinFom.RepairUI.Reports.Model;
using WinFom.RepairUI.Reports.XtraReport;
using Zen.Barcode;
using DevExpress.XtraReports.UI;

namespace WinFom.RepairUI.Forms
{
    public partial class AddRepairEntryForm : Form
    {
        AppSettings appSett = Helper.AppSet;
        AppUser appUser = SingleTon.LoginForm.appUser;
        RepInfoObj repObj = null;
        public bool IsDone { get; set; }
        public AddRepairEntryForm()
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
                Gujjar.TB4(pMain);
                Gujjar.NumbersOnly(tbDispatchingQty);
                Gujjar.NumbersOnly(tbWeight);
                tbRemarks.Text = "N/A";
                Gujjar.TBOptional(tbRemarks);

                LoadFormData();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void LoadFormData()
        {
            try
            {
                WaitForm wait1 = new WaitForm(LoadCbCategory);
                wait1.ShowDialog();

                BindCbCategory();

                WaitForm wait2 = new WaitForm(LoadPlaces);
                wait2.ShowDialog();

                BindCbPlaces();
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

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            try
            {
                AddRepCategoryForm form = new AddRepCategoryForm();
                form.ShowDialog();

                int id = form.CategoryId;
                if(id != 0)
                {
                    LoadCbCategory();
                    BindCbCategory();

                    cbCategory.SelectedItem = cbCategory.Items.OfType<ItemCategory>()
                        .FirstOrDefault(a => a.Id == id);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void BindCbCategory()
        {
            cbCategory.DataSource = repCats;
            cbCategory.DisplayMember = "Title";
            cbCategory.ValueMember = "Id";
        }

        private List<ItemCategory> repCats = null;
        private void LoadCbCategory()
        {
            try
            {
                using (Context db = new Context())
                {
                    if(repCats != null)
                    {
                        repCats.Clear();
                        repCats = null;
                    }
                    //repCats = db.RepCategories.OrderBy(a => a.Title).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private List<RepItem> repItems = null;

        private void LoadItems(int catId)
        {
            try
            {
                using (Context db = new Context())
                {
                    if(repItems != null)
                    {
                        repItems.Clear();
                        repItems = null;
                    }
                    //repItems = db.RepItems.Where(a => a.RepCategoryId == catId).OrderBy(a => a.Title).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void BindCbItems()
        {
            cbItems.DataSource = repItems;
            cbItems.DisplayMember = "Title";
            cbItems.ValueMember = "Id";
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCategory.SelectedIndex == -1)
                return;
            try
            {
                ItemCategory cat = cbCategory.SelectedItem as ItemCategory;
                LoadItems(cat.Id);
                BindCbItems();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                ItemCategory repCat = null;
                if(cbCategory.SelectedIndex == -1)
                {
                    throw new Exception("Please select/add category first");
                }
                repCat = cbCategory.SelectedItem as ItemCategory;
                AddRepItemForm form = new AddRepItemForm();
                form.ShowDialog();

                int id = form.ItemId;
                if(id != 0)
                {
                    LoadItems(repCat.Id);
                    BindCbItems();

                    cbItems.SelectedItem = cbItems.Items.OfType<RepItem>()
                        .FirstOrDefault(a => a.Id == id);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                AddRepPlaceForm form = new AddRepPlaceForm();
                form.ShowDialog();

                int id = form.PlaceId;
                if(id != 0)
                {
                    WaitForm wait = new WaitForm(LoadPlaces);
                    wait.ShowDialog();

                    BindCbPlaces();

                    cbRepairPlace.SelectedItem = cbRepairPlace.Items.OfType<RepPlace>()
                        .FirstOrDefault(a => a.Id == id);
                }
            }
            catch (Exception exp)
            {

                Gujjar.ErrMsg(exp);
            }
        }

        private List<RepPlace> repPlaces = null;
        private void BindCbPlaces()
        {
            cbRepairPlace.DataSource = repPlaces;
            cbRepairPlace.DisplayMember = "Name";
            cbRepairPlace.ValueMember = "Id";
        }

        private void LoadPlaces()
        {
            try
            {
                if(repPlaces != null)
                {
                    repPlaces.Clear();
                    repPlaces = null;
                }
                using (Context db = new Context())
                {
                    repPlaces = db.RepPlaces.OrderBy(a => a.Name).ToList();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void cbItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbItems.SelectedIndex == -1)
                return;

            RepItem repItem = cbItems.SelectedItem as RepItem;
            //tbLocation.Text = repItem.Location;
            tbWeight.Text = repItem.Weight.ToString("n1");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }
                if(cbCategory.SelectedIndex == -1)
                {
                    throw new Exception("Please choose category");
                }
                if(cbItems.SelectedIndex == -1)
                {
                    throw new Exception("Please choose item");
                }
                if(cbRepairPlace.SelectedIndex == -1)
                {
                    throw new Exception("Please choose repairing place");
                }

                if(!Helper.ConfirmUserPassword())
                {
                    return;
                }
                DialogResult res = Gujjar.ConfirmYesNo("Are you confirm..!!");
                if (res == DialogResult.No)
                    return;

                ItemCategory repCat = cbCategory.SelectedItem as ItemCategory;
                RepItem repItem = cbItems.SelectedItem as RepItem;
                RepPlace repPlace = cbRepairPlace.SelectedItem as RepPlace;

                //RepEntry repEntry = new RepEntry
                //{
                //    Id = 0,
                //    RepPlace = null,
                //    RepItem = null,
                //    DispatchDate = dtp.Value,
                //    DispatchingQty = tbDispatchingQty.Text.ToDecimal(),
                //    DispatchingRemarks = tbRemarks.Text,
                //    ReceivingDate = null,
                //    ReceivingQty = 0,
                //    ReceivingRemarks = "N/A",
                //    RepairCost = 0,
                //    RepItemId = repItem.Id,
                //    RepPlaceId = repPlace.Id,
                //    Status = ReceiveStatus.Dispatched,
                //    TakingOutPerson = tbTakingPerson.Text,
                //    Weight = tbWeight.Text.ToDecimal(),
                //    Unum = Helper.Unum
                //};

                

                using (Context db = new Context())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            //repEntry = db.RepEntries.Add(repEntry);
                            //db.SaveChanges();

                            //repObj = new RepInfoObj
                            //{
                            //    Barcode = null,
                            //    Dated = string.Format("Dated: {0}", dtp.Value.ToString()),
                            //    Operator = appUser.Name,
                            //    QrCode = null,
                            //    Qty = string.Format("Qty: {0}", repEntry.DispatchingQty.ToString("n1")),
                            //    RepItem = string.Format("{0} ({1})", repItem.Title, repItem.Location),
                            //    RepPlace = string.Format("Place: {0}", repPlace.Name),
                            //    ToPerson = string.Format("T.O.Person: {0}", tbTakingPerson.Text),
                            //    Weight = string.Format("Weigth: {0}-Kg", tbWeight.Text),
                            //    WorkingPlace = string.Format("Place: {0}", repItem.Location),
                            //    EntryId = repEntry.Id
                            //};

                            //Code128BarcodeDraw bcode = BarcodeDrawFactory.Code128WithChecksum;
                            //Image img = bcode.Draw(repEntry.Unum, 50);
                            //byte[] imgBytes = Gujjar.GetByteArrayFromImage(img);
                            //repObj.Barcode = imgBytes;

                            //CodeQrBarcodeDraw qrCode = BarcodeDrawFactory.CodeQr;
                            //Image img2 = qrCode.Draw("GBS Solutions, Burewala. 0323-9372084", 50);
                            //byte[] qrBytes = Gujjar.GetByteArrayFromImage(img2);
                            //repObj.QrCode = qrBytes;

                            //for (int i = 0; i < (int)numCopies.Value; i++)
                            //{
                            //    PrintReceipt();
                            //}
                            
                            //Gujjar.InfoMsg("Repairing entry is added in record and saved in database");
                            //IsDone = true;
                            //trans.Commit();
                            //Close();
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

        private void PrintReceipt()
        {
            try
            {
                CompanyRVM compRVM = new CompanyRVM
                {
                    Address = appSett.Address,
                    Logo = appSett.Logo,
                    Name = appSett.Name,
                    Phone = appSett.PhoneNo
                };

                List<CompanyRVM> compList = new List<CompanyRVM> { compRVM };
                List<RepInfoObj> objList = new List<RepInfoObj> { repObj };

                RepairItemReport rep = new RepairItemReport();

                DetailReportBand band1 = rep.Bands["DetailReport"] as DetailReportBand;
                DetailReportBand band2 = rep.Bands["DetailReport1"] as DetailReportBand;

                band1.DataSource = compList;
                band2.DataSource = objList;

                rep.ShowPrintMarginsWarning = false;
                rep.ShowPrintStatusDialog = false;

                var lengendsParam = rep.Parameters["Legends"];
                lengendsParam.Visible = false;
                lengendsParam.Value = Helper.Legends();

                rep.Print(appSett.ThermalPrinter);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        private void tbDispatchingQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal qty = 0;
                if(!string.IsNullOrEmpty(tbDispatchingQty.Text))
                {
                    qty = tbDispatchingQty.Text.ToDecimal();
                }
                decimal weight = 0;
                if(cbItems.SelectedIndex != -1)
                {
                    weight = (cbItems.SelectedItem as RepItem).Weight;
                }
                decimal totalW = qty * weight;
                tbWeight.Text = totalW.ToString("n1");
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
