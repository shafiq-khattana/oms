using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EFiling.Model
{
    public class EFile
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateAdded { get; set; }
        public string Description { get; set; }
        public int EFCategoryId { get; set; }
        public EFCategory Category { get; set; }
        public List<EFileImage> FileImages { get; set; }

        public EFile()
        {
            FileImages = new List<EFileImage>();
        }
    }
}
