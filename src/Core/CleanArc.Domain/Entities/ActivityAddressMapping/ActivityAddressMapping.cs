using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.ActivityAddressMapping;

public  class ActivityAddressMapping
    
{

    public int ID { get; set; }
    public int? ActivityID { get; set; }
    public int? CountryLookUpID { get; set; }
    public int? CityLookUpID { get; set; }
    public int? StateLookUpID { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? PostalCode { get; set; }
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsDeleted { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? CultureId { get; set; }
    public int? Code { get; set; }
    public string? Message { get; set; }


}


