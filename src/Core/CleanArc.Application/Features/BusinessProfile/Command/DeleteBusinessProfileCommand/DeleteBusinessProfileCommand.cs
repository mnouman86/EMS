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

namespace CleanArc.Application.Features.BusinessProfile.Command.DeleteBusinessProfileCommand
{
    public record  DeleteBusinessProfileCommand(string SelectedIds, int? UpdatedBy) : IRequest<OperationResult<bool>>,
    IValidatableModel<DeleteBusinessProfileCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public IValidator<DeleteBusinessProfileCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteBusinessProfileCommand> validator)
        {
            validator.RuleFor(c => c.SelectedIds)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please select Record");

            return validator;
        }
    }

    //public record class DeleteAgeTypeCommand
    //{
    //}
}
