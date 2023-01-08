using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Khattana.Common;


namespace PIZAP.Forms
{
    public partial class ProdSearchForm : Form
    {
        public string prodId = null;
        
        public ProdSearchForm()
        {
            InitializeComponent();
            

        }
        
        private void btnHeaderClose_Click(object sender, EventArgs e)
        {
            Close();
        }


       

        private void ProdSearchForm_Load(object sender, EventArgs e)
        {
            try
            {
                Gujjar.DGVDesign(dataGridView1);
                tbSearch.Focus();
                
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void ProdSearchForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                prodId = null;
                Close();
            }
            if(e.KeyCode == Keys.F1)
            {
                tbSearch.Focus();
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string txt = tbSearch.Text;
                if(string.IsNullOrEmpty(txt))
                {
                    
                    dataGridView1.Refresh();
                    dataGridView1.ClearSelection();
                }
                else
                {
                    
                    dataGridView1.Refresh();
                    dataGridView1.ClearSelection();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void dataGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (dataGridView1.Rows.Count > 0)
                    {
                        prodId = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    }
                    Close();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if(e.RowIndex == -1 || dataGridView1.NewRowIndex == e.RowIndex)
                {
                    return;
                }
                prodId = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                Close();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Down)
            {
                dataGridView1.Focus();
                dataGridView1.TabIndex = 0;
                if(dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.Rows[0].Selected = true;
                }
            }
        }

        //bool shouldPerform = true;
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //if (shouldPerform)
            //{
            //    if (dataGridView1.Rows.Count > 0)
            //    {
            //        dataGridView1.Rows[0].Selected = false;
            //    }
            //}
            //shouldPerform = productSearchViewModelBindingSource.Count == 0 ? true : false;
        }

        private void tbSearch_Enter(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }
    }
}
