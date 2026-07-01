using CleanArc.Application.Common;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.StaffAttendance;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface IStaffAttendanceRepository
    {
        Task<ListResponseWrapper<StaffAttendanceRow>> GetMyTodayAsync(int userId);
        Task<ResponseEntity> CheckInAsync(int userId, System.DateTime? checkInTime, string? remarks, bool isBackdated);
        Task<ResponseEntity> CheckOutAsync(int userId, System.DateTime? checkOutTime, string? remarks, bool isBackdated);
        Task<ListResponseWrapper<StaffAttendanceRow>> GetHistoryAsync(int userId, System.DateTime fromDate, System.DateTime toDate);
        Task<ListResponseWrapper<StaffAttendanceOverviewRow>> GetOverviewAsync(System.DateTime fromDate, System.DateTime toDate, int? userId);
    }
}
