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
using Model.Deal.ViewModel;
using Model.Admin.Model;
using WinFom.Common.Model;

namespace WinFom.Deal.Forms
{
    public partial class AddScheduleForm : Form
    {
        private TradeUnit _tradeUnit = null;
        private decimal _totalTradeUnits = 0;
        private string _packing = "";
        private decimal _perPackQty = 0;
        private decimal _totalPackings = 0;
        private string _packingUnit = "";
        private decimal _addedTradeUnits = 0;
        private decimal _remainingTradeUnits = 0;
        private DateTime _scheduleDate = DateTime.Now;
        public ScheduleVM scheduleVM = null;
        private decimal _tradeUnitPrice = 0;
        private decimal _addPackingUnits = 0;
        private decimal _remainingPackingUnits = 0;
        private AppSettings appSett = Helper.AppSet;
        public AddScheduleForm(TradeUnit tradeUnit, 
            decimal totalTradeUnits, 
            string packing, 
            decimal addedTradeUnits, 
            decimal totalPackings, 
            decimal perPackQty, 
            string packingUnit, 
            DateTime scheduleDate, 
            decimal tradeUnitPrice,
            decimal addedPackingUnits,
            decimal remainingPackingUnits)
        {
            InitializeComponent();
            _tradeUnit = tradeUnit;
            _totalTradeUnits = totalTradeUnits;
            _packing = packing;
            _perPackQty = perPackQty;
            _totalPackings = totalPackings;
            _packingUnit = packingUnit;
            _addedTradeUnits = addedTradeUnits;
            _scheduleDate = scheduleDate;
            _tradeUnitPrice = tradeUnitPrice;
            _addPackingUnits = addedPackingUnits;
            _remainingPackingUnits = remainingPackingUnits;
        }

        private void picBtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                _remainingTradeUnits = _totalTradeUnits - _addedTradeUnits;
                if(_remainingTradeUnits <= 0)
                {
                    Gujjar.ErrMsg(string.Format("You can not add more schedules. Becuase there is no remaining {0} to schedule.", _tradeUnit.Title));
                    Close();
                }

                Gujjar.TB4(pMain);
                Gujjar.NumbersOnly(tbNowTradeUnits);
                label2.Text = string.Format("Total {0}(s):", _tradeUnit.Title);
                tbTotalTradeUnits.Text = _totalTradeUnits.ToString("n4");
                label1.Text = string.Format("Total {0}(s):", _packing);
                tbTotalPackings.Text = _totalPackings.ToString("n1");

                label3.Text = string.Format("Already scheduled {0}(s):", _tradeUnit.Title);
                label5.Text = string.Format("Remaining {0}(s) to be scheduled:", _tradeUnit.Title);
                label4.Text = string.Format("Now, no of {0}(s) to schedule:", _tradeUnit.Title);
                label12.Text = string.Format("Total Qty In {0}-(s):", _packingUnit);
                decimal ltradeUnits = 0;
                string ltradeUnitsStr = tbNowTradeUnits.Text;
                if(!string.IsNullOrEmpty(ltradeUnitsStr))
                {
                    ltradeUnits = ltradeUnitsStr.ToDecimal();
                }
                tbPrice.Text = (_tradeUnitPrice * ltradeUnits).ToString("n4");

                label7.Text = string.Format("No of {0}(s):", _packing);
                label8.Text = string.Format("Per {0} qty:", _packing);
                label9.Text = _packingUnit;
                tbPerPackQty.Text = _perPackQty.ToString();

                tbRemainingTradeUnits.Text = _remainingTradeUnits.ToString("n4");
                tbAddedTradeUnits.Text = _addedTradeUnits.ToString("n4");
                
                numUpDown.Value = 1;
                label13.Text = string.Format("Scheduled No of {0}-(s):", _packing);
                label14.Text = string.Format("Remaining {0}-(s) to Schedule:", _packing);
                tbAddedPackingUnits.Text = _addPackingUnits.ToString("n2");
                tbRemainingPackingUnits.Text = _remainingPackingUnits.ToString("n2");
                tbRemarks.Text = "N/A";
                Gujjar.TBOptional(tbRemarks);
                dtpArrival.MinDate = appSett.StartDate;
                dtpArrival.MaxDate = appSett.EndDate;
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbNowTradeUnits_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void numUpDown_ValueChanged(object sender, EventArgs e)
        {
            int num = (int)numUpDown.Value;
            DateTime dt = _scheduleDate.AddDays(num);
            if(dt.Date < _scheduleDate.Date)
            {
                throw new Exception(string.Format("Last schedule date {0}, you can't schedule with this date {1}", 
                    Gujjar.LocalDate(_scheduleDate), Gujjar.LocalDate(dt)));
            }
            dtpArrival.Value = dt;
        }

        private void tbPackings_Leave(object sender, EventArgs e)
        {
            try
            {
                //string packingStr = tbPackings.Text;
                //if(string.IsNullOrEmpty(packingStr))
                //{
                //    tbPackings.Focus();
                //    tbPackings.BackColor = Color.Pink;
                //    throw new Exception(string.Format("Please enter no of {0}", _packing));
                //}

                //decimal packingAmnt = packingStr.ToDecimal();
                //decimal totalKgs = packingAmnt * _perPackQty;
                //decimal totalTradeUnits = totalKgs / _tradeUnit.Qty;
                //decimal price = totalTradeUnits * _tradeUnitPrice;
                ////decimal totalTradeSubUnits = totalKgs;
                //tbNowTradeUnits.Text = totalTradeUnits.ToString("n2");
                //tbTotalTradeSubUnits.Text = totalKgs.ToString("n2");
                //tbPrice.Text = price.ToString("n2");
                
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbNowTradeUnits_Leave(object sender, EventArgs e)
        {

        }

        private void tbNowTradeUnits_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                decimal ltradeUnits = 0;
                string ltradeUnitsStr = tbNowTradeUnits.Text;
                if (!string.IsNullOrEmpty(ltradeUnitsStr))
                {
                    ltradeUnits = ltradeUnitsStr.ToDecimal();
                    if (ltradeUnits > _remainingTradeUnits)
                    {
                        throw new Exception(string.Format("Remaining {0} {1}-(s), and you entered now {2} {3}-(s). Please re-check it. Thanks", _remainingTradeUnits, _tradeUnit.Title, ltradeUnits, _tradeUnit.Title));
                    }
                    int lpackings = (int)Math.Ceiling(ltradeUnits * _tradeUnit.Qty / _perPackQty);
                    tbPackings.Text = lpackings.ToString();
                }
                tbPrice.Text = (_tradeUnitPrice * ltradeUnits).ToString("n4");
            }
            catch (Exception exp)
            {
                if(exp.Message.Contains("Remaining"))
                {
                    tbRemainingTradeUnits.Text = _remainingTradeUnits.ToString();
                }
                Gujjar.ErrMsg(exp);
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }
                decimal packingAmnt = tbPackings.Text.ToDecimal();
                if (packingAmnt > _remainingPackingUnits)
                {
                    tbPackings.Focus();
                    tbPackings.BackColor = Color.Pink;
                    string msg = string.Format("Remaining {0}-(s) and now scheduled {0}-(s). Please re-check. Thanks", _packing, _packing);
                    throw new Exception(msg);
                }
                scheduleVM = new ScheduleVM
                {
                    Id = Guid.NewGuid().ToString(),
                    ExpectedDate = dtpArrival.Value.ToShortDateString(),
                    PackingUnits = string.Format("{0}-{1}(s)", tbPackings.Text, _packing),
                    Price = tbPrice.Text,
                    TradeUnits = string.Format("{0}-{1}(s)", tbNowTradeUnits.Text, _tradeUnit.Title),
                    TotalSubUnits = string.Format("{0}-{1}(s)", tbTotalTradeSubUnits.Text, _packingUnit),
                    Remarks = tbRemarks.Text
                };
                Close();
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void tbPackings_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string packingStr = tbPackings.Text;
                if (string.IsNullOrEmpty(packingStr))
                {
                    tbPackings.Focus();
                    tbPackings.BackColor = Color.Pink;
                    return;
                    //throw new Exception(string.Format("Please enter no of {0}", _packing));
                }

                decimal packingAmnt = packingStr.ToDecimal();
                if(packingAmnt > _remainingPackingUnits)
                {
                    string msg = string.Format("Remaining {0} {1}-(s) and now scheduled {2} {3}-(s). Please re-check. Thanks", _remainingPackingUnits,
                        _packing, packingAmnt, _packing);
                    tbPackings.Text = _remainingPackingUnits.ToString();
                    tbPackings.Select(0, tbPackings.Text.Length);

                    throw new Exception(msg);
                }
                decimal totalKgs = packingAmnt * _perPackQty;
                decimal totalTradeUnits = totalKgs / _tradeUnit.Qty;
                decimal price = totalTradeUnits * _tradeUnitPrice;
                //decimal totalTradeSubUnits = totalKgs;
                tbNowTradeUnits.Text = totalTradeUnits.ToString("n4");
                tbTotalTradeSubUnits.Text = totalKgs.ToString("n4");
                tbPrice.Text = price.ToString("n4");

            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
