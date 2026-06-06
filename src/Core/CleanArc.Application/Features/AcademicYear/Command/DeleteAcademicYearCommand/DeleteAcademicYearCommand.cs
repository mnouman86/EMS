using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.AcademicYear.Command.DeleteAcademicYearCommand
{
    public record DeleteAcademicYearCommand(DeleteRequest deleteRequest)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<DeleteAcademicYearCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<DeleteAcademicYearCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteAcademicYearCommand> v)
        {
            v.RuleFor(c => c.deleteRequest.SelectedIds).NotEmpty();
            return v;
        }
    }

    internal class DeleteAcademicYearCommandHandler : IRequestHandler<DeleteAcademicYearCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public DeleteAcademicYearCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(DeleteAcademicYearCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.AcademicYearRepository.DeleteAsync(r.deleteRequest, user.Id);
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }
}
