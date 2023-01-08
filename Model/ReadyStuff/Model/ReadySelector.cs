﻿using Model.Financials.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ReadyStuff.Model
{
    public class ReadySelector
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

        public string CNIC { get; set; }
        public byte[] PicData { get; set; }
        public byte[] ThumbData { get; set; }
        public byte[] ThumbPicData { get; set; }
        public List<ReadySchedule> Schedules { get; set; }
        public bool IsAvailable { get; set; }
        public GeneralAccount Account { get; set; }
        public string GeneralAccountId { get; set; }
        public ReadySelector()
        {
            
            Schedules = new List<ReadySchedule>();
        }

        public bool Equals(ReadySelector other)
        {
            return (Name.ToLower().Equals(other.Name.ToLower()) && Address.ToLower().Equals(other.Address.ToLower()) && Contact.Equals(other.Contact));
        }
    }
}
