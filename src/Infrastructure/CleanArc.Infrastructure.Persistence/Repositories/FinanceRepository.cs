using CleanArc.Application.Common;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Finance;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Finance;
using CleanArc.Infrastructure.Sql.SqlQueries;
using CleanArc.SharedKernel.Extensions;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Data;

namespace CleanArc.Infrastructure.Persistence.Repositories;

public class FinanceRepository : IFinanceRepository
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<FinanceRepository> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FinanceRepository(IConfiguration configuration, ILogger<FinanceRepository> logger, IHttpContextAccessor httpContextAccessor)
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

    private async Task<ListResponseWrapper<T>> List<T>(string sp, DynamicParameters p)
    {
        using var connection = OpenConnection();
        p.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        p.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
        var rows = (await connection.QueryAsync<T>(sp, p, commandType: CommandType.StoredProcedure)).ToList();
        return new ListResponseWrapper<T>
        {
            Data = rows,
            TotalCount = rows.Count,
            Code = p.Get<int?>("@Code") ?? 0,
            Message = p.Get<string>("@Message") ?? string.Empty
        };
    }

    private async Task<SingleResponseWrapper<T>> Single<T>(string sp, DynamicParameters p)
    {
        using var connection = OpenConnection();
        p.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        p.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
        var data = await connection.QuerySingleOrDefaultAsync<T>(sp, p, commandType: CommandType.StoredProcedure);
        return new SingleResponseWrapper<T>
        {
            Data = data,
            Code = p.Get<int>("@Code"),
            Message = p.Get<string>("@Message")
        };
    }

    /* -------- FIN-01 Income -------- */

    public Task<SingleResponseWrapper<IncomeDashboardSummary>> GetIncomeSummaryAsync(int academicYearId)
    {
        var p = new DynamicParameters(); p.Add("@AcademicYearId", academicYearId, DbType.Int32);
        return Single<IncomeDashboardSummary>(FinanceQueries.Get_IncomeSummary, p);
    }

    public Task<ListResponseWrapper<MonthAmountRow>> GetIncomeByMonthAsync(int academicYearId)
    {
        var p = new DynamicParameters(); p.Add("@AcademicYearId", academicYearId, DbType.Int32);
        return List<MonthAmountRow>(FinanceQueries.Get_IncomeByMonth, p);
    }

    public Task<ListResponseWrapper<CategoryAmountRow>> GetIncomeByClassAsync(int academicYearId)
    {
        var p = new DynamicParameters(); p.Add("@AcademicYearId", academicYearId, DbType.Int32);
        return List<CategoryAmountRow>(FinanceQueries.Get_IncomeByClass, p);
    }

    public Task<ListResponseWrapper<CategoryAmountRow>> GetIncomeByFeeTypeAsync(int academicYearId)
    {
        var p = new DynamicParameters(); p.Add("@AcademicYearId", academicYearId, DbType.Int32);
        return List<CategoryAmountRow>(FinanceQueries.Get_IncomeByFeeType, p);
    }

    /* -------- FIN-02 Expense -------- */

    public Task<SingleResponseWrapper<ExpenseDashboardSummary>> GetExpenseSummaryAsync(int academicYearId)
    {
        var p = new DynamicParameters(); p.Add("@AcademicYearId", academicYearId, DbType.Int32);
        return Single<ExpenseDashboardSummary>(FinanceQueries.Get_ExpenseSummary, p);
    }

    public Task<ListResponseWrapper<MonthAmountRow>> GetExpenseByMonthAsync(int academicYearId)
    {
        var p = new DynamicParameters(); p.Add("@AcademicYearId", academicYearId, DbType.Int32);
        return List<MonthAmountRow>(FinanceQueries.Get_ExpenseByMonth, p);
    }

    public Task<ListResponseWrapper<CategoryAmountRow>> GetExpenseByCategoryAsync(int academicYearId)
    {
        var p = new DynamicParameters(); p.Add("@AcademicYearId", academicYearId, DbType.Int32);
        return List<CategoryAmountRow>(FinanceQueries.Get_ExpenseByCategory, p);
    }

    /* -------- FIN-03 P&L -------- */

    public Task<ListResponseWrapper<PnLRow>> GetPnLAsync(DateTime fromDate, DateTime toDate)
    {
        var p = new DynamicParameters();
        p.Add("@FromDate", fromDate);
        p.Add("@ToDate", toDate);
        return List<PnLRow>(FinanceQueries.Get_PnL, p);
    }

    /* -------- FIN-04 Reports -------- */

    public Task<ListResponseWrapper<PnLRow>> GetMonthlySummaryAsync(int month, int year)
    {
        var p = new DynamicParameters();
        p.Add("@Month", month, DbType.Int32);
        p.Add("@Year", year, DbType.Int32);
        return List<PnLRow>(FinanceQueries.Get_MonthlySummary, p);
    }

    public Task<ListResponseWrapper<PnLRow>> GetAnnualSummaryAsync(int academicYearId)
    {
        var p = new DynamicParameters(); p.Add("@AcademicYearId", academicYearId, DbType.Int32);
        return List<PnLRow>(FinanceQueries.Get_AnnualSummary, p);
    }

    public Task<ListResponseWrapper<CategoryAmountRow>> GetCategoryWiseExpenseAsync(DateTime fromDate, DateTime toDate)
    {
        var p = new DynamicParameters();
        p.Add("@FromDate", fromDate);
        p.Add("@ToDate", toDate);
        return List<CategoryAmountRow>(FinanceQueries.Get_CategoryWiseExpense, p);
    }

    public Task<ListResponseWrapper<FeeCollectionVsTargetRow>> GetFeeCollectionVsTargetAsync(int academicYearId)
    {
        var p = new DynamicParameters(); p.Add("@AcademicYearId", academicYearId, DbType.Int32);
        return List<FeeCollectionVsTargetRow>(FinanceQueries.Get_FeeCollectionVsTarget, p);
    }

    /* -------- FIN-05 Cash Flow -------- */

    public async Task<ResponseEntity> SetOpeningCashBalanceAsync(SetOpeningCashBalanceDTO dto)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, dto);
        using var connection = OpenConnection();
        var r = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(
            FinanceQueries.Set_OpeningCashBalance, dto, commandType: CommandType.StoredProcedure);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(r);
        return r;
    }

    public Task<SingleResponseWrapper<OpeningCashBalance>> GetOpeningCashBalanceAsync(int academicYearId)
    {
        var p = new DynamicParameters(); p.Add("@AcademicYearId", academicYearId, DbType.Int32);
        return Single<OpeningCashBalance>(FinanceQueries.Get_OpeningCashBalance, p);
    }

    public Task<ListResponseWrapper<CashFlowLedgerRow>> GetCashFlowLedgerAsync(DateTime fromDate, DateTime toDate, int academicYearId)
    {
        var p = new DynamicParameters();
        p.Add("@FromDate", fromDate);
        p.Add("@ToDate", toDate);
        p.Add("@AcademicYearId", academicYearId, DbType.Int32);
        return List<CashFlowLedgerRow>(FinanceQueries.Get_CashFlowLedger, p);
    }
}
