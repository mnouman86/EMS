using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using MapsterMapper;
using Mediator;
using System;
using System.Collections.Generic;

namespace CleanArc.Application.Features.StaffAttendance
{
    /* ---------- Result DTOs (compact — 6 fields) ---------- */

    public class StaffAttendanceResult
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public string? Remarks { get; set; }
        public bool IsBackdated { get; set; }
    }

    public class StaffAttendanceOverviewResult : StaffAttendanceResult
    {
        public string? UserEmail { get; set; }
        public string? StaffName { get; set; }
        public string? RoleName { get; set; }
    }

    /* ---------- Today ---------- */
    public record GetMyStaffAttendanceTodayQuery : IRequest<OperationResult<StaffAttendanceResult?>>
    {
        public int CallerUserId { get; set; }
    }

    internal class GetMyTodayHandler : IRequestHandler<GetMyStaffAttendanceTodayQuery, OperationResult<StaffAttendanceResult?>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetMyTodayHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<StaffAttendanceResult?>> Handle(GetMyStaffAttendanceTodayQuery r, CancellationToken ct)
        {
            var res = await _u.StaffAttendanceRepository.GetMyTodayAsync(r.CallerUserId);
            if (res.Code != 200) return OperationResult<StaffAttendanceResult?>.FailureResult(res.Message, res.Code);
            var mapped = _m.Map<List<StaffAttendanceResult>>(res.Data);
            return OperationResult<StaffAttendanceResult?>.SuccessResult(mapped.FirstOrDefault(), res.Code, res.Message);
        }
    }

    /* ---------- Check-in ---------- */
    public record StaffCheckInCommand(DateTime? CheckInTime, string? Remarks, bool IsBackdated)
        : IRequest<OperationResult<ResponseEntity>>
    {
        public int CallerUserId { get; set; }
    }

    internal class StaffCheckInHandler : IRequestHandler<StaffCheckInCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u;
        public StaffCheckInHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(StaffCheckInCommand r, CancellationToken ct)
        {
            var res = await _u.StaffAttendanceRepository.CheckInAsync(r.CallerUserId, r.CheckInTime, r.Remarks, r.IsBackdated);
            return res != null && res.Code != 200
                ? OperationResult<ResponseEntity>.FailureResult(res.Message, res.Code)
                : OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* ---------- Check-out ---------- */
    public record StaffCheckOutCommand(DateTime? CheckOutTime, string? Remarks, bool IsBackdated)
        : IRequest<OperationResult<ResponseEntity>>
    {
        public int CallerUserId { get; set; }
    }

    internal class StaffCheckOutHandler : IRequestHandler<StaffCheckOutCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u;
        public StaffCheckOutHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(StaffCheckOutCommand r, CancellationToken ct)
        {
            var res = await _u.StaffAttendanceRepository.CheckOutAsync(r.CallerUserId, r.CheckOutTime, r.Remarks, r.IsBackdated);
            return res != null && res.Code != 200
                ? OperationResult<ResponseEntity>.FailureResult(res.Message, res.Code)
                : OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* ---------- My history ---------- */
    public record GetMyStaffAttendanceHistoryQuery(DateTime FromDate, DateTime ToDate)
        : IRequest<OperationResult<List<StaffAttendanceResult>>>
    {
        public int CallerUserId { get; set; }
    }

    internal class GetMyHistoryHandler : IRequestHandler<GetMyStaffAttendanceHistoryQuery, OperationResult<List<StaffAttendanceResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetMyHistoryHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<StaffAttendanceResult>>> Handle(GetMyStaffAttendanceHistoryQuery r, CancellationToken ct)
        {
            var res = await _u.StaffAttendanceRepository.GetHistoryAsync(r.CallerUserId, r.FromDate, r.ToDate);
            if (res.Code != 200) return OperationResult<List<StaffAttendanceResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<StaffAttendanceResult>>.SuccessResult(
                _m.Map<List<StaffAttendanceResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }

    /* ---------- Overview (admin/principal) ---------- */
    public record GetStaffAttendanceOverviewQuery(DateTime FromDate, DateTime ToDate, int? UserId)
        : IRequest<OperationResult<List<StaffAttendanceOverviewResult>>>;

    internal class GetOverviewHandler : IRequestHandler<GetStaffAttendanceOverviewQuery, OperationResult<List<StaffAttendanceOverviewResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetOverviewHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<StaffAttendanceOverviewResult>>> Handle(GetStaffAttendanceOverviewQuery r, CancellationToken ct)
        {
            var res = await _u.StaffAttendanceRepository.GetOverviewAsync(r.FromDate, r.ToDate, r.UserId);
            if (res.Code != 200) return OperationResult<List<StaffAttendanceOverviewResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<StaffAttendanceOverviewResult>>.SuccessResult(
                _m.Map<List<StaffAttendanceOverviewResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }
}
