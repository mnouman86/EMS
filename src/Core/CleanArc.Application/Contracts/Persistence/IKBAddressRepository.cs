//using CleanArc.Application.Features.KBDetail.Queries.GetKBDetailByIdAll;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.KBAddress;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IKBAddressRepository : IRepository<KBAddress>
{
    
}
