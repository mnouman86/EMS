using CleanArc.Domain.Entities.AmenityMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IAmenityMappingRepository:IRepository<CleanArc.Domain.Entities.AmenityMapping.AmenityMapping>
{
}
