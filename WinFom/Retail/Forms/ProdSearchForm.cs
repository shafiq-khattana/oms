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
using Model.Retail.ViewModel;

namespace WinFom.Retail.Forms
{
    public partial class ProdSearchForm : Form
    {
        private List<ProdSearchVM> prodList = null;
        public int ProdId = 0;
        public ProdSearchForm(List<ProdSearchVM> vm)
        {
            InitializeComponent();
            prodList = vm;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                Gujjar.DGVDesign(dataGridView1);
                dataGridView1.DataSource = prodList;
                dataGridView1.Refresh();

                dataGridView1.ClearSelection();
                tbCount.Text = prodList.Count.ToString();
                tbSearch.Focus();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            string term = tbSearch.Text;
            if(string.IsNullOrEmpty(term))
            {
                dataGridView1.DataSource = prodList;
                dataGridView1.Refresh();
                dataGridView1.ClearSelection();
                tbCount.Text = prodList.Count.ToString();
            }
            else

            {
                var data = prodList.Where(a => a.Title.ToLower().Contains(term.ToLower())).ToList();
                dataGridView1.DataSource = data;
                dataGridView1.Refresh();
                dataGridView1.ClearSelection();
                tbCount.Text = data.Count.ToString();
            }
        }

        private void dataGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    ProdId = dataGridView1.SelectedRows[0].Cells[0].Value.ToInt();
                }
                Close();
            }
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Down)
            {
                dataGridView1.Focus();
                dataGridView1.TabIndex = 0;
                if (dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.Rows[0].Selected = true;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1 || dataGridView1.NewRowIndex == e.RowIndex)
                {
                    return;
                }
                ProdId = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToInt();
                Close();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
