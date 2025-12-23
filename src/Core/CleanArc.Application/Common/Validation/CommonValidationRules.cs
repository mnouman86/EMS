using CleanArc.Application.Contracts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Common.Validation
{
    public static class CommonValidationRules
    {
        public static IRuleBuilderOptions<T, string> ValidName<T>(this IRuleBuilder<T, string> ruleBuilder, string fieldName)
        {
            return ruleBuilder
        .NotEmpty().WithMessage($"{fieldName} is required")
        .Length(2, 100).WithMessage($"{fieldName} must be between 2 and 50 characters")
        .Matches(@"^(?i)(Mr\.|Mrs\.|Ms\.|Miss|Dr\.|Prof\.|Engr\.|Hafiz|Mufti|Allama|Shaikh)?\.?\s*((([A-Z]\.)+|[\p{L}\p{M}]+)([\p{Zs}\p{Pd}'’\.]?))*\s*(Jr\.|Sr\.|I{2,3}|IV|V)?$")
            .WithMessage($"{fieldName} contains invalid characters or format")
                .Must(name => char.IsUpper(name[0]))
                .WithMessage($"{fieldName} must start with a capital letter");
        }

        public static IRuleBuilderOptions<T, string> ValidEmail<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Email is required")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters")
                .EmailAddress().WithMessage("Please enter a valid email address")
                .Must(email => email.Split('@').Length == 2 && email.Split('@')[1].Contains('.'))
                .WithMessage("Email domain must contain a period");
        }
        public static IRuleBuilderOptions<T, string> NotDisposableEmail<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        IEmailDomainValidator emailDomainValidator)
        {
            return ruleBuilder.Must(email =>
                    !string.IsNullOrWhiteSpace(email) &&
                    !emailDomainValidator.IsDisposable(email))
                .WithMessage("Disposable email addresses are not allowed.");
        }

        public static IRuleBuilderOptions<T, string> ValidPassword<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                .MaximumLength(100).WithMessage("Password cannot exceed 100 characters")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one number")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");
        }
        //8–100 chars, at least 1 uppercase, 1 lowercase, 1 number & 1 special char

    }

}
