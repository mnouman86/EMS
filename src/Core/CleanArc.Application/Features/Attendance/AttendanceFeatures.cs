using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using MapsterMapper;
using Mediator;
using System.Text.Json;

namespace CleanArc.Application.Features.Attendance
{
    /* ---------- Result DTOs ---------- */

    public class ClassAttendanceGridRowResult
    {
        public int StudentId { get; set; }
        public string StudentCode { get; set; }
        public string FormNo { get; set; }
        public string FullName { get; set; }
        public string StudentStatus { get; set; }
        public int? AttendanceId { get; set; }
        public int WorkingDays { get; set; }
        public int PresentDays { get; set; }
        public string Remarks { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class StudentAttendancePeriodResult
    {
        public int Id { get; set; }
        public int PeriodYear { get; set; }
        public int PeriodMonth { get; set; }
        public int WorkingDays { get; set; }
        public int PresentDays { get; set; }
        public int AbsentDays { get; set; }
        public decimal? AttendancePercent { get; set; }
        public string Remarks { get; set; }
        public string ClassName { get; set; }
        public string AcademicYear { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class StudentAttendanceSummaryResult
    {
        public int StudentId { get; set; }
        public int TotalWorkingDays { get; set; }
        public int TotalPresentDays { get; set; }
        public int TotalAbsentDays { get; set; }
        public int PeriodsRecorded { get; set; }
        public decimal? OverallPercent { get; set; }
    }

    /* ---------- Class entry grid ---------- */
    public record GetClassAttendanceGridQuery(int SchoolClassId, int PeriodYear, int PeriodMonth)
        : IRequest<OperationResult<List<ClassAttendanceGridRowResult>>>;

    internal class GetClassAttendanceGridHandler : IRequestHandler<GetClassAttendanceGridQuery, OperationResult<List<ClassAttendanceGridRowResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m; private readonly ITeacherScopeContext _scope;
        public GetClassAttendanceGridHandler(IUnitOfWork u, IMapper m, ITeacherScopeContext scope) { _u = u; _m = m; _scope = scope; }
        public async ValueTask<OperationResult<List<ClassAttendanceGridRowResult>>> Handle(GetClassAttendanceGridQuery r, CancellationToken ct)
        {
            if (_scope.IsTeacherScoped)
            {
                var classIds = await _scope.GetClassScopeAsync();
                if (!classIds.Contains(r.SchoolClassId))
                    return OperationResult<List<ClassAttendanceGridRowResult>>.FailureResult("Not authorized for this class.", 403);
            }

            var res = await _u.AttendanceRepository.GetClassGridAsync(r.SchoolClassId, r.PeriodYear, r.PeriodMonth);
            if (res.Code != 200) return OperationResult<List<ClassAttendanceGridRowResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<ClassAttendanceGridRowResult>>.SuccessResult(
                _m.Map<List<ClassAttendanceGridRowResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }

    /* ---------- Bulk save ---------- */
    public class AttendanceEntryInput
    {
        public int StudentId { get; set; }
        public int WorkingDays { get; set; }
        public int PresentDays { get; set; }
        public string Remarks { get; set; }
    }

    public record BulkSaveClassAttendanceCommand(
        int AcademicYearId, int SchoolClassId, int PeriodYear, int PeriodMonth,
        List<AttendanceEntryInput> Entries) : IRequest<OperationResult<ResponseEntity>>
    {
        public int ChangedBy { get; set; }
    }

    internal class BulkSaveClassAttendanceHandler : IRequestHandler<BulkSaveClassAttendanceCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly ITeacherScopeContext _scope;
        public BulkSaveClassAttendanceHandler(IUnitOfWork u, ITeacherScopeContext scope) { _u = u; _scope = scope; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(BulkSaveClassAttendanceCommand r, CancellationToken ct)
        {
            if (r.Entries == null || r.Entries.Count == 0)
                return OperationResult<ResponseEntity>.FailureResult("No attendance rows provided.", 400);

            if (_scope.IsTeacherScoped)
            {
                var classIds = await _scope.GetClassScopeAsync();
                if (!classIds.Contains(r.SchoolClassId))
                    return OperationResult<ResponseEntity>.FailureResult("Not authorized to write attendance for this class.", 403);
            }

            // Camel-case JSON because the SP's OPENJSON paths are '$.studentId', etc.
            var json = JsonSerializer.Serialize(r.Entries, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            var res = await _u.AttendanceRepository.BulkSaveAsync(
                r.AcademicYearId, r.SchoolClassId, r.PeriodYear, r.PeriodMonth, json, r.ChangedBy);

            if (res != null && res.Code != 200)
                return OperationResult<ResponseEntity>.FailureResult(res.Message, res.Code);
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* ---------- Student period history ---------- */
    public record GetStudentAttendanceHistoryQuery(int StudentId, int? AcademicYearId)
        : IRequest<OperationResult<List<StudentAttendancePeriodResult>>>;

    internal class GetStudentAttendanceHistoryHandler : IRequestHandler<GetStudentAttendanceHistoryQuery, OperationResult<List<StudentAttendancePeriodResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m; private readonly ITeacherScopeContext _scope;
        public GetStudentAttendanceHistoryHandler(IUnitOfWork u, IMapper m, ITeacherScopeContext scope) { _u = u; _m = m; _scope = scope; }
        public async ValueTask<OperationResult<List<StudentAttendancePeriodResult>>> Handle(GetStudentAttendanceHistoryQuery r, CancellationToken ct)
        {
            if (_scope.IsTeacherScoped && !await _scope.OwnsStudentAsync(r.StudentId))
                return OperationResult<List<StudentAttendancePeriodResult>>.FailureResult("Not authorized for this student.", 403);

            var res = await _u.AttendanceRepository.GetStudentHistoryAsync(r.StudentId, r.AcademicYearId);
            if (res.Code != 200) return OperationResult<List<StudentAttendancePeriodResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<StudentAttendancePeriodResult>>.SuccessResult(
                _m.Map<List<StudentAttendancePeriodResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }

    /* ---------- Student summary (totals) ---------- */
    public record GetStudentAttendanceSummaryQuery(int StudentId, int? AcademicYearId)
        : IRequest<OperationResult<StudentAttendanceSummaryResult>>;

    internal class GetStudentAttendanceSummaryHandler : IRequestHandler<GetStudentAttendanceSummaryQuery, OperationResult<StudentAttendanceSummaryResult>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m; private readonly ITeacherScopeContext _scope;
        public GetStudentAttendanceSummaryHandler(IUnitOfWork u, IMapper m, ITeacherScopeContext scope) { _u = u; _m = m; _scope = scope; }
        public async ValueTask<OperationResult<StudentAttendanceSummaryResult>> Handle(GetStudentAttendanceSummaryQuery r, CancellationToken ct)
        {
            if (_scope.IsTeacherScoped && !await _scope.OwnsStudentAsync(r.StudentId))
                return OperationResult<StudentAttendanceSummaryResult>.FailureResult("Not authorized for this student.", 403);

            var res = await _u.AttendanceRepository.GetStudentSummaryAsync(r.StudentId, r.AcademicYearId);
            if (res.Code != 200) return OperationResult<StudentAttendanceSummaryResult>.FailureResult(res.Message, res.Code);
            var first = _m.Map<List<StudentAttendanceSummaryResult>>(res.Data).FirstOrDefault()
                        ?? new StudentAttendanceSummaryResult { StudentId = r.StudentId };
            return OperationResult<StudentAttendanceSummaryResult>.SuccessResult(first, res.Code, res.Message);
        }
    }
}
