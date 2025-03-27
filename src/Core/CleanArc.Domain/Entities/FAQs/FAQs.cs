using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.FAQs;

public  class FAQs
    
{
    public int Id { get; set; } // Primary Key
    public int? GenericTitleId { get; set; } // Nullable Int for CategoryServiceID
    public string? Question { get; set; } // NVARCHAR(500) for Question
    public string? Answer { get; set; } // NVARCHAR(MAX) for Answer
    public int? CreatedBy { get; set; } // Nullable Int for CreatedBy
    public DateTime? CreatedAt { get; set; } // Nullable DateTime for CreatedAt
    public int? UpdatedBy { get; set; } // Nullable Int for UpdatedBy
    public DateTime? UpdatedAt { get; set; } // Nullable DateTime for UpdatedAt
    public int? CultureId { get; set; }

}
