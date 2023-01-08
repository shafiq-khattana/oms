﻿using Khattana.Common;
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

namespace WinFom.Deal.Forms
{
    public partial class AddTradeUnit : Form
    {
        public int TradeUnitId = 0;
        public AddTradeUnit()
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

                //Gujjar.NumbersOnly(tbPrice);
                Gujjar.NumbersOnly(tbQty);
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if(!Gujjar.IsValidForm(pMain))
                {
                    throw new Exception("Please fill all text fields");
                }

                TradeUnit tradeUnit = new TradeUnit
                {
                    Title = tbTitle.Text,
                    
                    Qty = tbQty.Text.ToDecimal()
                };

               
                using (Context db = new Context())
                {
                    var dbObj = db.TradeUnits.FirstOrDefault(a => a.Title.ToLower() == tradeUnit.Title.ToLower());
                    if(dbObj != null)
                    {
                        throw new Exception(string.Format("Trade unit with this name ({0}) already exists in database", dbObj.Title));
                    }

                    tradeUnit = db.TradeUnits.Add(tradeUnit);
                    db.SaveChanges();

                    Gujjar.InfoMsg("Trade unit is added in database successfully");
                    TradeUnitId = tradeUnit.Id;
                    Close();
                }
            }
            catch (Exception exp)
            {
                Gujjar.ErrMsg(exp);
            }
        }
    }
}
