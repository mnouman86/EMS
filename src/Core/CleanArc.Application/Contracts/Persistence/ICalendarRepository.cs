using CleanArc.Application.Common;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Calendar;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface ICalendarRepository
    {
        Task<ListResponseWrapper<SchoolHolidayRow>> GetHolidaysAsync(DateTime? fromDate, DateTime? toDate);
        Task<ResponseEntity> UpsertHolidayAsync(DateTime holidayDate, string description, int? changedBy);
        Task<ResponseEntity> DeleteHolidayAsync(int id);
        Task<CalendarConfigRow?> GetConfigAsync();
        Task<ResponseEntity> SetConfigAsync(string weekendDays, int? changedBy);
        Task<ListResponseWrapper<NonWorkingDateRow>> GetNonWorkingDatesAsync(DateTime fromDate, DateTime toDate);
    }
}
