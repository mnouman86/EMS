using AutoMapper;
using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.SearchAutoComplete.Queries.GetSearchAutoComplete;
using CleanArc.Application.Features.SearchBusinessCarDetail.Queries.GetSearchBusinessCarDetailById;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.UserProfile;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.User;
using CleanArc.SharedKernel.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace CleanArc.Application.Features.SearchAutoComplete.Queries.SearchAutoComplete;

internal class GetSearchAutoCompleteQueryHandler : IRequestHandler<GetSearchAutoCompleteQuery, OperationResult<List<GetSearchAutoCompleteQueryResult>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetSearchAutoCompleteQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IAppUserManager _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetSearchAutoCompleteQueryHandler(IUnitOfWork unitOfWork, ILogger<GetSearchAutoCompleteQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;

    }

   
    public async ValueTask<OperationResult<List<GetSearchAutoCompleteQueryResult>>> Handle(GetSearchAutoCompleteQuery request, CancellationToken cancellationToken)
    {

        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            var response = await _unitOfWork.SearchAutoCompleteRepository.GetSearchAutoCompleteAsync(request.searchRequestById);

            if (response.Code != 200)
            {
                return OperationResult<List<GetSearchAutoCompleteQueryResult>>.FailureResult(
                    response.Message,
                    response.Code
                );
            }

            var mappedResult = response.Data?.Select(x => new GetSearchAutoCompleteQueryResult
            {
                Name = x.Name ?? string.Empty
            }).ToList() ?? new List<GetSearchAutoCompleteQueryResult>();
            //var mappedResult = _mapper.Map<List<GetSearchAutoCompleteQueryResult>>(response.Data.ToList());

            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            //return OperationResult<GetKBDetailByIdAllQueryResult>.SuccessResult(result);
            return OperationResult<List<GetSearchAutoCompleteQueryResult>>.SuccessResult(
                mappedResult,
                response.Code,
                response.Message
            );
        }
    }
}