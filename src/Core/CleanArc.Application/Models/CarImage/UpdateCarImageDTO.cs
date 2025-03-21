using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.CarImage
{
    public class UpdateCarImageDTO
    {
        public int Id { get; set; }
        public int? BusinessID { get; set; }
        public int? CarID { get; set; }
        public string? ImagePath { get; set; } // NVARCHAR(MAX)
        public string? ImageTitle { get; set; } // NVARCHAR(MAX)
        public bool? IsMain { get; set; } // BIT
        public int? UpdatedBy { get; set; }
     
    }
}
