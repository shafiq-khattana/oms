using Model.Financials.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Deal.Model
{
    public class GoodCompany : IEquatable<GoodCompany>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameUrdu { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Owner { get; set; }
        public string OwnerContact { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateAdded { get; set; }
        public string Extra { get; set; }
        public List<DealSchedule> Schedules { get; set; }

        [ForeignKey("GeneralAccountId")]
        public GeneralAccount Account { get; set; }
        public string GeneralAccountId { get; set; }
        public List<PackingIssueReceiveRecord> PackingIssueReceiveRecords { get; set; }
        public List<PackingStock> PackingStocks { get; set; }
        public GoodCompany()
        {
            //Drivers = new List<Driver>();
            //Vehicles = new List<Vehicle>();
            PackingIssueReceiveRecords = new List<PackingIssueReceiveRecord>();
            PackingStocks = new List<PackingStock>();
        }

        public bool Equals(GoodCompany ot)
        {
            return (Name.Equals(ot.Name) && Phone.Equals(ot.Phone) && Address.Equals(ot.Address) && Owner.Equals(ot.Owner));
        }
    }
}
