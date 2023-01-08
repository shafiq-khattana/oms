using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Admin.Model
{
    public class SmsSubscriber
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DisplayName("Mobile No")]
        public string CellNo { get; set; }

        [DisplayName("Regular Alerts")]
        public bool IsActive { get; set; }

        [DisplayName("Closing Repoet Alerts")]
        public bool ShouldReceiveCloseReportAlert { get; set; }
        public List<AppMessageState> ShopMessageStates { get; set; }

        public SmsSubscriber()
        {
            ShopMessageStates = new List<AppMessageState>();
        }
    }
}
