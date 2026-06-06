using CleanArc.Application.Common;
using CleanArc.Application.Models.Request;
using CleanArc.Application.Models.Subject;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Subject;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface ISubjectRepository : IRepository<Subject>
    {
        Task<ResponseEntity> MapSubjectsToClassAsync(MapSubjectsToClassDTO dto);
        Task<ListResponseWrapper<SchoolClassSubject>> GetSubjectsByClassAsync(SearchRequestById request);
    }
}
