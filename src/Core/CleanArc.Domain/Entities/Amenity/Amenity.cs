using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.Amenity;

public class Amenity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    //public string? ServiceName { get; set; }
    public int? ServiceTypeEnumId { get; set; }
    public string? AmenityTypeEnum { get; set; }
    public string? Description { get; set; }
    public int? ServiceId { get; set; }
    public string? Icon { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsDeleted { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public int? CultureId { get; set; }

}
public class AmenityLookUp
{
    public int Id { get; set; }
    public string? Amenity { get; set; }
    //public string? ServiceName { get; set; }
    public int? GenericTitleId { get; set; }
    public bool? Selected { get; set; }

}
