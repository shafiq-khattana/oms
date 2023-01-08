using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Admin.Model
{
    public class KeyInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public string Key { get; set; }
        public DateTime DateAdded { get; set; }
        public string IsValid { get; set; }
        public string WahJi { get; set; }
    }
}
