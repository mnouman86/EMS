using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.KBTiming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.KBAddress;

public class KBAddress
{
    public int? CultureId { get; set; }
    public int? GenericTitleID { get; set; }
    public int? AddressID { get; set; }
    public int? CountryLookUpID { get; set; }
    public int? CityLookUpID { get; set; }
    public int? StatelookUpID { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? State { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? PostalCode { get; set; }
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
    public string? PhoneNo { get; set; }
    public string? Cost { get; set; }
    public string? Access { get; set; }
    //public string? Availablity { get; set; }
    public List<Availability> Timings { get; set; }
    public string? WhenToVisitIDs { get; set; }
    public string? WhenToVisit { get; set; }

}