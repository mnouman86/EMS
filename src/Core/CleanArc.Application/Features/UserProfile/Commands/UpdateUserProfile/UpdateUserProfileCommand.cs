using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArc.Application.Features.UserProfile.Commands.UpdateUserProfile;

public record UpdateUserProfileCommand(
       string Name,
       string FamilyName,
       string? PhoneNumber,
       string? Email,
       int? GenderId,
       int? NationalityId,
       DateTime? DateOfBirth,
       string? Address
   ) : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<UpdateUserProfileCommand>

{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateUserProfileCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateUserProfileCommand> validator)
    {

        validator
            .RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("User must have first name");

        validator.RuleFor(c => c.FamilyName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter your last name");


        validator.RuleFor(c => c.PhoneNumber).NotEmpty()
            .NotNull().WithMessage("Phone Number is required.")
            .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
            .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
            .Matches(new Regex(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")).WithMessage("Phone number is not valid");

        return validator;
    }
}