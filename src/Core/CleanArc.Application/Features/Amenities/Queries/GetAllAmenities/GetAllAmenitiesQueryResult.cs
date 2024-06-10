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
    public int ID { get; set; }
    public string CategoryName { get; set; }
    public string AmenityName { get; set; }
    public int CategoryID { get; set; }
    public string? Icon { get; set; }

    public string Description { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }


}
