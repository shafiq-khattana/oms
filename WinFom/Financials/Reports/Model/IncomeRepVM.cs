using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFom.Financials.ViewModel;

namespace WinFom.Financials.Reports.Model
{
    public class IncomeRepVM
    {
        public List<IncAcctExpLVM> Expenses { get; set; }
        public List<IncAcctIncLVM> Incomes { get; set; }
    }
}
