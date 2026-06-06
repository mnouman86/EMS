using CleanArc.Application.Common;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Attendance;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface IAttendanceRepository
    {
        Task<ListResponseWrapper<ClassAttendanceGridRow>> GetClassGridAsync(int schoolClassId, int periodYear, int periodMonth);
        Task<ResponseEntity> BulkSaveAsync(int academicYearId, int schoolClassId, int periodYear, int periodMonth, string entriesJson, int? changedBy);
        Task<ListResponseWrapper<StudentAttendancePeriodRow>> GetStudentHistoryAsync(int studentId, int? academicYearId);
        Task<ListResponseWrapper<StudentAttendanceSummaryRow>> GetStudentSummaryAsync(int studentId, int? academicYearId);
    }
}
