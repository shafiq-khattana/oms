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
using WinFom.Common.Forms;
using Model.AppGoodCompany.ViewModel;
using Model.Admin.Model;
using WinFom.Common.Model;

namespace WinFom.AppGoodCompany.Forms
{
    
    public partial class PackingIssueReceiveRecordForm : Form
    {
        private List<TGoodCompVM> companiesVm = new List<TGoodCompVM>();
        private List<TPackingVM> packingsVm = new List<TPackingVM>();
        private List<TTypeVM> ttypeVm = new List<TTypeVM>();
        private bool isDated = false;
        private List<PackingIssueReceiveRecord> trans = new List<PackingIssueReceiveRecord>();
        private string btndgvdesc = "btndgvdescription";
        private AppSettings appSett = Helper.AppSet;

        public PackingIssueReceiveRecordForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void LoadVmData()
        {
            try
            {
                using (Context db = new Context())
                {
                    var comps = db.GoodCompanies.OrderBy(a => a.Name).ToList();
                    var packs = db.DealPackings.OrderBy(a => a.Name).ToList();

                    foreach (var item in comps)
                    {
                        TGoodCompVM vm = new TGoodCompVM
                        {
                            Id = item.Id,
                            Name = string.Format("{0} ({1})", item.Name, item.Address)
                        };

                        companiesVm.Add(vm);
                    }
                    companiesVm.Add(new TGoodCompVM
                    {
                        Id = -1,
                        Name = "All Good Companies"
                    });


                    foreach (var item in packs)
                    {
                        if (item.Name == "N/A")
                            continue;
                        TPackingVM vm = new TPackingVM
                        {
                            Id = item.Id,
                            Name = item.Name
                        };
                        packingsVm.Add(vm);
                    }
                    packingsVm.Add(new TPackingVM
                    {
                        Id = -1,
                        Name = "All Packings"
                    });


                    var boj2 = Enum.GetNames(typeof(RecordType)).ToList();
                    boj2.Add("Both");
                    int id = 0;
                    foreach (var item in boj2)
                    {
                        ttypeVm.Add(new TTypeVM
                        {
                            Id = id++,
                            Name = item
                        });
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void BindVmData()
        {
            cbCompanies.DataSource = companiesVm;
            cbPackings.DataSource = packingsVm;
            cbTypes.DataSource = ttypeVm;
            cbCompanies.DisplayMember = cbPackings.DisplayMember = cbTypes.DisplayMember = "Name";
            cbCompanies.ValueMember = cbPackings.ValueMember = cbTypes.ValueMember = "Id";
        }
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                Gujjar.AddDatagridviewButton(dgv, btndgvdesc, "Desc", "Desc", 70);
                WaitForm wait = new WaitForm(LoadVmData);
                wait.ShowDialog();

                BindVmData();

                sbtnDated.Value = false;
                isDated = false;
                dtpFrom.MinDate = dtpTo.MinDate = appSett.StartDate;
                dtpFrom.MaxDate = dtpTo.MaxDate = appSett.EndDate;
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void pMain_Paint(object sender, PaintEventArgs e)
        {

        }

        DateTime dtFrom = DateTime.Now;
        DateTime dtTo = DateTime.Now;
        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                if(cbCompanies.SelectedIndex == -1 || cbPackings.SelectedIndex == -1 || cbTypes.SelectedIndex == -1)
                {
                    throw new Exception("Please select an appropriate option from dropdown list. Thanks");
                }
                dtFrom = dtpFrom.Value.Date;
                dtTo = dtpTo.Value.Date;
                packingIssueReceiveRecordVMBindingSource.List.Clear();
                WaitForm wait2 = new WaitForm(LoadTransactions);
                wait2.ShowDialog();

                TGoodCompVM goodComp = cbCompanies.SelectedItem as TGoodCompVM;
                if(goodComp.Id != -1)
                {
                    trans = trans.Where(a => a.GoodCompanyId == goodComp.Id).ToList();
                }

                TPackingVM packing = cbPackings.SelectedItem as TPackingVM;
                if(packing.Id != -1)
                {
                    trans = trans.Where(a => a.DealPackingId == packing.Id).ToList();
                }

                string tName = cbTypes.Text;

                if(tName != "Both")
                {
                    RecordType type = (RecordType)Enum.Parse(typeof(RecordType), tName);
                    trans = trans.Where(a => a.RecordType == type).ToList();
                }

                foreach (var item in trans)
                {
                    PackingIssueReceiveRecordVM vm = new PackingIssueReceiveRecordVM
                    {
                        Id = item.Id,
                        RecordType = item.RecordType.ToString(),
                        DateTime = item.DateTime,
                        DealPacking = item.DealPacking.Name,
                        DealPackingId = item.DealPackingId,
                        Description = item.Description,
                        GoodCompany = item.GoodCompany.Name,
                        GoodCompanyId = item.GoodCompanyId,
                        Qty = item.Qty,
                        Remarks = item.Remarks
                    };

                    packingIssueReceiveRecordVMBindingSource.List.Add(vm);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadTransactions()
        {
            try
            {
                using (Context db = new Context())
                {
                    trans = db.PackingIssueReceiveRecords.AsParallel().ToList()
                        .Where(a => { DateTime dt = a.DateTime.Date; return dt >= appSett.StartDate && dt <= appSett.EndDate; }).ToList();


                    if (isDated)
                    {
                        trans = trans.Where(a => a.DateTime.Date >= dtFrom && a.DateTime.Date <= dtTo).ToList();
                    }

                    foreach (var item in trans)
                    {
                        item.DealPacking = db.DealPackings.Find(item.DealPackingId);
                        item.GoodCompany = db.GoodCompanies.Find(item.GoodCompanyId);
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void sbtnDated_Click(object sender, EventArgs e)
        {
            isDated = sbtnDated.Value;
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ri = e.RowIndex;
                int ci = e.ColumnIndex;

                if (ri == -1 || ri == dgv.NewRowIndex)
                    return;

                if(dgv.Columns[btndgvdesc].Index == ci)
                {
                    int aid = dgv.Rows[ri].Cells[0].Value.ToInt();

                    var obj = packingIssueReceiveRecordVMBindingSource.List.OfType<PackingIssueReceiveRecordVM>()
                        .FirstOrDefault(a => a.Id == aid);

                    ShowAlarmForm form = new ShowAlarmForm(obj.Remarks, "Description");
                    form.ShowDialog();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }

    public class TGoodCompVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class TPackingVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class TTypeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
