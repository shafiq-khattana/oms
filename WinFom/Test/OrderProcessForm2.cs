using Khattana.BioMetric.SecuGenFingerPrintNonAuto;
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
    public partial class OrderProcessForm2 : Form
    {
        
        public OrderProcessForm2()
        {
            InitializeComponent();
            
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OrderProcessForm2_Load(object sender, EventArgs e)
        {
            try
            {
                Gujjar.NumbersOnly(tbAmountGiven);
                Gujjar.NumbersOnly(tbDeliveryBoyAmount);

                
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbAmountGiven_TextChanged(object sender, EventArgs e)
        {
            string txt = tbAmountGiven.Text;
            string txt2 = tbPaymentToReceive.Text;
            try
            {
                if (string.IsNullOrEmpty(txt))
                {
                    txt = "0";
                }
                if (string.IsNullOrEmpty(txt2))
                {
                    txt2 = "0";
                }
                decimal amountGiven = txt.ToDecimal();
                decimal payment = txt2.ToDecimal();

                decimal change = amountGiven - payment;

                tbChangeGiven.Text = change.ToString("n2");
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void creditRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (!creditRadioBtn.Checked)
                return;
           
        }

        private void cashRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!cashRadioBtn.Checked)
                    return;


                
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }


        }

        private void OrderProcessForm2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Control && e.KeyCode == Keys.Z)
                {
                    homeRadioBtn.Checked = true;
                    homeRadioBtn_CheckedChanged(null, null);
                }
                if (e.Control && e.KeyCode == Keys.X)
                {
                    creditRadioBtn.Checked = true;
                    creditRadioBtn_CheckedChanged(null, null);
                }
                if (e.Control && e.KeyCode == Keys.C)
                {
                    cashRadioBtn.Checked = true;
                    cashRadioBtn_CheckedChanged(null, null);
                }
                if (e.Control && e.KeyCode == Keys.A)
                {
                    tbAmountGiven.Focus();
                }

                if (e.KeyCode == Keys.F1)
                {

                    walkInNameTB.Focus();
                }
                if (e.KeyCode == Keys.F2)
                {

                    walkInCellTB.Focus();
                }
                if (e.KeyCode == Keys.F3)
                {

                    walkInAddressTB.Focus();
                }
                if (e.Control && e.KeyCode == Keys.S)
                {
                    btnOk.PerformClick();
                }
                if (e.Control && e.KeyCode == Keys.D)
                {
                   
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {

        }

        private void custSearchBtn_Click(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void textbox_Leave(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            string txt = tb.Text;
            if (string.IsNullOrEmpty(txt))
            {
                tb.Text = "N/A";
            }
        }

        private void textbox_enter(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            string tx = tb.Text;
            if (tx.Equals("N/A") || tx.Equals("n/a"))
            {
                tb.Clear();
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void homeRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                          
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
            

        }
    }

}
