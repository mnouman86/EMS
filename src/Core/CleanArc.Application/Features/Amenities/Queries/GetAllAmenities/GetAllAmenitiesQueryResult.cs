using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Amenities.Queries.GetAllAmenities;

public record GetAllAmenitiesQueryResult
//(int Id,string Name, string Description, int CategoryID,bool IsActive,bool IsDeleted, 
//int CreatedBy, DateTime CreatedAt,int UpdatedBy, DateTime UpdatedAt);
{
    public int Id { get; set; }
    public string Name { get; set; }
    //public string ServiceName { get; set; }
    //public int ServiceId { get; set; }
    //public int? ServiceCategoryId { get; set; }
    //public string? ServiceCategoryName { get; set; }
    public int? ServiceTypeEnumId { get; set; }
    public string? AmenityTypeEnum { get; set; }
    
    public string? Icon { get; set; }

    public string Description { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }


}
