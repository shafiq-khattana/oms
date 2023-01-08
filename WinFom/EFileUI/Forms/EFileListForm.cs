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
using Model.EFiling.Model;
using System.Data.Entity;
using Model.EFiling.ViewModel;
using WinFom.EFileUI.Forms;

namespace WinFom.RepairUI.Forms
{
    public partial class EFileListForm : Form
    {
        private List<EFile> eFiles = null;
        private AppSettings appSett = Helper.AppSet;
        private string btndgvview = "btndag32414";
        private string btndgvadd = "btndgvadd1221";
        public EFileListForm()
        {
            InitializeComponent();
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadEFiles()
        {
            try
            {
                if(eFiles != null)
                {
                    eFiles.Clear();
                    eFiles = null;
                }
                using (Context db = new Context())
                {
                    eFiles = db.EFiles.Include(a => a.Category).AsParallel().ToList()
                        .Where(a => a.DateAdded.Date >= appSett.StartDate.Date && a.DateAdded.Date <= appSett.EndDate.Date).ToList();
                    foreach (var item in eFiles)
                    {
                        item.FileImages = db.EFileImages.Where(a => a.EFileId == item.Id).ToList();
                    }
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void UpdateDgv(List<EFile> files)
        {
            try
            {
                eFileVMBindingSource.List.Clear();
                foreach (var item in files)
                {
                    EFileVM vm = new EFileVM
                    {
                        DateAdded = item.DateAdded,
                        Category = item.Category.Title,
                        Title = item.Title,
                        Description = item.Description,
                        Id = item.Id,
                        EFileImages = item.FileImages,   
                        eFiles = item.FileImages.Count                     
                    };
                    eFileVMBindingSource.List.Add(vm);
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
        private void LoadAndUpdateForm()
        {
            WaitForm wait = new WaitForm(LoadEFiles);
            wait.ShowDialog();
            UpdateDgv(eFiles);
        }
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAndUpdateForm();
                Gujjar.AddDatagridviewButton(dgv, btndgvadd, "Add", "Add", 80);
                Gujjar.AddDatagridviewButton(dgv, btndgvview, "View", "View", 80);
                Helper.IsOkApplied();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            string txt = tbSearch.Text;
            if(string.IsNullOrEmpty(txt))
            {
                UpdateDgv(eFiles);
            }
            else
            {
                var files = eFiles.Where(a => a.Title.ToLower().Contains(txt.ToLower())).ToList();
                UpdateDgv(files);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                AddEFileForm form = new AddEFileForm();
                form.ShowDialog();
                if(form.IsDone)
                {
                    LoadAndUpdateForm();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ri = e.RowIndex;
                int ci = e.ColumnIndex;

                if (ri == -1 || ri == dgv.NewRowIndex)
                    return;

                if(dgv.Columns[btndgvview].Index == ci)
                {
                    int id = dgv.Rows[ri].Cells[0].Value.ToInt();

                    var eFileObj = eFileVMBindingSource.List.OfType<EFileVM>()
                        .FirstOrDefault(a => a.Id == id);

                    ViewFileForm form = new ViewFileForm(eFileObj.EFileImages, eFileObj.Title);
                    form.ShowDialog();
                }
                if(dgv.Columns[btndgvadd].Index == ci)
                {
                    int id = dgv.Rows[ri].Cells[0].Value.ToInt();
                    AddEFileImageForm form = new AddEFileImageForm(id);
                    form.ShowDialog();

                    if(form.IsDone)
                    {
                        LoadAndUpdateForm();
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
