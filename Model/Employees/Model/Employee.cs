using Model.Deal.Model;
using Model.Financials.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Employees.Model
{
    public enum EmployeeType
    {
        ScheduleSelector,
        FactoryWorker
    }
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameUrdu { get; set; }
        public string Designation { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateEnded { get; set; }
        public string Extra { get; set; }

        public string CNIC { get; set; }
        public byte[] PicData { get; set; }
        public byte[] ThumbData { get; set; }
        public byte[] ThumbPicData { get; set; }
        
        public bool IsAvailable { get; set; }

        public GeneralAccount Account { get; set; }
        public string GeneralAccountId { get; set; }
        public decimal Salary { get; set; }
        public decimal Balance { get; set; }
        public bool CanLogin { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public List<CreditEntry> CredityEntries { get; set; }
        public List<AttendanceRecord> AttendaceRecords { get; set; }
        public List<DealSchedule> DealSchedules { get; set; }
        public List<EmployPayRollEntry> PayrollEntries { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public Employee()
        {
            CredityEntries = new List<CreditEntry>();
            AttendaceRecords = new List<AttendanceRecord>();
            DealSchedules = new List<DealSchedule>();
            PayrollEntries = new List<EmployPayRollEntry>();
        }

        public bool Equals(Employee other)
        {
            return (Name.ToLower().Equals(other.Name.ToLower()) && Address.ToLower().Equals(other.Address.ToLower()) && Contact.Equals(other.Contact));
        }
    }
}
