using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Jwt;
using CleanArc.Application.Models.StartupData;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;

namespace CleanArc.Application.Features.Admin.Queries.GetToken;

public record GetStartupDataQuery() : IRequest<OperationResult<StartupDataDto>>
{
};