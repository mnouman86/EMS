using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchCountryCities.Queries.GetAllSearchCountryCities;

//public  record GetAllSearchCountryCitiesQueriesResult(string CityName, string CountryName, string Description,string ImagePath, string ImageTitle, bool IsMain)
public class GetAllSearchCountryCitiesQueriesResult
{
    public string CityName { get; set; }
    public string CountryName { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
    public string ImageTitle { get; set; }
    public bool IsMain { get; set; }
}