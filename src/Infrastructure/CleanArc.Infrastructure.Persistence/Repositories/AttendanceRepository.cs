using CleanArc.Application.Common;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Attendance;
using CleanArc.Infrastructure.Sql.SqlQueries;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace CleanArc.Infrastructure.Persistence.Repositories;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AttendanceRepository> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AttendanceRepository(IConfiguration configuration, ILogger<AttendanceRepository> logger, IHttpContextAccessor httpContextAccessor)
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

    public Task<ListResponseWrapper<ClassAttendanceGridRow>> GetClassGridAsync(int schoolClassId, int periodYear, int periodMonth)
    {
        var p = new DynamicParameters();
        p.Add("@SchoolClassId", schoolClassId, DbType.Int32);
        p.Add("@PeriodYear", periodYear, DbType.Int32);
        p.Add("@PeriodMonth", periodMonth, DbType.Int32);
        return ListWithOutputs<ClassAttendanceGridRow>(AttendanceQueries.Get_ClassAttendanceGrid, p);
    }

    public Task<ResponseEntity> BulkSaveAsync(int academicYearId, int schoolClassId, int periodYear, int periodMonth, string entriesJson, int? changedBy)
    {
        var p = new DynamicParameters();
        p.Add("@AcademicYearId", academicYearId, DbType.Int32);
        p.Add("@SchoolClassId", schoolClassId, DbType.Int32);
        p.Add("@PeriodYear", periodYear, DbType.Int32);
        p.Add("@PeriodMonth", periodMonth, DbType.Int32);
        p.Add("@Entries", entriesJson, DbType.String, size: -1);
        p.Add("@ChangedBy", changedBy, DbType.Int32);
        using var conn = OpenConnection();
        return conn.QueryFirstOrDefaultAsync<ResponseEntity>(AttendanceQueries.BulkSave_ClassAttendance, p, commandType: CommandType.StoredProcedure);
    }

    public Task<ListResponseWrapper<StudentAttendancePeriodRow>> GetStudentHistoryAsync(int studentId, int? academicYearId)
    {
        var p = new DynamicParameters();
        p.Add("@StudentId", studentId, DbType.Int32);
        p.Add("@AcademicYearId", academicYearId, DbType.Int32);
        return ListWithOutputs<StudentAttendancePeriodRow>(AttendanceQueries.Get_StudentAttendanceHistory, p);
    }

    public Task<ListResponseWrapper<StudentAttendanceSummaryRow>> GetStudentSummaryAsync(int studentId, int? academicYearId)
    {
        var p = new DynamicParameters();
        p.Add("@StudentId", studentId, DbType.Int32);
        p.Add("@AcademicYearId", academicYearId, DbType.Int32);
        return ListWithOutputs<StudentAttendanceSummaryRow>(AttendanceQueries.Get_StudentAttendanceSummary, p);
    }

    /* ---------- Daily attendance ---------- */
    public Task<ListResponseWrapper<DailyAttendanceGridRow>> GetDailyGridAsync(int schoolClassId, DateTime attendanceDate)
    {
        var p = new DynamicParameters();
        p.Add("@SchoolClassId", schoolClassId, DbType.Int32);
        p.Add("@AttendanceDate", attendanceDate.Date, DbType.Date);
        return ListWithOutputs<DailyAttendanceGridRow>(AttendanceQueries.Get_DailyAttendanceGrid, p);
    }

    public Task<ResponseEntity> BulkSaveDailyAsync(int academicYearId, int schoolClassId, DateTime attendanceDate, string entriesJson, int? changedBy)
    {
        var p = new DynamicParameters();
        p.Add("@AcademicYearId", academicYearId, DbType.Int32);
        p.Add("@SchoolClassId", schoolClassId, DbType.Int32);
        p.Add("@AttendanceDate", attendanceDate.Date, DbType.Date);
        p.Add("@Entries", entriesJson, DbType.String, size: -1);
        p.Add("@ChangedBy", changedBy, DbType.Int32);
        using var conn = OpenConnection();
        return conn.QueryFirstOrDefaultAsync<ResponseEntity>(AttendanceQueries.BulkSave_DailyAttendance, p, commandType: CommandType.StoredProcedure);
    }

    public Task<ListResponseWrapper<StudentDailyAttendanceRow>> GetStudentDailyHistoryAsync(int studentId, DateTime? fromDate, DateTime? toDate)
    {
        var p = new DynamicParameters();
        p.Add("@StudentId", studentId, DbType.Int32);
        p.Add("@FromDate", fromDate?.Date, DbType.Date);
        p.Add("@ToDate", toDate?.Date, DbType.Date);
        return ListWithOutputs<StudentDailyAttendanceRow>(AttendanceQueries.Get_StudentDailyHistory, p);
    }

    public Task<ListResponseWrapper<StudentAttendanceBoardRow>> GetStudentSummaryBoardAsync(
        DateTime fromDate, DateTime toDate, string? classIdsCsv, int? classId)
    {
        var p = new DynamicParameters();
        p.Add("@FromDate", fromDate.Date, DbType.Date);
        p.Add("@ToDate", toDate.Date, DbType.Date);
        p.Add("@ClassIdsCsv", classIdsCsv, DbType.String, size: -1);
        p.Add("@ClassId", classId, DbType.Int32);
        return ListWithOutputs<StudentAttendanceBoardRow>(AttendanceQueries.Get_StudentAttendanceBoard, p);
    }
}
