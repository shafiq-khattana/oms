using System;
using System.Collections.Generic;

namespace Model.ReadyStuff.Model
{
    public class ReadyDriver : IEquatable<ReadyDriver>
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
        public ReadyDriver()
        {
            Schedules = new List<ReadySchedule>();
        }

        public bool Equals(ReadyDriver other)
        {
            return (Name.ToLower().Equals(other.Name.ToLower()) && Contact.Equals(other.Contact));
        }
    }
}