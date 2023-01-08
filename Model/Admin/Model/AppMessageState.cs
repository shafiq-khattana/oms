using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Admin.Model
{
    public class AppMessageState
    {
        public int Id { get; set; }

        public int ShopMessageId { get; set; }

        [ForeignKey("ShopMessageId")]
        public ShopMessage ShopMessage { get; set; }

        public int SmsSubsriberId { get; set; }

        [ForeignKey("SmsSubsriberId")]
        public SmsSubscriber SmsSubsriber { get; set; }

        public bool State { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
