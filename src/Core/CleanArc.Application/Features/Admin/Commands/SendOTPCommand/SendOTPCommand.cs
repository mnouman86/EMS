using CleanArc.Application.Models.Common;
using CleanArc.Domain.Enums;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Admin.Commands.SendOTPCommand
{
    public record SendOTPCommand(string UserName, string Password, OTPDeliveryMethod Method) : IRequest<OperationResult<bool>>;
}
