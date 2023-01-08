using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.Financials.Reports.Model
{
    public class FSummaryRVM
    {
        public int TotalAccount { get; set; }
        public int TotalTrans { get; set; }
        public decimal TotalAssets { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal TotalLiability { get; set; }
        public decimal TotalIncome { get; set; }

        public List<FBarchRVM> FBarchRVMList { get; set; }
        public decimal AssetsPlusExpenses
        {
            get
            {
                return TotalAssets + TotalExpenses;
            }
        }
        public decimal LiabilityPlusIncome
        {
            get
            {
                return TotalLiability + TotalIncome;
            }
        }

        public FSummaryRVM()
        {
            FBarchRVMList = new List<FBarchRVM>();
        }
    }
}
