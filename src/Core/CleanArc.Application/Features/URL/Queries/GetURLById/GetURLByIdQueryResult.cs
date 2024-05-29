using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.URL.Queries.GetURLById
{
    public record GetURLByIdQueryResult(int Id, string Path, string Title, string Description);

}
