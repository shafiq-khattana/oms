using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Admin.Model
{
    public class Rahzam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public string ItheyRakh { get; set; }
        public string ChalJanDey { get; set; }
        public string HunAramEe { get; set; }
        public string ChalBasKerYar { get; set; }
        public string ChangaPhir { get; set; }
        public string RabRakha { get; set; }
    }
}
