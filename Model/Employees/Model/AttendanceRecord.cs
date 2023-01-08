using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Employees.Model
{
    public class AttendanceRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public DateTime AttendIn { get; set; }
        public DateTime? AttendOut { get; set; }
    }
    public enum EmployeeStatus
    {
        Present,
        Absent
    }
}
