using CleanArc.Application.Common;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.CarDetail;
using CleanArc.Domain.Entities.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface ICarDetailRepository:IRepository<CarDetail>
    {

    }
}
