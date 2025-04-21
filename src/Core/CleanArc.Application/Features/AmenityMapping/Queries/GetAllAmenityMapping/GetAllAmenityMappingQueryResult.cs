using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.AmenityMapping.Queries.GetAllAmenityMapping;

public class GetAllAmenityMappingQueryResult
{
    public int Id { get; set; }
    public string? Amenity { get; set; }
    public string? Icon { get; set; }
    //public string? ServiceName { get; set; }
    public int? GenericTitleId { get; set; }
    public bool? Selected { get; set; }
    public int? ServiceTypeEnumId { get; set; }
    public string? ServiceType { get; set; }
}
