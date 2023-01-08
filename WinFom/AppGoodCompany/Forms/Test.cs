using Khattana.Common;
using Model.Deal.Model;
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
using WinFom.Common.Forms;

namespace WinFom.AppGoodCompany.Forms
{
    public partial class Test : Form
    {
        private TradeUnit tradeUnit = null;
        //private List<TradeUnit> tradeUnits = null;
        public Test()
        {
            InitializeComponent();
        }

        private void Test_Load(object sender, EventArgs e)
        {
           
           
        }

        
        

        private void cbTradeUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTradeUnits.SelectedIndex == -1)
                return;

            tradeUnit = cbTradeUnits.SelectedItem as TradeUnit;
            string title = tradeUnit.Title;
            decimal qty = tradeUnit.Qty;
            if (qty == 0)
            {
                qty = 1;
            }
            label11.Text = string.Format("Per {0} qty:", title);
            label12.Text = string.Format("Total {0}(s):", title);
            label13.Text = string.Format("Per {0} price:", title);
            tbPerTradeUnitQty.Text = qty.ToString("n4");

            string totalWeightStr = "";//tbGrandTotal.Text;
            if (!string.IsNullOrEmpty(totalWeightStr))
            {
                decimal totalWeight = totalWeightStr.ToDecimal();
                decimal totalTradeUnits = totalWeight / qty;

                tbTotalTradeUnits.Text = totalTradeUnits.ToString("n4");
            }
            else
            {
                tbTotalTradeUnits.Text = "0";
            }
        }

        private void tbPerTradeUnitPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal totalTradeUnits = 0;
                if (!string.IsNullOrEmpty(tbTotalTradeUnits.Text))
                {
                    totalTradeUnits = tbTotalTradeUnits.Text.ToDecimal();
                }
                decimal perPrice = 0;
                if (!string.IsNullOrEmpty(tbPerTradeUnitPrice.Text))
                {
                    perPrice = tbPerTradeUnitPrice.Text.ToDecimal();
                }
                decimal totalPrice = totalTradeUnits * perPrice;

                tbTotalPrice.Text = totalPrice.ToString("n4");
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbBrokerSharePercentage_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal totalPrice = 0;
                if (!string.IsNullOrEmpty(tbTotalPrice.Text))
                {
                    totalPrice = tbTotalPrice.Text.ToDecimal();
                }
                decimal bshper = 0;
                if (!string.IsNullOrEmpty(tbBrokerSharePercentage.Text))
                {
                    bshper = tbBrokerSharePercentage.Text.ToDecimal();

                }

                decimal brokerShare = totalPrice * bshper / 100;
                decimal netprice = totalPrice - brokerShare;
                tbBrokerShare.Text = brokerShare.ToString("n4");
                tbNetPrice.Text = netprice.ToString("n4");

            }
            catch (Exception)
            {
                //Gujjar.ErrMsg(ep);
            }
        }

        private void tbBrokerSharePercentage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.OemPeriod)
                {
                    string str = tbBrokerSharePercentage.Text;
                    if (string.IsNullOrEmpty(str))
                    {
                        //tbBrokerSharePercentage.Text = "0";
                        e.Handled = true;
                        //sch.BrokerShareAmount = tbBrokerShare.Text.ToDecimal();
                        //sch.BrokerSharePercentage = tbBrokerSharePercentage.Text.ToDecimal();
                        //sch.NetScheduleAmount = tbNetPrice.Text.ToDecimal();
                        //sch.GrossPrice = tbTotalPrice.Text.ToDecimal();
                        //sch.PerTradeUnitPrice = tbPerTradeUnitPrice.Text.ToDecimal();
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
