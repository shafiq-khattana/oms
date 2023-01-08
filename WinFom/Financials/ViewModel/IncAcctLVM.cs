using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.Financials.ViewModel
{
    public class IncAcctExpLVM
    {
        public string Account { get; set; }
        public decimal Balance { get; set; }
    }
    public class IncAcctIncLVM
    {
        public string Account { get; set; }
        public decimal Balance { get; set; }
    }
    public class GenAcctLVM
    {
        public string Account { get; set; }
        public decimal Balance { get; set; }
    }
}
