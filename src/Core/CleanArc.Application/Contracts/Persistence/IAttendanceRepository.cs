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

        Task<ListResponseWrapper<DailyAttendanceGridRow>> GetDailyGridAsync(int schoolClassId, DateTime attendanceDate);
        Task<ResponseEntity> BulkSaveDailyAsync(int academicYearId, int schoolClassId, DateTime attendanceDate, string entriesJson, int? changedBy);
        Task<ListResponseWrapper<StudentDailyAttendanceRow>> GetStudentDailyHistoryAsync(int studentId, DateTime? fromDate, DateTime? toDate);
    }
}
