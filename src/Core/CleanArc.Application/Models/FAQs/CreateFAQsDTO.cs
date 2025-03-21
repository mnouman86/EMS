using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.FAQs
{
    public class CreateFAQsDTO
    {
        // public int Id { get; set; }
        public int? CategoryServiceID { get; set; } // Nullable Int for CategoryServiceID
        public int? ServiceID { get; set; } // Nullable Int for ServiceID
        public int? SubServiceID { get; set; } // Nullable Int for SubServiceID
        public string? Question { get; set; } // NVARCHAR(500) for Question
        public string? Answer { get; set; } // NVARCHAR(MAX) for Answer
        public int? CreatedBy { get; set; } // Nullable Int for CreatedBy
        public int? CultureId { get; set; }
        public int? Code { get; set; }
        public string? Message { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public int? UpdatedBy { get; set; }
        //public DateTime? UpdatedAt { get; set; }

    }
}
