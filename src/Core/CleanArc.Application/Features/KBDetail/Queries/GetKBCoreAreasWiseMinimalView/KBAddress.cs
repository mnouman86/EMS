using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.KBDetail.Queries.GetKBCoreAreasWiseMinimalView;

public class KBAddress
{
    public string? AddressID { get; set; }
    public int? CountryLookUpID { get; set; }
    public int? CityLookUpID { get; set; }
    public int? StatelookUpID { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? State { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }

 //  GA.ID as AddressID,
	//GA.AddressLine1, GA.AddressLine2,
	//GA.CityLookUpID,GA.CountryLookUpID,GA.StatelookUpID,
 //   St.[Name] as [State],
 //   Ci.[Name] as [City],
 //   GA.[Latitude],
 //   GA.[Longitude]

}