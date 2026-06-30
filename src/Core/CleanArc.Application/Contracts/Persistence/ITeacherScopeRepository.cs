using CleanArc.Application.Common;
using CleanArc.Domain.Entities.Authorization;

namespace CleanArc.Application.Contracts.Persistence
{
    public interface ITeacherScopeRepository
    {
        Task<ListResponseWrapper<TeacherClassScopeRow>> GetClassScopeAsync(int userId);
        Task<MyTeachingBundle> GetMyTeachingAsync(int userId);
    }
}
