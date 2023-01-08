using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Deal.Model
{
    public class WeighBridge : IEquatable<WeighBridge>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public WeighBridgeType WeighBrideType { get; set; }

        public List<ScheduleWeighBridge> ScheduleWeighBridges { get; set; }
        public WeighBridge()
        {
            ScheduleWeighBridges = new List<ScheduleWeighBridge>();
        }

        public bool Equals(WeighBridge ot)
        {
            return (Name.Equals(ot.Name) && Address.Equals(ot.Address) && Phone.Equals(ot.Phone) && WeighBrideType == ot.WeighBrideType);
        }
    }

    public enum WeighBridgeType
    {
        Loading,
        Receiving
    }
}
