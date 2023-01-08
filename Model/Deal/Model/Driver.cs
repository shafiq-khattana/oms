using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Deal.Model
{
    public class Driver : IEquatable<Driver>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameUrdu { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateAdded { get; set; }
        public string Extra { get; set; }

        public List<DealSchedule> Schedules { get; set; }
        public bool IsAvailable { get; set; }
        public Driver()
        {
            Schedules = new List<DealSchedule>();
        }
        
        public bool Equals(Driver other)
        {
            return (Name.ToLower().Equals(other.Name.ToLower()) && Contact.Equals(other.Contact));
        }
    }
}
