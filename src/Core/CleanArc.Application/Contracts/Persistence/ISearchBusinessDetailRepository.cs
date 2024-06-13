using CleanArc.Domain.Entities.AgeType;
using CleanArc.Domain.Entities.SearchBusinessDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface ISearchBusinessDetailRepository : IRepository<SearchBusinessDetail>
    {
        // Task CreateAgeType(AgeType ageType);
    }
}
