using CleanArc.Application.Common;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Parent;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface IParentRepository
    {
        Task<ResponseEntity> LinkAsync(int userId, int studentId, string? relationship, int? createdBy);
        Task<ResponseEntity> UnlinkAsync(int userId, int studentId);
        Task<ListResponseWrapper<ParentChildRow>> GetChildrenAsync(int userId);
        Task<bool> IsLinkedAsync(int userId, int studentId);
        Task<ListResponseWrapper<StudentPaymentRow>> GetStudentPaymentsAsync(int studentId);
    }
}
