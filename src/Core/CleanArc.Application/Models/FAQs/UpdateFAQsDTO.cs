using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.FAQs
{
    public class UpdateFAQsDTO
    {

        public int Id { get; set; } // Primary Key
        //public int? GenericTitleId { get; set; } // Nullable Int for CategoryServiceID
        public string? Question { get; set; } // NVARCHAR(500) for Question
        public string? Answer { get; set; } // NVARCHAR(MAX) for Answer
        //public bool? IsActive { get; set; } // Nullable Bit for IsActive
        //public bool? IsDeleted { get; set; } // Nullable Bit for IsDeleted
        //public int? CreatedBy { get; set; } // Nullable Int for CreatedBy
        //public DateTime? CreatedAt { get; set; } // Nullable DateTime for CreatedAt
        public int? UpdatedBy { get; set; } // Nullable Int for UpdatedBy
        //public DateTime? UpdatedAt { get; set; } // Nullable DateTime for UpdatedAt
        public int? CultureId { get; set; }
        //public DateTime? UpdatedAt { get; set; }
    }
}
