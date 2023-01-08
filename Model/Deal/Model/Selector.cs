using Model.Financials.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Deal.Model
{
    public class Selector
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

        public List<OmeDeal> Deals { get; set; }
        public List<DealSchedule> Schedules { get; set; }

        [ForeignKey("GeneralAccountId")]
        public GeneralAccount Account { get; set; }
        public string GeneralAccountId { get; set; }
        public Selector()
        {
            Deals = new List<OmeDeal>();
            Schedules = new List<DealSchedule>();
        }

        public bool Equals(Selector other)
        {
            return (Name.ToLower().Equals(other.Name.ToLower()) && Address.ToLower().Equals(other.Address.ToLower()) && Contact.Equals(other.Contact));
        }
    }
}