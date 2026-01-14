using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Currency.Queries.GetAllCurrency;

public class GetCurrencyRatesQueryResult
{
    public int Id { get; set; }
    public string CurrencyCode { get; set; }
    public decimal Rate { get; set; }
    public string DisplaySymbol { get; set; }
    public string CountryFlagSymbol { get; set; }

}

//    public GetAllCurrencyQueryResult(int iD, string name, string description, bool isDeleted, bool isActive, int createdBy, DateTime createdAt, int updatedBy, DateTime updatedAt)
//    {
//        ID = iD;
//        Name = name;
//        Description = description;
//        IsDeleted = isDeleted;
//        IsActive = isActive;
//        CreatedBy = createdBy;
//        CreatedAt = createdAt;
//        UpdatedBy = updatedBy;
//        UpdatedAt = updatedAt;
//    }
//}
