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
using Model.Retail.Model;

namespace WinFom.Retail.Forms
{
    public partial class OrderProcessForm : Form
    {
        public OrdProcessVM OrderProcessViewModel = null;
        public bool IsCredit = false;
        private Customer customer = null;
        private decimal dueAmount = 0;
        private int numCopies2 = 1;
        private bool printerState = false;
        private bool customerPrint = false;
        public OrderProcessForm(Customer cust, decimal dueAmount, int numberCopies, bool printState, bool custPrint)
        {
            InitializeComponent();
            customer = cust;
            this.dueAmount = dueAmount;
            numCopies2 = numberCopies;
            printerState = printState;
            customerPrint = custPrint;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            button1.PerformClick();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                label9.BackColor = Color.LimeGreen;
                OrderProcessViewModel = new OrdProcessVM();
                panelWalkIn.Enabled = false;
                if(customer.Id == 1)
                {
                    groupBox1.Enabled = false;
                    OrderProcessViewModel.IsCredit = false;
                    OrderProcessViewModel.IsWalkIn = true;
                    OrderProcessViewModel.OrderType = SaleOrderType.Cash;
                }
                else
                {
                    OrderProcessViewModel.IsWalkIn = false;
                    groupBox1.Enabled = true;
                }
                walkInAddressTB.Text = customer.Address;
                walkInCellTB.Text = customer.Contact;
                walkInNameTB.Text = customer.Name;

                tbPaymentToReceive.Text = dueAmount.ToString("n1");
                tbAmountGiven.Text = dueAmount.ToString("n1");
                tbAmountGiven.Focus();
                tbAmountGiven.Select(0, tbAmountGiven.Text.Length);
                numCopies.Value = numCopies2;
                bswPrinterState.Value = bswCustomer.Value = printerState;
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbAmountGiven_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal amountGiven = 0;
                decimal remainingAmount = dueAmount;
                string txt = tbAmountGiven.Text;
                if(!string.IsNullOrEmpty(txt))
                {
                    amountGiven = txt.ToDecimal();
                }
                decimal change = 0;
                switch(OrderProcessViewModel.OrderType)
                {
                    case SaleOrderType.Cash:
                        change = amountGiven - dueAmount;
                        break;

                    case SaleOrderType.Partial:
                    case SaleOrderType.Credit:
                        change = 0;
                        break;
                }
                tbChangeGiven.Text = change.ToString("n1");
                remainingAmount = dueAmount - amountGiven;
                if(remainingAmount <= 0 && customer.Id == 1)
                {
                    remainingAmount = 0;
                }
                if(remainingAmount < 0 && OrderProcessViewModel.OrderType == SaleOrderType.Partial)
                {
                    Gujjar.InfoMsg("This can't be a partial order, as you have entered more amount than required. It is converted into cash order");
                    cashRadioBtn.Checked = true;
                    remainingAmount = 0;
                }
                tbRemainingAmount.Text = remainingAmount.ToString("n1");
            }
            catch(FormatException)
            {
                //ignore
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OrderProcessViewModel = null;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }
                decimal amountGiven = 0;
                string txt1 = tbAmountGiven.Text;
                if(!string.IsNullOrEmpty(txt1))
                {
                    amountGiven = txt1.ToDecimal();
                }
                if(OrderProcessViewModel.OrderType == SaleOrderType.Partial)
                {
                    if(amountGiven == dueAmount)
                    {
                        throw new Exception("This is partial order, then how can be amount given and due amount is same?");
                    }
                }
                if(amountGiven < dueAmount && OrderProcessViewModel.OrderType == SaleOrderType.Cash)
                {
                    tbAmountGiven.Focus();
                    throw new Exception("Amount given is less than due amount");
                }
                if(amountGiven != 0 && OrderProcessViewModel.OrderType == SaleOrderType.Credit)
                {
                    throw new Exception("In credit order, amount given must be zero. Or make this order as partial order");
                }
                DialogResult res = Gujjar.ConfirmYesNo("Please confirm!!..");
                if (res == DialogResult.No)
                    return;

                OrderProcessViewModel.AmountGiven = amountGiven;
                OrderProcessViewModel.ChangeGiven = tbChangeGiven.Text.ToDecimal();
                OrderProcessViewModel.IsCredit = IsCredit;
                OrderProcessViewModel.PrinterState = bswPrinterState.Value;
                OrderProcessViewModel.Copies = (int)numCopies.Value;
                OrderProcessViewModel.RemainingAmount = tbRemainingAmount.Text.ToDecimal();
                OrderProcessViewModel.CustomerPrintCopy = bswCustomer.Value;
                Close(); 
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void cashRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if(cashRadioBtn.Checked)
            {
                OrderProcessViewModel.RemainingAmount = 0;
                OrderProcessViewModel.OrderType = SaleOrderType.Cash;
                IsCredit = false;
                label9.BackColor = Color.LimeGreen;
                tbAmountGiven.Text = dueAmount.ToString("n1");                
                tbAmountGiven.Focus();
                tbAmountGiven.Select(0, tbAmountGiven.Text.Length);
            }
            
        }

        private void creditRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if(creditRadioBtn.Checked)
            {
                IsCredit = true;
                label9.BackColor = Color.Coral;
                
                tbAmountGiven.Text = "0";
                tbChangeGiven.Text = "0";
                tbRemainingAmount.Text = tbPaymentToReceive.Text;
                OrderProcessViewModel.OrderType = SaleOrderType.Credit;
                OrderProcessViewModel.RemainingAmount = tbRemainingAmount.Text.ToDecimal();
            }
            tbAmountGiven.Enabled = !creditRadioBtn.Checked;
        }

        private void OrderProcessForm_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if(e.Control && e.KeyCode == Keys.C)
                {
                    cashRadioBtn.Checked = true;
                    
                }
                if(e.Control && e.KeyCode == Keys.X)
                {
                    creditRadioBtn.Checked = true;
                   
                }
                if(e.Control && e.KeyCode == Keys.S)
                {
                    btnOk.PerformClick();
                }
                if(e.Control && e.KeyCode == Keys.Z)
                {
                    partialRadioButton.Checked = true;
                }
                if(e.Control && e.KeyCode == Keys.A)
                {
                    bswCustomer.Value = !bswCustomer.Value;
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void partialRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (partialRadioButton.Checked)
            {
                IsCredit = true;
                label9.BackColor = Color.Orange;

                tbAmountGiven.Text = "0";
                tbChangeGiven.Text = "0";
                tbRemainingAmount.Text = tbPaymentToReceive.Text;
                OrderProcessViewModel.OrderType = SaleOrderType.Partial;
                OrderProcessViewModel.RemainingAmount = tbRemainingAmount.Text.ToDecimal();

                tbAmountGiven.Text = dueAmount.ToString("n1");
                tbAmountGiven.Focus();
                tbAmountGiven.Select(0, tbAmountGiven.Text.Length);
            }
        }
    }
}
