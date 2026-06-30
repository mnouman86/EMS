using CleanArc.Application.Common;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Complaint;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Complaint;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace CleanArc.Infrastructure.Persistence.Repositories;

public class ComplaintRepository : IComplaintRepository
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<ComplaintRepository> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ComplaintRepository(IConfiguration configuration, ILogger<ComplaintRepository> logger, IHttpContextAccessor httpContextAccessor)
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

    /* Output @Code / @Message → ResponseEntity. With optional @Id output → RecordID. */
    private async Task<ResponseEntity> ExecMutation(string sp, DynamicParameters p, bool withId = false)
    {
        using var conn = OpenConnection();
        p.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        p.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
        if (withId) p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

        await conn.ExecuteAsync(sp, p, commandType: CommandType.StoredProcedure);

        var code = p.Get<int?>("@Code") ?? 0;
        var msg = p.Get<string>("@Message") ?? string.Empty;
        var entity = new ResponseEntity { Code = code, Message = msg, IsSuccess = code == 200 };
        if (withId) entity.RecordID = (p.Get<int?>("@Id") ?? -1).ToString();
        return entity;
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

    /* ---------- Nature ---------- */

    public Task<ResponseEntity> UpsertNatureAsync(UpsertComplaintNatureDTO dto)
    {
        var p = new DynamicParameters();
        p.Add("@ComplaintNatureId", dto.ComplaintNatureId, DbType.Int32);
        p.Add("@Name", dto.Name, DbType.String, size: 120);
        p.Add("@ComplaintType", dto.ComplaintType, DbType.String, size: 20);
        p.Add("@DisplayOrder", dto.DisplayOrder, DbType.Int32);
        p.Add("@IsActive", dto.IsActive, DbType.Boolean);
        return ExecMutation("dbo.usp_UpsertComplaintNature", p, withId: true);
    }

    public Task<ListResponseWrapper<ComplaintNature>> GetNaturesAsync(bool activeOnly)
    {
        var p = new DynamicParameters();
        p.Add("@ActiveOnly", activeOnly, DbType.Boolean);
        return ListWithOutputs<ComplaintNature>("dbo.usp_GetComplaintNatures", p);
    }

    public Task<ResponseEntity> DeactivateNatureAsync(int complaintNatureId)
    {
        var p = new DynamicParameters();
        p.Add("@ComplaintNatureId", complaintNatureId, DbType.Int32);
        return ExecMutation("dbo.usp_DeactivateComplaintNature", p);
    }

    /* ---------- Complaint mutations ---------- */

    public Task<ResponseEntity> CreateComplaintAsync(CreateComplaintDTO dto)
    {
        var p = new DynamicParameters();
        p.Add("@LogonUserId", dto.LogonUserId, DbType.Int32);
        p.Add("@ComplaintNatureId", dto.ComplaintNatureId, DbType.Int32);
        p.Add("@ComplainantName", dto.ComplainantName, DbType.String, size: 200);
        p.Add("@ContactNumber", dto.ContactNumber, DbType.String, size: 40);
        p.Add("@ComplaintAgainst", dto.ComplaintAgainst, DbType.String, size: 200);
        p.Add("@Description", dto.Description, DbType.String, size: -1);
        p.Add("@AttachmentPath", dto.AttachmentPath, DbType.String, size: 400);
        p.Add("@AttachmentOriginalName", dto.AttachmentOriginalName, DbType.String, size: 260);
        return ExecMutation("dbo.usp_CreateComplaint", p, withId: true);
    }

    public Task<ResponseEntity> UpdateComplaintAsync(UpdateComplaintDTO dto)
    {
        var p = new DynamicParameters();
        p.Add("@ComplaintId", dto.ComplaintId, DbType.Int32);
        p.Add("@ActorUserId", dto.ActorUserId, DbType.Int32);
        p.Add("@ComplaintNatureId", dto.ComplaintNatureId, DbType.Int32);
        p.Add("@ComplainantName", dto.ComplainantName, DbType.String, size: 200);
        p.Add("@ContactNumber", dto.ContactNumber, DbType.String, size: 40);
        p.Add("@ComplaintAgainst", dto.ComplaintAgainst, DbType.String, size: 200);
        p.Add("@Description", dto.Description, DbType.String, size: -1);
        p.Add("@AttachmentPath", dto.AttachmentPath, DbType.String, size: 400);
        p.Add("@AttachmentOriginalName", dto.AttachmentOriginalName, DbType.String, size: 260);
        p.Add("@ClearAttachment", dto.ClearAttachment, DbType.Boolean);
        return ExecMutation("dbo.usp_UpdateComplaint", p);
    }

    public Task<ResponseEntity> ChangeStatusAsync(int complaintId, int actorUserId, string toStatus, string note)
    {
        var p = new DynamicParameters();
        p.Add("@ComplaintId", complaintId, DbType.Int32);
        p.Add("@ActorUserId", actorUserId, DbType.Int32);
        p.Add("@ToStatus", toStatus, DbType.String, size: 20);
        p.Add("@Note", note, DbType.String, size: 2000);
        return ExecMutation("dbo.usp_ChangeComplaintStatus", p);
    }

    public Task<ResponseEntity> AddNoteAsync(int complaintId, int actorUserId, string note)
    {
        var p = new DynamicParameters();
        p.Add("@ComplaintId", complaintId, DbType.Int32);
        p.Add("@ActorUserId", actorUserId, DbType.Int32);
        p.Add("@Note", note, DbType.String, size: 2000);
        return ExecMutation("dbo.usp_AddComplaintNote", p);
    }

    public Task<ResponseEntity> SoftDeleteAsync(int complaintId, int actorUserId, string reason)
    {
        var p = new DynamicParameters();
        p.Add("@ComplaintId", complaintId, DbType.Int32);
        p.Add("@ActorUserId", actorUserId, DbType.Int32);
        p.Add("@Reason", reason, DbType.String, size: 500);
        return ExecMutation("dbo.usp_SoftDeleteComplaint", p);
    }

    public Task<ResponseEntity> RestoreAsync(int complaintId, int actorUserId)
    {
        var p = new DynamicParameters();
        p.Add("@ComplaintId", complaintId, DbType.Int32);
        p.Add("@ActorUserId", actorUserId, DbType.Int32);
        return ExecMutation("dbo.usp_RestoreComplaint", p);
    }

    /* ---------- Reads ---------- */

    public Task<ListResponseWrapper<ComplaintRow>> GetMyComplaintsAsync(int logonUserId)
    {
        var p = new DynamicParameters();
        p.Add("@LogonUserId", logonUserId, DbType.Int32);
        return ListWithOutputs<ComplaintRow>("dbo.usp_GetMyComplaints", p);
    }

    public Task<ListResponseWrapper<ComplaintRow>> GetAllComplaintsAsync(GetComplaintsReportDTO filter)
    {
        var p = new DynamicParameters();
        p.Add("@FromDate", filter.FromDate, DbType.Date);
        p.Add("@ToDate", filter.ToDate, DbType.Date);
        p.Add("@ComplaintType", filter.ComplaintType, DbType.String, size: 20);
        p.Add("@ComplaintNatureId", filter.ComplaintNatureId, DbType.Int32);
        p.Add("@Status", filter.Status, DbType.String, size: 20);
        p.Add("@ComplainantLike", filter.ComplainantLike, DbType.String, size: 200);
        p.Add("@LogonUserId", filter.LogonUserId, DbType.Int32);
        p.Add("@HasAttachment", filter.HasAttachment, DbType.Boolean);
        p.Add("@IncludeDeleted", filter.IncludeDeleted, DbType.Boolean);
        return ListWithOutputs<ComplaintRow>("dbo.usp_GetAllComplaints", p);
    }

    public async Task<SingleResponseWrapper<ComplaintDetail>> GetComplaintDetailAsync(int complaintId, int callerUserId, bool isAdmin)
    {
        using var conn = OpenConnection();
        var p = new DynamicParameters();
        p.Add("@ComplaintId", complaintId, DbType.Int32);
        p.Add("@CallerUserId", callerUserId, DbType.Int32);
        p.Add("@IsAdmin", isAdmin, DbType.Boolean);
        p.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
        p.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

        using var multi = await conn.QueryMultipleAsync("dbo.usp_GetComplaintDetail", p, commandType: CommandType.StoredProcedure);

        var code = p.Get<int?>("@Code") ?? 0;
        var msg = p.Get<string>("@Message") ?? string.Empty;

        if (code != 200)
            return new SingleResponseWrapper<ComplaintDetail> { Code = code, Message = msg, Data = null! };

        var header = (await multi.ReadAsync<ComplaintRow>()).FirstOrDefault();
        var trail = (await multi.ReadAsync<ComplaintAuditEntry>()).ToList();

        // OUTPUT params land after result-sets are consumed.
        code = p.Get<int?>("@Code") ?? code;
        msg = p.Get<string>("@Message") ?? msg;

        return new SingleResponseWrapper<ComplaintDetail>
        {
            Code = code,
            Message = msg,
            Data = new ComplaintDetail { Header = header, AuditTrail = trail }
        };
    }
}
