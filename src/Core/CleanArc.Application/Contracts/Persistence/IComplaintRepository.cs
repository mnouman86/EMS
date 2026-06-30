using CleanArc.Application.Common;
using CleanArc.Application.Models.Complaint;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Complaint;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface IComplaintRepository
    {
        /* Nature catalogue */
        Task<ResponseEntity> UpsertNatureAsync(UpsertComplaintNatureDTO dto);
        Task<ListResponseWrapper<ComplaintNature>> GetNaturesAsync(bool activeOnly);
        Task<ResponseEntity> DeactivateNatureAsync(int complaintNatureId);

        /* Complaint */
        Task<ResponseEntity> CreateComplaintAsync(CreateComplaintDTO dto);
        Task<ResponseEntity> UpdateComplaintAsync(UpdateComplaintDTO dto);
        Task<ResponseEntity> ChangeStatusAsync(int complaintId, int actorUserId, string toStatus, string note);
        Task<ResponseEntity> AddNoteAsync(int complaintId, int actorUserId, string note);
        Task<ResponseEntity> SoftDeleteAsync(int complaintId, int actorUserId, string reason);
        Task<ResponseEntity> RestoreAsync(int complaintId, int actorUserId);

        Task<ListResponseWrapper<ComplaintRow>> GetMyComplaintsAsync(int logonUserId);
        Task<ListResponseWrapper<ComplaintRow>> GetAllComplaintsAsync(GetComplaintsReportDTO filter);
        Task<SingleResponseWrapper<ComplaintDetail>> GetComplaintDetailAsync(int complaintId, int callerUserId, bool isAdmin);
    }
}
