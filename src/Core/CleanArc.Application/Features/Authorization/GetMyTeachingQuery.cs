using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Entities.Authorization;
using Mediator;

namespace CleanArc.Application.Features.Authorization
{
    /// <summary>
    /// The teacher landing page query. Returns three lists:
    ///   ClassesIHead — classes where I am the class teacher.
    ///   MySubjects   — distinct subjects I teach (any class).
    ///   ClassSubjectMap — every (class, subject) pair I teach.
    ///
    /// Admins/principals invoking this still get useful data (their own
    /// class-teacher rows + assignments, if any). It is harmless either way
    /// — the SP only returns rows for an Employee linked to the calling
    /// UserId via email, which is exactly what we want.
    /// </summary>
    public record GetMyTeachingQuery(int UserId) : IRequest<OperationResult<MyTeachingBundle>>;

    internal class GetMyTeachingQueryHandler : IRequestHandler<GetMyTeachingQuery, OperationResult<MyTeachingBundle>>
    {
        private readonly IUnitOfWork _u;
        public GetMyTeachingQueryHandler(IUnitOfWork u) { _u = u; }
        public async ValueTask<OperationResult<MyTeachingBundle>> Handle(GetMyTeachingQuery r, CancellationToken ct)
        {
            var bundle = await _u.TeacherScopeRepository.GetMyTeachingAsync(r.UserId);
            return OperationResult<MyTeachingBundle>.SuccessResult(bundle);
        }
    }
}
