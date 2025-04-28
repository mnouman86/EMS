using CleanArc.Domain.Entities.RoomSizeUnit;
using CleanArc.Domain.Entities.Service;
using CleanArc.Domain.Entities.VehicleType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface IVehicleTypeRepository : IRepository<VehicleType>
    {
    }
}
