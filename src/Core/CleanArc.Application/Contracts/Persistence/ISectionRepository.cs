using CleanArc.Domain.Entities.Country;
using CleanArc.Domain.Entities.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface ISectionRepository : IRepository<Section>
    {
    }
}
