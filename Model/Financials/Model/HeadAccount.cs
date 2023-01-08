using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Financials.Model
{
    public class HeadAccount : AccountBase
    {
        public List<TopHeadAccount> TopHeadAccounts { get; set; }
        public HeadAccount()
        {
            TopHeadAccounts = new List<TopHeadAccount>();
        }
    }
}
