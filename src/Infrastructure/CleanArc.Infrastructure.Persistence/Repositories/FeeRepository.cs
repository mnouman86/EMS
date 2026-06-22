using CleanArc.Application.Common;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Fee;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Fee;
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

public class FeeRepository : IFeeRepository
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<FeeRepository> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FeeRepository(IConfiguration configuration, ILogger<FeeRepository> logger, IHttpContextAccessor httpContextAccessor)
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

    private async Task<SingleResponseWrapper<T>> SingleWithOutputs<T>(string sp, DynamicParameters parameters)
    {
        using var connection = OpenConnection();
        parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
        var data = await connection.QuerySingleOrDefaultAsync<T>(sp, parameters, commandType: CommandType.StoredProcedure);
        return new SingleResponseWrapper<T>
        {
            Data = data,
            Code = parameters.Get<int>("@Code"),
            Message = parameters.Get<string>("@Message")
        };
    }

    /* -------- Configuration -------- */

    public Task<ResponseEntity> UpsertFeeTypeAsync(UpsertFeeTypeDTO dto) => Scalar(FeeQueries.Upsert_FeeType, dto);

    public Task<ResponseEntity> DeleteFeeTypeAsync(DeleteRequest req, int? updatedBy)
    {
        var p = new DynamicParameters();
        p.Add("@Ids", req.SelectedIds);
        p.Add("@UpdatedBy", updatedBy);
        p.Add("@IsDeleted", req.isDeleted);
        p.Add("@ForceHard", req.ForceHard);
        using var conn = OpenConnection();
        return conn.QueryFirstOrDefaultAsync<ResponseEntity>(FeeQueries.Delete_FeeType, p, commandType: CommandType.StoredProcedure);
    }

    public async Task<ListResponseWrapper<FeeType>> GetFeeTypesAsync(SearchRequest req)
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
        var rows = (await conn.QueryAsync<FeeType>(FeeQueries.GetAll_FeeTypes, p, commandType: CommandType.StoredProcedure)).ToList();
        return new ListResponseWrapper<FeeType>
        {
            Data = rows,
            TotalCount = p.Get<int?>("@TotalCount") ?? 0,
            Code = p.Get<int?>("@Code") ?? 0,
            Message = p.Get<string>("@Message") ?? string.Empty
        };
    }

    public Task<ResponseEntity> UpsertFeeTypeAmountAsync(UpsertFeeTypeAmountDTO dto) => Scalar(FeeQueries.Upsert_FeeTypeAmount, dto);

    public Task<ListResponseWrapper<FeeStructureRow>> GetFeeStructureForClassAsync(int schoolClassId, int academicYearId)
    {
        var p = new DynamicParameters();
        p.Add("@SchoolClassId", schoolClassId, DbType.Int32);
        p.Add("@AcademicYearId", academicYearId, DbType.Int32);
        return ListWithOutputs<FeeStructureRow>(FeeQueries.Get_FeeStructureForClass, p);
    }

    public Task<ResponseEntity> ConfigureFeeCalendarAsync(ConfigureFeeCalendarDTO dto) => Scalar(FeeQueries.Configure_FeeCalendar, dto);

    /* -------- Invoices -------- */

    public Task<ListResponseWrapper<InvoicePreviewRow>> GenerateMonthlyInvoicesAsync(GenerateMonthlyInvoicesDTO dto)
    {
        var p = new DynamicParameters();
        p.Add("@AcademicYearId", dto.AcademicYearId, DbType.Int32);
        p.Add("@BillingMonth", dto.BillingMonth, DbType.Int32);
        p.Add("@BillingYear", dto.BillingYear, DbType.Int32);
        p.Add("@ClassId", dto.ClassId, DbType.Int32);
        p.Add("@ExcludedStudentIdsCsv", dto.ExcludedStudentIdsCsv);
        p.Add("@DryRun", dto.DryRun);
        p.Add("@CreatedBy", dto.CreatedBy, DbType.Int32);
        return ListWithOutputs<InvoicePreviewRow>(FeeQueries.Generate_MonthlyInvoices, p);
    }

    public Task<ResponseEntity> CancelInvoiceAsync(CancelInvoiceDTO dto) => Scalar(FeeQueries.Cancel_FeeInvoice, dto);

    /* -------- Payments -------- */

    public Task<ResponseEntity> RecordPaymentAsync(RecordPaymentDTO dto) => Scalar(FeeQueries.Record_FeePayment, dto);
    public Task<ResponseEntity> ClearChequeAsync(ClearChequeDTO dto) => Scalar(FeeQueries.Clear_Cheque, dto);
    public Task<ResponseEntity> ApplyAdvanceAsync(ApplyAdvanceDTO dto) => Scalar(FeeQueries.Apply_Advance, dto);
    public Task<ResponseEntity> ReversePaymentAsync(ReversePaymentDTO dto) => Scalar(FeeQueries.Reverse_FeePayment, dto);

    /* -------- Ledger / receipts -------- */

    public Task<ListResponseWrapper<StudentLedgerEntry>> GetStudentLedgerAsync(int studentId, int? academicYearId)
    {
        var p = new DynamicParameters();
        p.Add("@StudentId", studentId, DbType.Int32);
        p.Add("@AcademicYearId", academicYearId, DbType.Int32);
        return ListWithOutputs<StudentLedgerEntry>(FeeQueries.Get_StudentLedger, p);
    }

    public Task<SingleResponseWrapper<FeePayment>> GetPaymentByIdAsync(int paymentId)
    {
        var p = new DynamicParameters();
        p.Add("@PaymentId", paymentId, DbType.Int32);
        return SingleWithOutputs<FeePayment>(FeeQueries.Get_PaymentById, p);
    }

    public Task<ListResponseWrapper<FeePaymentAllocation>> GetPaymentAllocationsAsync(int paymentId)
    {
        var p = new DynamicParameters();
        p.Add("@PaymentId", paymentId, DbType.Int32);
        return ListWithOutputs<FeePaymentAllocation>(FeeQueries.Get_PaymentAllocations, p);
    }

    /* -------- Concession -------- */

    public Task<ResponseEntity> GrantConcessionAsync(GrantConcessionDTO dto) => Scalar(FeeQueries.Grant_Concession, dto);
    public Task<ResponseEntity> RevokeConcessionAsync(RevokeConcessionDTO dto) => Scalar(FeeQueries.Revoke_Concession, dto);

    public Task<ListResponseWrapper<FeeConcession>> GetConcessionsAsync(int? studentId)
    {
        var p = new DynamicParameters();
        p.Add("@StudentId", studentId, DbType.Int32);
        return ListWithOutputs<FeeConcession>(FeeQueries.Get_Concessions, p);
    }

    /* -------- Reminders -------- */

    public Task<ListResponseWrapper<PendingFeeRow>> GetReminderTargetsAsync(string studentIdsCsv)
    {
        var p = new DynamicParameters();
        p.Add("@StudentIdsCsv", studentIdsCsv);
        return ListWithOutputs<PendingFeeRow>(FeeQueries.Get_ReminderTargets, p);
    }

    /* -------- Arrears -------- */

    public Task<ResponseEntity> CarryForwardArrearsAsync(CarryForwardArrearsDTO dto) => Scalar(FeeQueries.CarryForward_Arrears, dto);
    public Task<ResponseEntity> WriteOffArrearAsync(WriteOffArrearDTO dto) => Scalar(FeeQueries.WriteOff_Arrear, dto);

    public Task<ListResponseWrapper<FeeArrearRow>> GetArrearsAsync(int? studentId, bool includeWrittenOff)
    {
        var p = new DynamicParameters();
        p.Add("@StudentId", studentId, DbType.Int32);
        p.Add("@IncludeWrittenOff", includeWrittenOff, DbType.Boolean);
        return ListWithOutputs<FeeArrearRow>(FeeQueries.Get_Arrears, p);
    }

    /* -------- Dashboards / Reports -------- */

    public Task<SingleResponseWrapper<CollectionDashboardSummary>> GetCollectionSummaryAsync(System.DateTime fromDate, System.DateTime toDate, int? classId)
    {
        var p = new DynamicParameters();
        p.Add("@FromDate", fromDate);
        p.Add("@ToDate", toDate);
        p.Add("@ClassId", classId, DbType.Int32);
        return SingleWithOutputs<CollectionDashboardSummary>(FeeQueries.Get_CollectionSummary, p);
    }

    public Task<ListResponseWrapper<CollectionByClassRow>> GetCollectionByClassAsync(System.DateTime fromDate, System.DateTime toDate)
    {
        var p = new DynamicParameters();
        p.Add("@FromDate", fromDate);
        p.Add("@ToDate", toDate);
        return ListWithOutputs<CollectionByClassRow>(FeeQueries.Get_CollectionByClass, p);
    }

    public Task<ListResponseWrapper<CollectionByDayRow>> GetCollectionByDayAsync(System.DateTime fromDate, System.DateTime toDate)
    {
        var p = new DynamicParameters();
        p.Add("@FromDate", fromDate);
        p.Add("@ToDate", toDate);
        return ListWithOutputs<CollectionByDayRow>(FeeQueries.Get_CollectionByDay, p);
    }

    public Task<ListResponseWrapper<PendingFeeRow>> GetPendingFeeListAsync(int? classId, int? minOutstanding, int? minDaysOverdue)
    {
        var p = new DynamicParameters();
        p.Add("@ClassId", classId, DbType.Int32);
        p.Add("@MinOutstanding", minOutstanding, DbType.Int32);
        p.Add("@MinDaysOverdue", minDaysOverdue, DbType.Int32);
        return ListWithOutputs<PendingFeeRow>(FeeQueries.Get_PendingFeeList, p);
    }

    public Task<ListResponseWrapper<PendingFeeRow>> GetMonthlyNonSubmittedAsync(int month, int year, int? classId)
    {
        var p = new DynamicParameters();
        p.Add("@Month", month, DbType.Int32);
        p.Add("@Year", year, DbType.Int32);
        p.Add("@ClassId", classId, DbType.Int32);
        return ListWithOutputs<PendingFeeRow>(FeeQueries.Get_MonthlyNonSubmitted, p);
    }

    public Task<ListResponseWrapper<FeeReportAgeingRow>> GetOutstandingAgeingAsync(int? classId)
    {
        var p = new DynamicParameters();
        p.Add("@ClassId", classId, DbType.Int32);
        return ListWithOutputs<FeeReportAgeingRow>(FeeQueries.Get_OutstandingAgeing, p);
    }

    /* -------- Advance / Parent -------- */

    public Task<SingleResponseWrapper<StudentAdvanceBalance>> GetAdvanceBalanceAsync(int studentId)
    {
        var p = new DynamicParameters();
        p.Add("@StudentId", studentId, DbType.Int32);
        return SingleWithOutputs<StudentAdvanceBalance>(FeeQueries.Get_AdvanceBalance, p);
    }

    public Task<SingleResponseWrapper<ParentFeeSummary>> ParentSearchAsync(ParentFeeSearchDTO dto)
    {
        var p = new DynamicParameters();
        p.Add("@StudentCode", dto.StudentCode);
        p.Add("@SecondFactor", dto.SecondFactor);
        p.Add("@ClientIp", dto.ClientIp);
        return SingleWithOutputs<ParentFeeSummary>(FeeQueries.Parent_FeeSearch, p);
    }

    public Task<ListResponseWrapper<FeeClassBoardRow>> GetClassFeeBoardAsync(
        int classId, int? academicYearId, int? periodYear, int? periodMonth)
    {
        var p = new DynamicParameters();
        p.Add("@ClassId", classId, DbType.Int32);
        p.Add("@AcademicYearId", academicYearId, DbType.Int32);
        p.Add("@PeriodYear", periodYear, DbType.Int32);
        p.Add("@PeriodMonth", periodMonth, DbType.Int32);
        return ListWithOutputs<FeeClassBoardRow>(FeeQueries.Get_FeeClassBoard, p);
    }
}
