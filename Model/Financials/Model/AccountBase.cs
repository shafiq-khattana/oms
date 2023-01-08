using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Financials.Model
{
    public class AccountBase
    {
        public string Id { get; set; }
        public string AccountNo { get; set; }
        public string Address { get; set; }
        public string AddressUrdu { get; set; }
        public string Title { get; set; }
        public string TitleUrdu { get; set; }
        public string Description { get; set; }
        public AccountNature AccountNature { get; set; }
        public bool ExplicitilyCreated { get; set; }
        

        public AccountBase()
        {
            //AccountTransactions = new List<AccountTransaction>();
        }
    }
    public enum AccountNature
    {
        Credit,
        Debit
    }
}
