using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Amenities.Queries.GetAmenitiesById
{
    public record GetAmenityByIdQueryResult(int Id, string Name, string Description, int CategoryID, bool IsActive, bool IsDeleted,
    int CreatedBy, DateTime CreatedAt, int UpdatedBy, DateTime UpdatedAt);


}
