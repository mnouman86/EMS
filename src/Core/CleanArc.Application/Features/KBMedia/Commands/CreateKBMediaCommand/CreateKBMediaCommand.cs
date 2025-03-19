using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using CleanArc.SharedKernel.ValidationBase;
using FluentValidation;
using CleanArc.Application.Models.Request;using System.Text.Json.Serialization; using CleanArc.Domain.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.KBMedia.Commands.CreateKBMediaCommand;
public record CreateKBMediaCommand(
    int? GenericTitleID,
   // int? KBDescriptionID,
   //string MediaType,
     string ImagePath,
    string ImageTitle,
    bool? IsMain,
   // string? Description,
    int? CreatedBy,
    int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateKBMediaCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateKBMediaCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateKBMediaCommand> validator)
    {
        validator.RuleFor(c => c.GenericTitleID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid  GenericTitleID");
       
        validator.RuleFor(c => c.ImagePath)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a ImagePath");
        validator.RuleFor(c => c.ImageTitle)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a ImageTitle");
        
        return validator;
    }
}

