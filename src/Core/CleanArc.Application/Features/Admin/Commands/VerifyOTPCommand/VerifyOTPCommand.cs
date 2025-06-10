using CleanArc.Application.Models.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArc.Application.Models.Jwt;


namespace CleanArc.Application.Features.Admin.Commands.VerifyOTPCommand
{
    public record VerifyOTPCommand(int UserId, string Code) : IRequest<OperationResult<AccessToken>>;
}
