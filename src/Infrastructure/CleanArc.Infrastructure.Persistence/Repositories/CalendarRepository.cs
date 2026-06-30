using CleanArc.Application.Common;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Calendar;
using CleanArc.Infrastructure.Sql.SqlQueries;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace CleanArc.Infrastructure.Persistence.Repositories;

public class CalendarRepository : ICalendarRepository
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<CalendarRepository> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CalendarRepository(IConfiguration configuration, ILogger<CalendarRepository> logger, IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    private SqlConnection OpenConnection()
    {
        var conn = new SqlConnection(_configuration.GetConnectionString("DBConnection1"));
        conn.Open();
        return conn;
    }

    private async Task<ListResponseWrapper<T>> ListWithOutputs<T>(string sp, DynamicParameters parameters)
    {
        using var connection = OpenConnection();
        parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
        var rows = (await connection.QueryAsync<T>(sp, parameters, commandType: CommandType.StoredProcedure)).ToList();
        return new ListResponseWrapper<T>
        {
            Data = rows,
            TotalCount = rows.Count,
            Code = parameters.Get<int?>("@Code") ?? 0,
            Message = parameters.Get<string>("@Message") ?? string.Empty
        };
    }

    public Task<ListResponseWrapper<SchoolHolidayRow>> GetHolidaysAsync(DateTime? fromDate, DateTime? toDate)
    {
        var p = new DynamicParameters();
        p.Add("@FromDate", fromDate, DbType.Date);
        p.Add("@ToDate", toDate, DbType.Date);
        return ListWithOutputs<SchoolHolidayRow>(CalendarQueries.Get_SchoolHolidays, p);
    }

    public Task<ResponseEntity> UpsertHolidayAsync(DateTime holidayDate, string description, int? changedBy)
    {
        var p = new DynamicParameters();
        p.Add("@HolidayDate", holidayDate, DbType.Date);
        p.Add("@Description", description);
        p.Add("@ChangedBy", changedBy, DbType.Int32);
        using var conn = OpenConnection();
        return conn.QueryFirstOrDefaultAsync<ResponseEntity>(CalendarQueries.Upsert_SchoolHoliday, p, commandType: CommandType.StoredProcedure);
    }

    public Task<ResponseEntity> DeleteHolidayAsync(int id)
    {
        var p = new DynamicParameters();
        p.Add("@Id", id, DbType.Int32);
        using var conn = OpenConnection();
        return conn.QueryFirstOrDefaultAsync<ResponseEntity>(CalendarQueries.Delete_SchoolHoliday, p, commandType: CommandType.StoredProcedure);
    }

    public async Task<CalendarConfigRow?> GetConfigAsync()
    {
        var res = await ListWithOutputs<CalendarConfigRow>(CalendarQueries.Get_CalendarConfig, new DynamicParameters());
        return res.Data.FirstOrDefault();
    }

    public Task<ResponseEntity> SetConfigAsync(string weekendDays, int? changedBy)
    {
        var p = new DynamicParameters();
        p.Add("@WeekendDays", weekendDays);
        p.Add("@ChangedBy", changedBy, DbType.Int32);
        using var conn = OpenConnection();
        return conn.QueryFirstOrDefaultAsync<ResponseEntity>(CalendarQueries.Set_CalendarConfig, p, commandType: CommandType.StoredProcedure);
    }

    public Task<ListResponseWrapper<NonWorkingDateRow>> GetNonWorkingDatesAsync(DateTime fromDate, DateTime toDate)
    {
        var p = new DynamicParameters();
        p.Add("@FromDate", fromDate, DbType.Date);
        p.Add("@ToDate", toDate, DbType.Date);
        return ListWithOutputs<NonWorkingDateRow>(CalendarQueries.Get_NonWorkingDates, p);
    }
}
