using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.KBDescription;

public  class KBDescription
    
{
    public int ID { get; set; }
    public int? KBDetailID { get; set; }
    public string? SubHeading { get; set; }
    public string? Content { get; set; }
    public string? KBContentType { get; set; }
    public string? MediaType { get; set; }
    public string? ImagePath { get; set; }
    public string? ImageTitle { get; set; }
    public bool? IsMain { get; set; }
    public int? MediaID { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsDeleted { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? CultureId { get; set; }
    public int? Code { get; set; }
    public string? Message { get; set; }

}
