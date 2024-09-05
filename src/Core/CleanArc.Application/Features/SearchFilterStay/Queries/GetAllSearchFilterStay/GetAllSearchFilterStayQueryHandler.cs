using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.SearchFilterStay.Queries.GetAllSearchFilterStay;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchFilterStay.Queries.GetAllSearchFilterStay
{
    internal class GetAllSearchFilterStayQueryHandler :  IRequestHandler<GetAllSearchFilterStayQuery, OperationResult<List<GetAllSearchFilterStayQueryResult>>>
    {

        private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllSearchFilterStayQueryHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor


    public GetAllSearchFilterStayQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<GetAllSearchFilterStayQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async ValueTask<OperationResult<List<GetAllSearchFilterStayQueryResult>>> Handle(GetAllSearchFilterStayQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            var SearchFilterStay = await _unitOfWork.SearchFilterStayRepository.GetAllWithParamAsync(request.searchRequestStays);

            //var resultCheck = uRLs.Select(c => new GetAllProductsQueryResult(c.Id, c.Path, c.Title, c.Description)).ToList();
            var result = _mapper.Map<List<GetAllSearchFilterStayQueryResult>>(SearchFilterStay);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
            return OperationResult<List<GetAllSearchFilterStayQueryResult>>.SuccessResult(result);
        }
    }
}

}

    
