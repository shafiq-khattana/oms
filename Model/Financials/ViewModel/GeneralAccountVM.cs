using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Financials.ViewModel
{
    public class GeneralAccountVM
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public decimal Balance { get; set; }
        public int Links { get; set; }

        [DisplayName("Link Bal")]
        public decimal LinkBalance { get; set; }

        [DisplayName("Eff Balance")]
        public decimal EffectiveBalance { get; set; }

    }
}
