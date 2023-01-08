using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.Financials.Reports.Model
{
    public class FHeadRVM
    {
        public string HeadTitle { get; set; }
        public int AccountCount { get; set; }
        public decimal Balance { get; set; }
        public int TransCount { get; set; }
        public List<FAccountRVM> Accounts { get; set; }
    }
    public class FAccountRVM
    {
        public string Title { get; set; }
        public decimal Balance { get; set; }
        public int TransCount { get; set; }
    }
}
