using CleanArc.Domain.Entities.Advertisement;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IAdvertisementRepository:IRepository<Advertisement>
{
   // Task CreateAdvertisement(Advertisement Advertisement);
}
