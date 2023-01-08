using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EFiling.Model
{
    public class EFileImage
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateAdded { get; set; }
        public string MimeType { get; set; }
        public byte[] PicData { get; set; }
        public int EFileId { get; set; }
        public EFile EFile { get; set; }
    }
}
