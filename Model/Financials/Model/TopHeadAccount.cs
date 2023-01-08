using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Financials.Model
{
    public class TopHeadAccount : AccountBase
    {
        public string HeadAccountId { get; set; }
        public HeadAccount HeadAccount { get; set; }
        public List<SubHeadAccount> SubHeadAccounts { get; set; }

        public TopHeadAccount()
        {
            SubHeadAccounts = new List<SubHeadAccount>();
        }
    }
}
