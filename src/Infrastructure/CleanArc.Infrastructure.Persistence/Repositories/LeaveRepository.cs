using CleanArc.Application.Common;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Leave;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Leave;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace CleanArc.Infrastructure.Persistence.Repositories;

public class LeaveRepository : ILeaveRepository
{
    private readonly IConfiguration _config;
    private readonly ILogger<LeaveRepository> _logger;
    private readonly IHttpContextAccessor _http;

    public LeaveRepository(IConfiguration c, ILogger<LeaveRepository> l, IHttpContextAccessor h)
    { _config = c; _logger = l; _http = h; }

    private SqlConnection Open()
    {
        var c = new SqlConnection(_config.GetConnectionString("DBConnection1"));
        c.Open(); return c;
    }

    private async Task<ResponseEntity> Mut(string sp, DynamicParameters p, bool withId = false)
    {
        using var conn = Open();
        p.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        p.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
        if (withId) p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
        await conn.ExecuteAsync(sp, p, commandType: CommandType.StoredProcedure);
        var res = new ResponseEntity
        {
            Code = p.Get<int?>("@Code") ?? 0,
            Message = p.Get<string>("@Message") ?? "",
            IsSuccess = (p.Get<int?>("@Code") ?? 0) == 200
        };
        if (withId) res.RecordID = (p.Get<int?>("@Id") ?? -1).ToString();
        return res;
    }

    private async Task<ListResponseWrapper<T>> List<T>(string sp, DynamicParameters p)
    {
        using var conn = Open();
        p.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        p.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
        var rows = (await conn.QueryAsync<T>(sp, p, commandType: CommandType.StoredProcedure)).ToList();
        return new ListResponseWrapper<T>
        {
            Data = rows, TotalCount = rows.Count,
            Code = p.Get<int?>("@Code") ?? 0,
            Message = p.Get<string>("@Message") ?? ""
        };
    }

    /* Types */
    public Task<ResponseEntity> UpsertLeaveTypeAsync(UpsertLeaveTypeDTO d, int actor)
    {
        var p = new DynamicParameters();
        p.Add("@LeaveTypeId", d.LeaveTypeId, DbType.Int32);
        p.Add("@Code", d.Code, DbType.AnsiString, size: 20);
        p.Add("@Name", d.Name, DbType.String, size: 80);
        p.Add("@DefaultAnnualQuota", d.DefaultAnnualQuota, DbType.Decimal);
        p.Add("@IsPaid", d.IsPaid, DbType.Boolean);
        p.Add("@RequiresAttachment", d.RequiresAttachment, DbType.Boolean);
        p.Add("@IsActive", d.IsActive, DbType.Boolean);
        p.Add("@DisplayOrder", d.DisplayOrder, DbType.Int32);
        p.Add("@ActorUserId", actor, DbType.Int32);
        // upsert SP has @Id + @Code_out + @Message OUTPUT
        p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
        p.Add("@Code_out", dbType: DbType.Int32, direction: ParameterDirection.Output);
        p.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
        using var conn = Open();
        conn.Execute("dbo.usp_Upsert_LeaveType", p, commandType: CommandType.StoredProcedure);
        return Task.FromResult(new ResponseEntity
        {
            Code = p.Get<int?>("@Code_out") ?? 0,
            Message = p.Get<string>("@Message") ?? "",
            IsSuccess = (p.Get<int?>("@Code_out") ?? 0) == 200,
            RecordID = (p.Get<int?>("@Id") ?? -1).ToString()
        });
    }

    public Task<ListResponseWrapper<LeaveType>> GetAllLeaveTypesAsync(bool activeOnly)
    {
        var p = new DynamicParameters();
        p.Add("@ActiveOnly", activeOnly, DbType.Boolean);
        return List<LeaveType>("dbo.usp_GetAll_LeaveTypes", p);
    }

    public Task<ResponseEntity> DeleteLeaveTypeAsync(int id, int actor)
    {
        var p = new DynamicParameters();
        p.Add("@LeaveTypeId", id, DbType.Int32);
        p.Add("@ActorUserId", actor, DbType.Int32);
        return Mut("dbo.usp_Delete_LeaveType", p);
    }

    /* Policy */
    public Task<ResponseEntity> UpsertLeavePolicyAsync(UpsertLeavePolicyDTO d, int actor)
    {
        var p = new DynamicParameters();
        p.Add("@EmployeeId", d.EmployeeId, DbType.Int32);
        p.Add("@LeaveTypeId", d.LeaveTypeId, DbType.Int32);
        p.Add("@AcademicYearId", d.AcademicYearId, DbType.Int32);
        p.Add("@AnnualQuota", d.AnnualQuota, DbType.Decimal);
        p.Add("@Notes", d.Notes, DbType.String, size: 400);
        p.Add("@ActorUserId", actor, DbType.Int32);
        return Mut("dbo.usp_Upsert_LeavePolicy", p);
    }

    public Task<ListResponseWrapper<LeavePolicyRow>> GetAllLeavePoliciesAsync(int? academicYearId, int? employeeId)
    {
        var p = new DynamicParameters();
        p.Add("@AcademicYearId", academicYearId, DbType.Int32);
        p.Add("@EmployeeId", employeeId, DbType.Int32);
        return List<LeavePolicyRow>("dbo.usp_GetAll_LeavePolicies", p);
    }

    /* Routing */
    public Task<ResponseEntity> UpsertLeaveRouteAsync(UpsertLeaveRouteDTO d, int actor)
    {
        var p = new DynamicParameters();
        p.Add("@ApplicantRoleId", d.ApplicantRoleId, DbType.Int32);
        p.Add("@ApproverUserId", d.ApproverUserId, DbType.Int32);
        p.Add("@Notes", d.Notes, DbType.String, size: 400);
        p.Add("@ActorUserId", actor, DbType.Int32);
        return Mut("dbo.usp_Upsert_LeaveApprovalRoute", p);
    }

    public Task<ListResponseWrapper<LeaveApprovalRouteRow>> GetAllLeaveRoutesAsync()
        => List<LeaveApprovalRouteRow>("dbo.usp_GetAll_LeaveApprovalRoutes", new DynamicParameters());

    /* Balance */
    public Task<ListResponseWrapper<LeaveBalanceRow>> GetLeaveBalanceAsync(int userId, int? academicYearId)
    {
        var p = new DynamicParameters();
        p.Add("@UserId", userId, DbType.Int32);
        p.Add("@AcademicYearId", academicYearId, DbType.Int32);
        return List<LeaveBalanceRow>("dbo.usp_Get_LeaveBalance", p);
    }

    /* Application */
    public Task<ResponseEntity> SubmitLeaveApplicationAsync(SubmitLeaveApplicationDTO d)
    {
        var p = new DynamicParameters();
        p.Add("@ApplicantUserId", d.ApplicantUserId, DbType.Int32);
        p.Add("@LeaveTypeId", d.LeaveTypeId, DbType.Int32);
        p.Add("@StartDate", d.StartDate, DbType.Date);
        p.Add("@EndDate", d.EndDate, DbType.Date);
        p.Add("@HalfDayFrom", d.HalfDayFrom, DbType.Boolean);
        p.Add("@HalfDayTo", d.HalfDayTo, DbType.Boolean);
        p.Add("@Reason", d.Reason, DbType.String, size: 1000);
        p.Add("@AttachmentPath", d.AttachmentPath, DbType.String, size: 400);
        p.Add("@AttachmentOriginalName", d.AttachmentOriginalName, DbType.String, size: 260);
        return Mut("dbo.usp_Submit_LeaveApplication", p, withId: true);
    }

    public async Task<decimal> PreviewWorkingDaysAsync(DateTime startDate, DateTime endDate, bool halfDayFrom, bool halfDayTo)
    {
        using var conn = Open();
        var p = new DynamicParameters();
        p.Add("@StartDate", startDate, DbType.Date);
        p.Add("@EndDate", endDate, DbType.Date);
        p.Add("@HalfDayFrom", halfDayFrom, DbType.Boolean);
        p.Add("@HalfDayTo", halfDayTo, DbType.Boolean);
        p.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        p.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
        var days = await conn.QueryFirstOrDefaultAsync<decimal?>(
            "dbo.usp_Preview_LeaveWorkingDays", p, commandType: CommandType.StoredProcedure);
        return days ?? 0m;
    }

    public Task<ResponseEntity> DecideLeaveApplicationAsync(int id, int decider, string decision, string? reason)
    {
        var p = new DynamicParameters();
        p.Add("@LeaveApplicationId", id, DbType.Int32);
        p.Add("@DeciderUserId", decider, DbType.Int32);
        p.Add("@Decision", decision, DbType.String, size: 20);
        p.Add("@Reason", reason, DbType.String, size: 1000);
        return Mut("dbo.usp_Decide_LeaveApplication", p);
    }

    public Task<ResponseEntity> CancelLeaveApplicationAsync(int id, int actor, string? reason)
    {
        var p = new DynamicParameters();
        p.Add("@LeaveApplicationId", id, DbType.Int32);
        p.Add("@ActorUserId", actor, DbType.Int32);
        p.Add("@Reason", reason, DbType.String, size: 1000);
        return Mut("dbo.usp_Cancel_LeaveApplication", p);
    }

    public Task<ListResponseWrapper<LeaveApplicationRow>> GetLeaveApplicationsAsync(GetLeaveApplicationsFilter f, int caller)
    {
        var p = new DynamicParameters();
        p.Add("@Scope", f.Scope, DbType.String, size: 20);
        p.Add("@CallerUserId", caller, DbType.Int32);
        p.Add("@Status", f.Status, DbType.String, size: 20);
        p.Add("@FromDate", f.FromDate, DbType.Date);
        p.Add("@ToDate", f.ToDate, DbType.Date);
        p.Add("@LeaveTypeId", f.LeaveTypeId, DbType.Int32);
        return List<LeaveApplicationRow>("dbo.usp_GetAll_LeaveApplications", p);
    }

    public async Task<SingleResponseWrapper<LeaveDashboardBundle>> GetDashboardAsync()
    {
        using var conn = Open();
        var p = new DynamicParameters();
        p.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        p.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
        using var multi = await conn.QueryMultipleAsync("dbo.usp_Get_LeaveDashboardKpis", p, commandType: CommandType.StoredProcedure);
        var totals = (await multi.ReadFirstOrDefaultAsync<LeaveDashboardTotals>()) ?? new LeaveDashboardTotals();
        var byType = (await multi.ReadAsync<LeavePendingByType>()).ToList();
        var recent = (await multi.ReadAsync<LeaveRecentActivity>()).ToList();
        return new SingleResponseWrapper<LeaveDashboardBundle>
        {
            Code = p.Get<int?>("@Code") ?? 200,
            Message = p.Get<string>("@Message") ?? "OK",
            Data = new LeaveDashboardBundle { Totals = totals, PendingByType = byType, Recent = recent }
        };
    }
}
