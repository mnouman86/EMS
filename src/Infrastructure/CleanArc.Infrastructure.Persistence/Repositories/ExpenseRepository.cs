using CleanArc.Application.Common;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Expense;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Expense;
using CleanArc.Infrastructure.Persistence.Helpers;
using CleanArc.Infrastructure.Sql.SqlQueries;
using CleanArc.SharedKernel.Extensions;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace CleanArc.Infrastructure.Persistence.Repositories;

public class ExpenseRepository : IExpenseRepository
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<ExpenseRepository> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ExpenseRepository(IConfiguration configuration, ILogger<ExpenseRepository> logger, IHttpContextAccessor httpContextAccessor)
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

    private async Task<ResponseEntity> Scalar<T>(string sp, T parameters)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, parameters);
        using var connection = OpenConnection();
        var r = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(sp, parameters, commandType: CommandType.StoredProcedure);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(r);
        return r;
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

    /* -------- Categories -------- */

    public Task<ResponseEntity> UpsertCategoryAsync(UpsertExpenseCategoryDTO dto) => Scalar(ExpenseQueries.Upsert_Category, dto);

    public Task<ResponseEntity> DeleteCategoryAsync(DeleteRequest req, int? updatedBy)
    {
        var p = new DynamicParameters();
        p.Add("@Ids", req.SelectedIds);
        p.Add("@UpdatedBy", updatedBy);
        p.Add("@IsDeleted", req.isDeleted);
        p.Add("@ForceHard", req.ForceHard);
        using var conn = OpenConnection();
        return conn.QueryFirstOrDefaultAsync<ResponseEntity>(ExpenseQueries.Delete_Category, p, commandType: CommandType.StoredProcedure);
    }

    public Task<ListResponseWrapper<ExpenseCategory>> GetCategoriesAsync(bool includeInactive)
    {
        var p = new DynamicParameters();
        p.Add("@IncludeInactive", includeInactive);
        return ListWithOutputs<ExpenseCategory>(ExpenseQueries.Get_Categories, p);
    }

    /* -------- Recurring -------- */

    public Task<ResponseEntity> UpsertRecurringTemplateAsync(UpsertRecurringTemplateDTO dto) => Scalar(ExpenseQueries.Upsert_RecurringTemplate, dto);

    public Task<ListResponseWrapper<ExpenseRecurringTemplate>> GetRecurringTemplatesAsync()
        => ListWithOutputs<ExpenseRecurringTemplate>(ExpenseQueries.Get_RecurringTemplates, new DynamicParameters());

    public Task<ResponseEntity> GenerateRecurringExpensesAsync(GenerateRecurringExpensesDTO dto) => Scalar(ExpenseQueries.Generate_RecurringExpenses, dto);

    /* -------- Expenses -------- */

    public Task<ResponseEntity> RecordExpenseAsync(RecordExpenseDTO dto) => Scalar(ExpenseQueries.Record_Expense, dto);

    public Task<ResponseEntity> DeleteExpenseAsync(DeleteRequest req, int? updatedBy)
    {
        var p = new DynamicParameters();
        p.Add("@Ids", req.SelectedIds);
        p.Add("@UpdatedBy", updatedBy);
        p.Add("@IsDeleted", req.isDeleted);
        p.Add("@ForceHard", req.ForceHard);
        using var conn = OpenConnection();
        return conn.QueryFirstOrDefaultAsync<ResponseEntity>(ExpenseQueries.Delete_Expense, p, commandType: CommandType.StoredProcedure);
    }

    public async Task<ListResponseWrapper<Expense>> GetExpensesAsync(SearchRequest req)
    {
        var p = new DynamicParameters();
        p.Add("@PageNumber", req.PageNumber, DbType.Int32);
        if (req.PageSize > 0) p.Add("@PageSize", req.PageSize, DbType.Int32);
        p.Add("@SortingArray", DataTableHelper.ToDataTable(req.SortingArray), DbType.Object);
        p.Add("@FilterArray", DataTableHelper.ToDataTable(req.FilterArray), DbType.Object);
        p.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
        p.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        p.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
        using var conn = OpenConnection();
        var rows = (await conn.QueryAsync<Expense>(ExpenseQueries.Get_Expenses, p, commandType: CommandType.StoredProcedure)).ToList();
        return new ListResponseWrapper<Expense>
        {
            Data = rows,
            TotalCount = p.Get<int?>("@TotalCount") ?? 0,
            Code = p.Get<int?>("@Code") ?? 0,
            Message = p.Get<string>("@Message") ?? string.Empty
        };
    }

    /* -------- Budget -------- */

    public Task<ListResponseWrapper<BudgetVsActualRow>> GetBudgetMonitoringAsync(int month, int year)
    {
        var p = new DynamicParameters();
        p.Add("@Month", month, DbType.Int32);
        p.Add("@Year", year, DbType.Int32);
        return ListWithOutputs<BudgetVsActualRow>(ExpenseQueries.Get_BudgetMonitoring, p);
    }

    /* -------- Payroll -------- */

    public Task<ResponseEntity> StartPayrollRunAsync(StartPayrollRunDTO dto) => Scalar(ExpenseQueries.Start_PayrollRun, dto);
    public Task<ResponseEntity> AdjustPayrollEntryAsync(AdjustPayrollEntryDTO dto) => Scalar(ExpenseQueries.Adjust_PayrollEntry, dto);
    public Task<ResponseEntity> ConfirmPayrollRunAsync(ConfirmPayrollRunDTO dto) => Scalar(ExpenseQueries.Confirm_PayrollRun, dto);

    public Task<ListResponseWrapper<PayrollRun>> GetPayrollRunsAsync(int? year)
    {
        var p = new DynamicParameters();
        p.Add("@Year", year, DbType.Int32);
        return ListWithOutputs<PayrollRun>(ExpenseQueries.Get_PayrollRuns, p);
    }

    public Task<ListResponseWrapper<PayrollEntry>> GetPayrollEntriesAsync(int payrollRunId)
    {
        var p = new DynamicParameters();
        p.Add("@PayrollRunId", payrollRunId, DbType.Int32);
        return ListWithOutputs<PayrollEntry>(ExpenseQueries.Get_PayrollEntries, p);
    }

    public async Task<SingleResponseWrapper<PayrollEntry>> GetPayrollEntryAsync(int payrollEntryId)
    {
        var p = new DynamicParameters();
        p.Add("@PayrollEntryId", payrollEntryId, DbType.Int32);
        p.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        p.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
        using var conn = OpenConnection();
        var data = await conn.QuerySingleOrDefaultAsync<PayrollEntry>(
            ExpenseQueries.Get_PayrollEntry, p, commandType: CommandType.StoredProcedure);
        return new SingleResponseWrapper<PayrollEntry>
        {
            Data = data,
            Code = p.Get<int>("@Code"),
            Message = p.Get<string>("@Message")
        };
    }
}
