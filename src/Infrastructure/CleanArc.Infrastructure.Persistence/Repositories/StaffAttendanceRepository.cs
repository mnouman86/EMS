using CleanArc.Application.Common;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.StaffAttendance;
using CleanArc.Infrastructure.Sql.SqlQueries;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace CleanArc.Infrastructure.Persistence.Repositories;

public class StaffAttendanceRepository : IStaffAttendanceRepository
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<StaffAttendanceRepository> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public StaffAttendanceRepository(IConfiguration configuration, ILogger<StaffAttendanceRepository> logger, IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration; _logger = logger; _httpContextAccessor = httpContextAccessor;
    }

    private SqlConnection OpenConnection()
    {
        var conn = new SqlConnection(_configuration.GetConnectionString("DBConnection1"));
        conn.Open();
        return conn;
    }

    private async Task<ListResponseWrapper<T>> ListWithOutputs<T>(string sp, DynamicParameters parameters)
    {
        using var conn = OpenConnection();
        parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
        var rows = (await conn.QueryAsync<T>(sp, parameters, commandType: CommandType.StoredProcedure)).ToList();
        return new ListResponseWrapper<T>
        {
            Data = rows,
            TotalCount = rows.Count,
            Code = parameters.Get<int?>("@Code") ?? 0,
            Message = parameters.Get<string>("@Message") ?? string.Empty
        };
    }

    public Task<ListResponseWrapper<StaffAttendanceRow>> GetMyTodayAsync(int userId)
    {
        var p = new DynamicParameters();
        p.Add("@UserId", userId, DbType.Int32);
        return ListWithOutputs<StaffAttendanceRow>(StaffAttendanceQueries.Get_MyToday, p);
    }

    public Task<ResponseEntity> CheckInAsync(int userId, System.DateTime? checkInTime, string? remarks, bool isBackdated)
    {
        var p = new DynamicParameters();
        p.Add("@UserId", userId, DbType.Int32);
        p.Add("@CheckInTime", checkInTime, DbType.DateTime2);
        p.Add("@Remarks", remarks, DbType.String, size: 500);
        p.Add("@IsBackdated", isBackdated, DbType.Boolean);
        using var conn = OpenConnection();
        return conn.QueryFirstOrDefaultAsync<ResponseEntity>(StaffAttendanceQueries.Record_CheckIn, p, commandType: CommandType.StoredProcedure);
    }

    public Task<ResponseEntity> CheckOutAsync(int userId, System.DateTime? checkOutTime, string? remarks, bool isBackdated)
    {
        var p = new DynamicParameters();
        p.Add("@UserId", userId, DbType.Int32);
        p.Add("@CheckOutTime", checkOutTime, DbType.DateTime2);
        p.Add("@Remarks", remarks, DbType.String, size: 500);
        p.Add("@IsBackdated", isBackdated, DbType.Boolean);
        using var conn = OpenConnection();
        return conn.QueryFirstOrDefaultAsync<ResponseEntity>(StaffAttendanceQueries.Record_CheckOut, p, commandType: CommandType.StoredProcedure);
    }

    public Task<ListResponseWrapper<StaffAttendanceRow>> GetHistoryAsync(int userId, System.DateTime fromDate, System.DateTime toDate)
    {
        var p = new DynamicParameters();
        p.Add("@UserId", userId, DbType.Int32);
        p.Add("@FromDate", fromDate, DbType.Date);
        p.Add("@ToDate", toDate, DbType.Date);
        return ListWithOutputs<StaffAttendanceRow>(StaffAttendanceQueries.Get_History, p);
    }

    public Task<ListResponseWrapper<StaffAttendanceOverviewRow>> GetOverviewAsync(System.DateTime fromDate, System.DateTime toDate, int? userId)
    {
        var p = new DynamicParameters();
        p.Add("@FromDate", fromDate, DbType.Date);
        p.Add("@ToDate", toDate, DbType.Date);
        p.Add("@UserId", userId, DbType.Int32);
        return ListWithOutputs<StaffAttendanceOverviewRow>(StaffAttendanceQueries.Get_Overview, p);
    }
}
