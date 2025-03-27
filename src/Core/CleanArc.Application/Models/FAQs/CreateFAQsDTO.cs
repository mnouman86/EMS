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
        public int? GenericTitleId { get; set; } // Nullable Int for CategoryServiceID
        public string? Question { get; set; } // NVARCHAR(500) for Question
        public string? Answer { get; set; } // NVARCHAR(MAX) for Answer
        public int? CreatedBy { get; set; } // Nullable Int for CreatedBy
        public int? CultureId { get; set; }

    }
}
