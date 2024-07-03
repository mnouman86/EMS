using CleanArc.Domain.Entities.AdvertisementPlace;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IAdvertisementPlaceRepository:IRepository<AdvertisementPlace>
{
   // Task CreateAdvertisementPlace(AdvertisementPlace AdvertisementPlace);
}
