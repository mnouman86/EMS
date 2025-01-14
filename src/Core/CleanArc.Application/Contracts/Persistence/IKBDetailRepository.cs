//using CleanArc.Application.Features.KBDetail.Queries.GetKBDetailByIdAll;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.KBDetail;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IKBDetailRepository:IRepository<KBDetail>
{
    Task<IReadOnlyList<KBMinimalDetail>> GetKBMinimalViewAsync(SearchRequest searchRequest);
    Task<IReadOnlyList<CoreAreas>> GetKBCoreAreasMinimalViewAsync(SearchRequest searchRequest);
    Task<KnowledgeBaseByID> GetByIdAllAsync(long id);
   // Task CreateAgeType(AgeType ageType);
}
