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
    public string Name { get; set; }
    public string Description { get; set; }
    public int CategoryID { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }

}
