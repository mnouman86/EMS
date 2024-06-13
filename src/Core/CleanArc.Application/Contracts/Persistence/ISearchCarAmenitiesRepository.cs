using CleanArc.Domain.Entities.AgeType;
using CleanArc.Domain.Entities.SearchCarAmenities;
using CleanArc.Domain.Entities.SearchHotelDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface ISearchCarAmenitiesRepository : IRepository<SearchCarAmenities>
    {
        // Task CreateAgeType(AgeType ageType);
    }
}
