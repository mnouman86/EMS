using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.ActivityImageMapping;

public  class ActivityImageMapping
    
{
    public int Id { get; set; }
    public int? ActivityID { get; set; }
    public string? ImagePath { get; set; }
	public List<string>? ImagePaths { get; set; }  // Changed from string? to List<string>?
	public string? ImageTitle { get; set; }
    public bool? IsMain { get; set; }
    public int? CategoryID { get; set; }
    public int? BusinessID { get; set; }
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
