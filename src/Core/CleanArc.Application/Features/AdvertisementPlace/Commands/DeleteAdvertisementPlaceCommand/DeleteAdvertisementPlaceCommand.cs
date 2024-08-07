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
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.AdvertisementPlace.Commands.DeleteAdvertisementPlaceCommand;

public record DeleteAdvertisementPlaceCommand(string SelectedIds, int? UpdatedBy, int? CultureId) : IRequest<OperationResult<bool>>,
    IValidatableModel<DeleteAdvertisementPlaceCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<DeleteAdvertisementPlaceCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteAdvertisementPlaceCommand> validator)
    {
        validator.RuleFor(c => c.SelectedIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select Record");
       
        return validator;
    }
}

//public record class DeleteAdvertisementPlaceCommand
//{
//}
