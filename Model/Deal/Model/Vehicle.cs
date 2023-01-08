using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Deal.Model
{
    public class Vehicle : IEquatable<Vehicle>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string No { get; set; }
        public VehicleType VehicleType { get; set; }

        public List<DealSchedule> Schedules { get; set; }
        public VehicleStatus Status { get; set; }

        public bool Equals(Vehicle ot)
        {
            return (No.Equals(ot.No) && VehicleType == ot.VehicleType);
        }
    }
    public enum VehicleType
    {
        Truck,
        Tarala,
        Trolli,
        Others
    }
    public enum VehicleStatus
    {
        Dispatched,
        Loaded,
        Available
    }
}