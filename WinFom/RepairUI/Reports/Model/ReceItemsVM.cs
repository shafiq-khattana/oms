using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFom.RepairUI.Reports.Model
{
    public class ReceItemsVM
    {
        public int Id { get; set; }
        public string RecePerson { get; set; }
        public string Dated { get; set; }
        public decimal TotalReceItems { get; set; }
        public decimal BillAmount { get; set; }
        public List<ReceItem> Items { get; set; }
        public ReceItemsVM()
        {
            Items = new List<ReceItem>();
        }
    }
    public class ReceItem
    {
        public string Item { get; set; }
        public decimal Qty { get; set; }
        public string Remarks { get; set; }
    }
}
