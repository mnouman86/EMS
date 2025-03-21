using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.CarImage.Queries.GetCarImageById
{
    public class GetCarImageByIdQueryResult
    {
        public int Id { get; set; }
        public int? BusinessID { get; set; }
        public int? CarDetailID { get; set; }
        public int? CategoryID { get; set; }
        public string? ImagePath { get; set; } // NVARCHAR(MAX)
        public string? ImageTitle { get; set; } // NVARCHAR(MAX)
        public bool? IsMain { get; set; } // BIT
        public int? CreatedBy { get; set; } // NVARCHAR(100)
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        //public bool IsRefundable { get; set; }
        //public bool IsCancelation { get; set; }
    }
}
