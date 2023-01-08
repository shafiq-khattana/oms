using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repair.ViewModel
{
    public class DispatchVM
    {
        public int Id { get; set; }

        [DisplayName("Bill Id")]
        public string BillNo { get; set; }
        public string Date { get; set; }

        [DisplayName("Repair Shop")]
        public string Place { get; set; }

        [DisplayName("T.O.Person")]
        public string TOPerson { get; set; }

        [DisplayName("T.Items")]
        public decimal TotalItems { get; set; }

        [DisplayName("Received")]
        public decimal ReceivedItems { get; set; }

        [DisplayName("Remaining")]
        public decimal RemainingItems { get; set; }

        [DisplayName("Bill Paid")]
        public decimal BillPaid { get; set; }

        [Browsable(false)]
        public int PlaceId { get; set; }
    }
}
