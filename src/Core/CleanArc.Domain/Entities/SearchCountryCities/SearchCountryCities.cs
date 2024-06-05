using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.SearchCountryCities;

public class SearchCountryCities
{
    public bool? IsMain { get; set; }
    public string? ImageTitle { get; set; }
    public string? ImagePath { get; set; }
    public string? Description { get; set; }
    public string? CountryName { get; set; }
    public string? CityName { get; set; }
}
