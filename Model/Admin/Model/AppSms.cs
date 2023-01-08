using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Admin.Model
{
    public class AppSms
    {
        public int Id { get; set; }
        public string ReceiverCell { get; set; }
        public string Message { get; set; }
        public string Sender { get; set; }
        public bool IsSent { get; set; }
        public DateTime GenerateTime { get; set; }
        public DateTime? SentTime { get; set; }
    }
}
