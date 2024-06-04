using CleanArc.Domain.Entities.AgeType;
using CleanArc.Domain.Entities.SearchHotelAmenities;
using CleanArc.Domain.Entities.SearchHotelDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface ISearchHotelAmenitiesRepository : IRepository<SearchHotelAmenities>
    {
        // Task CreateAgeType(AgeType ageType);
    }
}
