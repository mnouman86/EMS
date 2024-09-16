using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.SearchHotel.Queries.GetAllSearchHotels;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging; using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchBusinessCarDetail.Queries.GetAllSearchBusinessCarDetail;

internal class GetAllSearchBusinessCarDetailQueryHandler: IRequestHandler<GetAllSearchBusinessCarDetailQuery, OperationResult<List<GetAllSearchBusinessCarDetailQueryResult>>>
    {

        private readonly IUnitOfWork _unitOfWork;
private readonly IMapper _mapper;
private readonly ILogger<GetAllSearchBusinessCarDetailQueryHandler> _logger;
private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor


public GetAllSearchBusinessCarDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<GetAllSearchBusinessCarDetailQueryHandler> logger)
{
    _unitOfWork = unitOfWork;
    _mapper = mapper;
    _httpContextAccessor = httpContextAccessor;
    _logger = logger;
}

public async ValueTask<OperationResult<List<GetAllSearchBusinessCarDetailQueryResult>>> Handle(GetAllSearchBusinessCarDetailQuery request, CancellationToken cancellationToken)
{
    using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
    {
        var searchHotel = await _unitOfWork.SearchBusinessCarDetailRepository.GetAllAsync(request.searchRequest);

        //var resultCheck = uRLs.Select(c => new GetAllProductsQueryResult(c.Id, c.Path, c.Title, c.Description)).ToList();
        var result = _mapper.Map<List<GetAllSearchBusinessCarDetailQueryResult>>(searchHotel);
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
        return OperationResult<List<GetAllSearchBusinessCarDetailQueryResult>>.SuccessResult(result);
    }
}
}

