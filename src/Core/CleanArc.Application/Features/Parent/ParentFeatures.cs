using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using MapsterMapper;
using Mediator;
using System;
using System.Collections.Generic;

namespace CleanArc.Application.Features.Parent
{
    /* ---------- Children linked to a parent (parent dashboard / child switcher / admin view) ---------- */
    public record GetChildrenOfUserQuery(int UserId) : IRequest<OperationResult<List<ParentChildResult>>>;

    public class ParentChildResult
    {
        public int StudentId { get; set; }
        public string StudentCode { get; set; }
        public string FormNo { get; set; }
        public string FullName { get; set; }
        public int? ClassId { get; set; }
        public string ClassName { get; set; }
        public string Status { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Relationship { get; set; }
    }

    internal class GetChildrenOfUserQueryHandler : IRequestHandler<GetChildrenOfUserQuery, OperationResult<List<ParentChildResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetChildrenOfUserQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<ParentChildResult>>> Handle(GetChildrenOfUserQuery r, CancellationToken ct)
        {
            var res = await _u.ParentRepository.GetChildrenAsync(r.UserId);
            if (res.Code != 200) return OperationResult<List<ParentChildResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<ParentChildResult>>.SuccessResult(_m.Map<List<ParentChildResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }

    /* ---------- Ownership check ---------- */
    public record IsParentOfStudentQuery(int UserId, int StudentId) : IRequest<OperationResult<bool>>;

    internal class IsParentOfStudentQueryHandler : IRequestHandler<IsParentOfStudentQuery, OperationResult<bool>>
    {
        private readonly IUnitOfWork _u;
        public IsParentOfStudentQueryHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<bool>> Handle(IsParentOfStudentQuery r, CancellationToken ct)
        {
            var linked = await _u.ParentRepository.IsLinkedAsync(r.UserId, r.StudentId);
            return OperationResult<bool>.SuccessResult(linked);
        }
    }

    /* ---------- A child's payments (for receipt downloads) ---------- */
    public record GetStudentPaymentsQuery(int StudentId) : IRequest<OperationResult<List<StudentPaymentResult>>>;

    public class StudentPaymentResult
    {
        public int PaymentId { get; set; }
        public string ReceiptNo { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMode { get; set; }
        public string ReferenceNo { get; set; }
    }

    internal class GetStudentPaymentsQueryHandler : IRequestHandler<GetStudentPaymentsQuery, OperationResult<List<StudentPaymentResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetStudentPaymentsQueryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<StudentPaymentResult>>> Handle(GetStudentPaymentsQuery r, CancellationToken ct)
        {
            var res = await _u.ParentRepository.GetStudentPaymentsAsync(r.StudentId);
            if (res.Code != 200) return OperationResult<List<StudentPaymentResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<StudentPaymentResult>>.SuccessResult(_m.Map<List<StudentPaymentResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }

    /* ---------- Link / unlink (admin) ---------- */
    public record LinkChildCommand(int ParentUserId, int StudentId, string Relationship) : IRequest<OperationResult<ResponseEntity>>
    {
        public int ChangedBy { get; set; }
    }

    internal class LinkChildCommandHandler : IRequestHandler<LinkChildCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u;
        public LinkChildCommandHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(LinkChildCommand r, CancellationToken ct)
        {
            var res = await _u.ParentRepository.LinkAsync(r.ParentUserId, r.StudentId, r.Relationship, r.ChangedBy);
            if (res != null && res.Code != 200) return OperationResult<ResponseEntity>.FailureResult(res.Message, res.Code);
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    public record UnlinkChildCommand(int ParentUserId, int StudentId) : IRequest<OperationResult<ResponseEntity>>;

    internal class UnlinkChildCommandHandler : IRequestHandler<UnlinkChildCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u;
        public UnlinkChildCommandHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(UnlinkChildCommand r, CancellationToken ct)
        {
            var res = await _u.ParentRepository.UnlinkAsync(r.ParentUserId, r.StudentId);
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }
}
