using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.Advertisement;

public  class Advertisement
    
{
    public int ID { get; set; }
    public int? PageID { get; set; }
    public int? PlaceID { get; set; }
    public string? ImageTitle { get; set; }
    public string? ImagePath { get; set; }
    public string? Url { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsDeleted { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    //public int CultureId { get; set; }
    //public int Code { get; set; }
    //public string Message { get; set; }


}
