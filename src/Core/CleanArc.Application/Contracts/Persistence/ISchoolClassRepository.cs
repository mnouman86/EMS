using CleanArc.Application.Models.SchoolClass;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.SchoolClass;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface ISchoolClassRepository : IRepository<SchoolClass>
    {
        Task<ResponseEntity> AssignClassTeacherAsync(AssignClassTeacherDTO dto);
    }
}
