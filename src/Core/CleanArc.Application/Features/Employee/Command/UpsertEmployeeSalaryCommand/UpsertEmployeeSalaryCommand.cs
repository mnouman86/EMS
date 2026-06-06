using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Employee;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Employee.Command.UpsertEmployeeSalaryCommand
{
    public record UpsertEmployeeSalaryCommand(
        int EmployeeId, decimal BasicSalary, decimal Allowances, decimal FixedDeductions,
        string? AllowanceBreakdownJson, DateTime EffectiveFrom)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<UpsertEmployeeSalaryCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<UpsertEmployeeSalaryCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpsertEmployeeSalaryCommand> v)
        {
            v.RuleFor(c => c.EmployeeId).GreaterThan(0);
            v.RuleFor(c => c.BasicSalary).GreaterThanOrEqualTo(0);
            v.RuleFor(c => c.Allowances).GreaterThanOrEqualTo(0);
            v.RuleFor(c => c.FixedDeductions).GreaterThanOrEqualTo(0);
            return v;
        }
    }

    internal class UpsertEmployeeSalaryCommandHandler : IRequestHandler<UpsertEmployeeSalaryCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public UpsertEmployeeSalaryCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(UpsertEmployeeSalaryCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.EmployeeRepository.UpsertSalaryAsync(new UpsertEmployeeSalaryDTO
            {
                EmployeeId = r.EmployeeId,
                BasicSalary = r.BasicSalary,
                Allowances = r.Allowances,
                FixedDeductions = r.FixedDeductions,
                AllowanceBreakdownJson = r.AllowanceBreakdownJson,
                EffectiveFrom = r.EffectiveFrom,
                CreatedBy = user.Id
            });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }
}
