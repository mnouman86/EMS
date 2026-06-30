using CleanArc.Application.Common;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Inventory;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Inventory;
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

public class InventoryRepository : IInventoryRepository
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<InventoryRepository> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public InventoryRepository(IConfiguration configuration, ILogger<InventoryRepository> logger, IHttpContextAccessor httpContextAccessor)
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

    /* ---------- Catalogue ---------- */

    public Task<ResponseEntity> UpsertCategoryAsync(UpsertCategoryDTO dto) => Scalar(InventoryQueries.Upsert_Category, dto);

    public Task<ListResponseWrapper<InventoryCategory>> GetCategoriesAsync()
        => ListWithOutputs<InventoryCategory>(InventoryQueries.GetAll_Categories, new DynamicParameters());

    public Task<ResponseEntity> UpsertItemAsync(UpsertItemDTO dto) => Scalar(InventoryQueries.Upsert_Item, dto);

    public Task<ResponseEntity> DeleteItemAsync(DeleteRequest req, int? updatedBy)
    {
        var p = new DynamicParameters();
        p.Add("@Ids", req.SelectedIds);
        p.Add("@UpdatedBy", updatedBy);
        p.Add("@IsDeleted", req.isDeleted);
        p.Add("@ForceHard", req.ForceHard);
        using var conn = OpenConnection();
        return conn.QueryFirstOrDefaultAsync<ResponseEntity>(InventoryQueries.Delete_Item, p, commandType: CommandType.StoredProcedure);
    }

    public async Task<ListResponseWrapper<InventoryItem>> GetItemsAsync(SearchRequest req)
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
        var rows = (await conn.QueryAsync<InventoryItem>(InventoryQueries.GetAll_Items, p, commandType: CommandType.StoredProcedure)).ToList();
        return new ListResponseWrapper<InventoryItem>
        {
            Data = rows,
            TotalCount = p.Get<int?>("@TotalCount") ?? 0,
            Code = p.Get<int?>("@Code") ?? 0,
            Message = p.Get<string>("@Message") ?? string.Empty
        };
    }

    public async Task<SingleResponseWrapper<InventoryItem>> GetItemByIdAsync(int id)
    {
        using var connection = OpenConnection();
        var p = new DynamicParameters();
        p.Add("@Id", id, DbType.Int32);
        p.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        p.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
        var data = await connection.QuerySingleOrDefaultAsync<InventoryItem>(
            InventoryQueries.Get_ItemById, p, commandType: CommandType.StoredProcedure);
        return new SingleResponseWrapper<InventoryItem>
        {
            Data = data,
            Code = p.Get<int>("@Code"),
            Message = p.Get<string>("@Message")
        };
    }

    /* ---------- Movements ---------- */

    public Task<ResponseEntity> RecordPurchaseAsync(RecordPurchaseDTO dto) => Scalar(InventoryQueries.Record_Purchase, dto);
    public Task<ResponseEntity> IssueItemsAsync(IssueItemsDTO dto) => Scalar(InventoryQueries.Issue_Items, dto);
    public Task<ResponseEntity> RecordReturnAsync(RecordReturnDTO dto) => Scalar(InventoryQueries.Record_Return, dto);
    public Task<ResponseEntity> RecordAdjustmentAsync(StockAdjustmentDTO dto) => Scalar(InventoryQueries.Record_Adjustment, dto);

    /* ---------- Dashboard / Alerts / Reports ---------- */

    public Task<ListResponseWrapper<StockDashboardRow>> GetStockDashboardAsync(int? categoryId, string status, System.DateTime fromDate, System.DateTime toDate)
    {
        var p = new DynamicParameters();
        p.Add("@CategoryId", categoryId, DbType.Int32);
        p.Add("@Status", status);
        p.Add("@FromDate", fromDate);
        p.Add("@ToDate", toDate);
        return ListWithOutputs<StockDashboardRow>(InventoryQueries.Get_StockDashboard, p);
    }

    public Task<ListResponseWrapper<LowStockAlertRow>> GetLowStockAlertsAsync()
        => ListWithOutputs<LowStockAlertRow>(InventoryQueries.Get_LowStockAlerts, new DynamicParameters());

    public Task<ResponseEntity> SnoozeAlertAsync(SnoozeAlertDTO dto) => Scalar(InventoryQueries.Snooze_Alert, dto);

    public Task<ListResponseWrapper<InventoryPurchase>> GetPurchaseRegisterAsync(
        System.DateTime fromDate, System.DateTime toDate, int? categoryId, string? vendorName,
        bool includeCancelled = false)
    {
        var p = new DynamicParameters();
        p.Add("@FromDate", fromDate);
        p.Add("@ToDate", toDate);
        p.Add("@CategoryId", categoryId, DbType.Int32);
        p.Add("@VendorName", vendorName, DbType.String, size: 150);
        p.Add("@IncludeCancelled", includeCancelled, DbType.Boolean);
        return ListWithOutputs<InventoryPurchase>(InventoryQueries.Get_PurchaseRegister, p);
    }

    public Task<ListResponseWrapper<InventoryIssue>> GetIssueRegisterAsync(
        System.DateTime fromDate, System.DateTime toDate,
        int? categoryId, int? itemId, string? issuedToType, int? issuedToId, int? issuedByUserId,
        bool includeCancelled = false)
    {
        var p = new DynamicParameters();
        p.Add("@FromDate", fromDate);
        p.Add("@ToDate", toDate);
        p.Add("@CategoryId", categoryId, DbType.Int32);
        p.Add("@ItemId", itemId, DbType.Int32);
        p.Add("@IssuedToType", issuedToType, DbType.String, size: 20);
        p.Add("@IssuedToId", issuedToId, DbType.Int32);
        p.Add("@IssuedByUserId", issuedByUserId, DbType.Int32);
        p.Add("@IncludeCancelled", includeCancelled, DbType.Boolean);
        return ListWithOutputs<InventoryIssue>(InventoryQueries.Get_IssueRegister, p);
    }

    public Task<ResponseEntity> CancelPurchaseAsync(int purchaseId, string reason, int? actorUserId)
    {
        var p = new DynamicParameters();
        p.Add("@PurchaseId", purchaseId, DbType.Int32);
        p.Add("@Reason", reason, DbType.String, size: 500);
        p.Add("@ActorUserId", actorUserId, DbType.Int32);
        return Scalar(InventoryQueries.Cancel_Purchase, p);
    }

    public Task<ResponseEntity> CancelIssueAsync(int issueId, string reason, int? actorUserId)
    {
        var p = new DynamicParameters();
        p.Add("@IssueId", issueId, DbType.Int32);
        p.Add("@Reason", reason, DbType.String, size: 500);
        p.Add("@ActorUserId", actorUserId, DbType.Int32);
        return Scalar(InventoryQueries.Cancel_Issue, p);
    }

    public Task<ListResponseWrapper<ItemLedgerRow>> GetItemLedgerAsync(int itemId, System.DateTime fromDate, System.DateTime toDate)
    {
        var p = new DynamicParameters();
        p.Add("@ItemId", itemId, DbType.Int32);
        p.Add("@FromDate", fromDate);
        p.Add("@ToDate", toDate);
        return ListWithOutputs<ItemLedgerRow>(InventoryQueries.Get_ItemLedger, p);
    }

    public Task<ListResponseWrapper<InventoryIssueDetailRow>> GetIssueDetailAsync(int issueId)
    {
        var p = new DynamicParameters();
        p.Add("@IssueId", issueId, DbType.Int32);
        return ListWithOutputs<InventoryIssueDetailRow>(InventoryQueries.Get_IssueDetail, p);
    }

    public Task<ListResponseWrapper<InventoryPurchaseDetailRow>> GetPurchaseDetailAsync(int purchaseId)
    {
        var p = new DynamicParameters();
        p.Add("@PurchaseId", purchaseId, DbType.Int32);
        return ListWithOutputs<InventoryPurchaseDetailRow>(InventoryQueries.Get_PurchaseDetail, p);
    }

    /* ---------- Issue request workflow (new) ---------- */

    public Task<ResponseEntity> CreateIssueRequestAsync(CreateInventoryIssueRequestDTO dto)
        => Scalar(InventoryQueries.Create_IssueRequest, new
        {
            dto.RequestedByUserId, dto.Purpose, dto.LinesJson
        });

    public Task<ResponseEntity> ApproveIssueRequestAsync(int requestId, int approvedByUserId)
        => Scalar(InventoryQueries.Approve_IssueRequest, new { RequestId = requestId, ApprovedByUserId = approvedByUserId });

    public Task<ResponseEntity> RejectIssueRequestAsync(int requestId, int rejectedByUserId, string reason)
        => Scalar(InventoryQueries.Reject_IssueRequest, new { RequestId = requestId, RejectedByUserId = rejectedByUserId, Reason = reason });

    public Task<ResponseEntity> FulfillIssueRequestAsync(int requestId, int fulfilledByUserId, System.DateTime issueDate)
        => Scalar(InventoryQueries.Fulfill_IssueRequest, new { RequestId = requestId, FulfilledByUserId = fulfilledByUserId, IssueDate = issueDate });

    public Task<ListResponseWrapper<InventoryIssueRequestRow>> GetIssueRequestsAsync(
        int callerUserId, bool mineOnly, string? status, System.DateTime? fromDate, System.DateTime? toDate)
    {
        var p = new DynamicParameters();
        p.Add("@CallerUserId", callerUserId, DbType.Int32);
        p.Add("@MineOnly", mineOnly, DbType.Boolean);
        p.Add("@Status", status, DbType.String, size: 20);
        p.Add("@FromDate", fromDate, DbType.DateTime);
        p.Add("@ToDate", toDate, DbType.DateTime);
        return ListWithOutputs<InventoryIssueRequestRow>(InventoryQueries.Get_IssueRequests, p);
    }

    public Task<ListResponseWrapper<InventoryIssueRequestLineRow>> GetIssueRequestLinesAsync(int requestId)
    {
        var p = new DynamicParameters();
        p.Add("@RequestId", requestId, DbType.Int32);
        return ListWithOutputs<InventoryIssueRequestLineRow>(InventoryQueries.Get_IssueRequestLines, p);
    }

    public Task<ListResponseWrapper<MyIssuedInventoryRow>> GetMyIssuedAsync(int callerUserId, System.DateTime fromDate, System.DateTime toDate)
    {
        var p = new DynamicParameters();
        p.Add("@UserId", callerUserId, DbType.Int32);
        p.Add("@FromDate", fromDate);
        p.Add("@ToDate", toDate);
        return ListWithOutputs<MyIssuedInventoryRow>(InventoryQueries.Get_MyIssued, p);
    }
}
