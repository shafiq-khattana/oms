using Model.EFiling.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EFiling.ViewModel
{
    public class EFileVM
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [Browsable(false)]
        public List<EFileImage> EFileImages { get; set; }
        [DisplayName("Date Added")]
        public DateTime DateAdded { get; set; }
        public int eFiles { get; set; }
    }
}
