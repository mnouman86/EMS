using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using CleanArc.SharedKernel.ValidationBase;
using FluentValidation;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArc.Application.Models.Request;using System.Text.Json.Serialization; using CleanArc.Domain.Common;

namespace CleanArc.Application.Features.ProcessOrder.Commands.UpdateProcessOrderCommand;
public record UpdateProcessOrderCommand(int Id, int? OrderStatusEnumId,
    int? CultureId,
     int? UpdatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateProcessOrderCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateProcessOrderCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateProcessOrderCommand> validator)
    {
        validator.RuleFor(c => c.Id)
     .NotEmpty()
     .NotNull()
     .WithMessage("Please enter a valid Id");
        validator.RuleFor(c => c.OrderStatusEnumId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a OrderStatusEnumId");
        


        return validator;
    }
}

