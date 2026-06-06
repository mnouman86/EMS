using CleanArc.Application.Models.Student;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Student;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<ResponseEntity> ChangeStatusAsync(ChangeStudentStatusDTO dto);
        Task<ResponseEntity> BulkImportAsync(BulkImportStudentsDTO dto);
        Task<ResponseEntity> PromoteAsync(PromoteStudentsDTO dto);
    }
}
