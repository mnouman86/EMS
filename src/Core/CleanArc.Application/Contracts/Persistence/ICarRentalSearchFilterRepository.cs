using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.CarRentalSearchFilter;
using CleanArc.Domain.Entities.SearchHotelDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public interface ICarRentalSearchFilterRepository
{
    Task<IReadOnlyList<CarRentalSearchFilter>> GetAllWithParamAsync(CarRentalSearchFilterRequest request);

}
