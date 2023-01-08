using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Financials.Model
{
    public class SubHeadAccount : AccountBase
    {
        public string TopHeadAccountId { get; set; }
        public TopHeadAccount TopHeadAccount { get; set; }

        public List<GeneralAccount> Accounts { get; set; }
        public SubHeadAccount()
        {
            Accounts = new List<GeneralAccount>();
        }
    }
}
