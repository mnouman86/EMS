using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.Result.Queries.GetMarksEntryGrid;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Result;
using MapsterMapper;
using Mediator;

namespace CleanArc.Application.Features.Result.Queries.GetStudentResultCard;

internal class GetStudentResultCardQueryHandler : IRequestHandler<GetStudentResultCardQuery, OperationResult<GetStudentResultCardQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITeacherScopeContext _scope;

    public GetStudentResultCardQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ITeacherScopeContext scope)
    {
        _unitOfWork = unitOfWork; _mapper = mapper; _scope = scope;
    }

    public async ValueTask<OperationResult<GetStudentResultCardQueryResult>> Handle(GetStudentResultCardQuery request, CancellationToken cancellationToken)
    {
        // Results module = assigned-subject classes (ST scope).
        if (_scope.IsTeacherScoped && !await _scope.OwnsStudentAsync(request.StudentId, TeacherScopeKind.Subject))
            return OperationResult<GetStudentResultCardQueryResult>.FailureResult("Not authorized for this student.", 403);

        var req = new StudentCardRequest { ResultSessionId = request.ResultSessionId, StudentId = request.StudentId };

        var cardRes = await _unitOfWork.ResultRepository.GetStudentResultCardAsync(req);
        if (cardRes.Code != 200 || cardRes.Data == null)
            return OperationResult<GetStudentResultCardQueryResult>.FailureResult(cardRes.Message ?? "Not found", cardRes.Code);

        var marksRes = await _unitOfWork.ResultRepository.GetStudentResultMarksAsync(req);

        var card = _mapper.Map<GetStudentResultCardQueryResult>(cardRes.Data);
        card.Marks = _mapper.Map<List<MarksEntryGridRow>>(marksRes.Data) ?? new();
        card.FileNameSuggestion = $"TSSS_{card.StudentCode}_{card.SessionName?.Replace(' ', '_')}_Result.pdf";

        return OperationResult<GetStudentResultCardQueryResult>.SuccessResult(card);
    }
}
