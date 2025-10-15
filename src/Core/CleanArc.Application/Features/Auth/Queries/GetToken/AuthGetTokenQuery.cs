using CleanArc.Application.Common.Validation;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Jwt;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;

namespace CleanArc.Application.Features.Auth.Queries.GetToken;

public record AuthGetTokenQuery(
        string GrantType,
        string ClientId,
        string ClientSecret) : IRequest<AuthTokenResponse>;
