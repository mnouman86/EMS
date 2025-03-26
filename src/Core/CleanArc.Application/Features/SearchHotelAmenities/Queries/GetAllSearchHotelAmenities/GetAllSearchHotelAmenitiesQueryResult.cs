using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchHotelAmenities.Queries.GetAllSearchHotelAmenities;

public class GetAllSearchHotelAmenitiesQueryResult
{
    public string? Amenity { get; set; }
    public string? Description { get; set; }
    public bool Selected { get; set; }
    public int? ID { get; set; }
    public int? GenericTitleID { get; set; }
    public string? Icon { get; set; }
}

