using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EFiling.Model
{
    public class EFCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<EFile> EFIles { get; set; }

        public EFCategory()
        {
            EFIles = new List<EFile>();
        }
    }
}
