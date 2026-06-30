using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using MapsterMapper;
using Mediator;

namespace CleanArc.Application.Features.Calendar
{
    public class HolidayResult
    {
        public int Id { get; set; }
        public DateTime HolidayDate { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CalendarConfigResult
    {
        public string WeekendDays { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class NonWorkingDateResult
    {
        public DateTime NonWorkingDate { get; set; }
        public string Reason { get; set; }
    }

    /* ---------- Get holidays in range ---------- */
    public record GetHolidaysQuery(DateTime? FromDate, DateTime? ToDate) : IRequest<OperationResult<List<HolidayResult>>>;
    internal class GetHolidaysHandler : IRequestHandler<GetHolidaysQuery, OperationResult<List<HolidayResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetHolidaysHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<HolidayResult>>> Handle(GetHolidaysQuery r, CancellationToken ct)
        {
            var res = await _u.CalendarRepository.GetHolidaysAsync(r.FromDate, r.ToDate);
            if (res.Code != 200) return OperationResult<List<HolidayResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<HolidayResult>>.SuccessResult(_m.Map<List<HolidayResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }

    /* ---------- Upsert holiday ---------- */
    public record UpsertHolidayCommand(DateTime HolidayDate, string Description) : IRequest<OperationResult<ResponseEntity>>
    {
        public int ChangedBy { get; set; }
    }
    internal class UpsertHolidayHandler : IRequestHandler<UpsertHolidayCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u;
        public UpsertHolidayHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(UpsertHolidayCommand r, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(r.Description))
                return OperationResult<ResponseEntity>.FailureResult("Description is required.", 400);
            var res = await _u.CalendarRepository.UpsertHolidayAsync(r.HolidayDate, r.Description, r.ChangedBy);
            if (res != null && res.Code != 200) return OperationResult<ResponseEntity>.FailureResult(res.Message, res.Code);
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* ---------- Delete holiday ---------- */
    public record DeleteHolidayCommand(int Id) : IRequest<OperationResult<ResponseEntity>>;
    internal class DeleteHolidayHandler : IRequestHandler<DeleteHolidayCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u;
        public DeleteHolidayHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(DeleteHolidayCommand r, CancellationToken ct)
        {
            var res = await _u.CalendarRepository.DeleteHolidayAsync(r.Id);
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* ---------- Get config ---------- */
    public record GetCalendarConfigQuery() : IRequest<OperationResult<CalendarConfigResult>>;
    internal class GetCalendarConfigHandler : IRequestHandler<GetCalendarConfigQuery, OperationResult<CalendarConfigResult>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetCalendarConfigHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<CalendarConfigResult>> Handle(GetCalendarConfigQuery r, CancellationToken ct)
        {
            var row = await _u.CalendarRepository.GetConfigAsync();
            if (row is null)
                return OperationResult<CalendarConfigResult>.SuccessResult(new CalendarConfigResult { WeekendDays = "Saturday,Sunday" });
            return OperationResult<CalendarConfigResult>.SuccessResult(_m.Map<CalendarConfigResult>(row));
        }
    }

    /* ---------- Set config ---------- */
    public record SetCalendarConfigCommand(string WeekendDays) : IRequest<OperationResult<ResponseEntity>>
    {
        public int ChangedBy { get; set; }
    }
    internal class SetCalendarConfigHandler : IRequestHandler<SetCalendarConfigCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u;
        public SetCalendarConfigHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(SetCalendarConfigCommand r, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(r.WeekendDays))
                return OperationResult<ResponseEntity>.FailureResult("Weekend days CSV is required.", 400);
            var res = await _u.CalendarRepository.SetConfigAsync(r.WeekendDays.Trim(), r.ChangedBy);
            if (res != null && res.Code != 200) return OperationResult<ResponseEntity>.FailureResult(res.Message, res.Code);
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* ---------- Get non-working dates (used by daily attendance picker) ---------- */
    public record GetNonWorkingDatesQuery(DateTime FromDate, DateTime ToDate) : IRequest<OperationResult<List<NonWorkingDateResult>>>;
    internal class GetNonWorkingDatesHandler : IRequestHandler<GetNonWorkingDatesQuery, OperationResult<List<NonWorkingDateResult>>>
    {
        private readonly IUnitOfWork _u; private readonly IMapper _m;
        public GetNonWorkingDatesHandler(IUnitOfWork u, IMapper m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<List<NonWorkingDateResult>>> Handle(GetNonWorkingDatesQuery r, CancellationToken ct)
        {
            var res = await _u.CalendarRepository.GetNonWorkingDatesAsync(r.FromDate, r.ToDate);
            if (res.Code != 200) return OperationResult<List<NonWorkingDateResult>>.FailureResult(res.Message, res.Code);
            return OperationResult<List<NonWorkingDateResult>>.SuccessResult(_m.Map<List<NonWorkingDateResult>>(res.Data), res.Code, res.Message, res.TotalCount);
        }
    }
}
