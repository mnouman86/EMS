using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Fee;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Fee.Queries.ParentFeeSearch
{
    /* FEE-13: Parent self-service search (anonymous, rate-limited at SP layer). */
    public record ParentFeeSearchQuery(string? StudentCode, string? SecondFactor)
        : IRequest<OperationResult<ParentFeeSummaryResult>>, IValidatableModel<ParentFeeSearchQuery>
    {
        [JsonIgnore] public string? ClientIp { get; set; }

        public IValidator<ParentFeeSearchQuery> ValidateApplicationModel(ApplicationBaseValidationModelProvider<ParentFeeSearchQuery> v)
        {
            v.RuleFor(c => c.StudentCode).NotEmpty();
            v.RuleFor(c => c.SecondFactor).NotEmpty();
            return v;
        }
    }

    public class ParentFeeSummaryResult
    {
        public int StudentId { get; set; }
        public string StudentCode { get; set; }
        public string StudentFullName { get; set; }
        public string CurrentMonthStatus { get; set; }
        public decimal TotalOutstanding { get; set; }
        public DateTime? LastPaymentDate { get; set; }
    }

    internal class ParentFeeSearchQueryHandler : IRequestHandler<ParentFeeSearchQuery, OperationResult<ParentFeeSummaryResult>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m; private readonly IHttpContextAccessor _http;
        public ParentFeeSearchQueryHandler(IUnitOfWork u, IMapper m, IHttpContextAccessor http) { _u = u; _m = m; _http = http; }

        public async ValueTask<OperationResult<ParentFeeSummaryResult>> Handle(ParentFeeSearchQuery r, CancellationToken ct)
        {
            var ip = r.ClientIp ?? _http?.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "unknown";
            var res = await _u.FeeRepository.ParentSearchAsync(new ParentFeeSearchDTO
            {
                StudentCode = r.StudentCode, SecondFactor = r.SecondFactor, ClientIp = ip
            });
            // Generic failure response to avoid data leakage (mirrors RES-07)
            if (res.Code != 200 || res.Data == null)
                return OperationResult<ParentFeeSummaryResult>.FailureResult(res.Message ?? "No fee record found", res.Code == 0 ? 404 : res.Code);
            return OperationResult<ParentFeeSummaryResult>.SuccessResult(_m.Map<ParentFeeSummaryResult>(res.Data));
        }
    }
}
