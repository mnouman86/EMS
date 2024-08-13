using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.ActivityIncludeOptionMapping;

public  class ActivityIncludeOptionMapping

{
    public int ID { get; set; }
    public string? IncludeOptionIDs { get; set; }
    public string? Name { get; set; }
    public int? ActivityID { get; set; }
    public string? Title { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsDelete { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? CultureId { get; set; }
    public int? Code { get; set; }
    public int? Message { get; set; }
    public string SeasonIDs { get; set; }
}
