using CleanArc.Domain.Entities.AdvertisementPage;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IAdvertisementPageRepository:IRepository<AdvertisementPage>
{
   // Task CreateAdvertisementPage(AdvertisementPage AdvertisementPage);
}
