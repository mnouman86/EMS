using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchCarAmenities.Queries.GetAllSearchCarAmenities;

public class GetAllSearchCarAmenitiesQueryResult
{

    public int? Id { get; set; }
    public int? CarDetailID { get; set; }
    public string? Amenity { get; set; }
    public string? Description { get; set; }
    public bool? Selected { get; set; }

}

