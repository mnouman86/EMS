using CleanArc.Domain.Entities.SearchAutoComplete;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArc.Application.Models.Request;
using CleanArc.Application.Common;

namespace CleanArc.Application.Contracts.Persistence;

public  interface ISearchAutoCompleteRepository
{
    Task<ListResponseWrapper<SearchAutoComplete>> GetSearchAutoCompleteAsync(SearchRequestById searchRequestById);
}
