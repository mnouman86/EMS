using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.UserExperience.Queries.GetUserExperienceById
{
    public class GetUserExperienceByIdQuery:IRequest<OperationResult<GetUserExperienceByIdQueryResult>>
    {
                public int Id { get; set; }

}
}
