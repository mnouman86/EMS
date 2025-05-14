using CleanArc.Application.Contracts.Persistence;

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
using CleanArc.Application.Features.Activity.Queries.GetAllActivity;

namespace CleanArc.Application.Features.SearchHotelRoomDetail.Queries.GetAllSearchHotelRoomDetail;

internal class GetAllSearchHotelRoomDetailQueryHandler: IRequestHandler<GetAllSearchHotelRoomDetailQuery, OperationResult<List<GetAllSearchHotelRoomDetailQueryResult>>>
    {

        private readonly IUnitOfWork _unitOfWork;
private readonly IMapper _mapper;
private readonly ILogger<GetAllSearchHotelRoomDetailQueryHandler> _logger;
private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor


public GetAllSearchHotelRoomDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<GetAllSearchHotelRoomDetailQueryHandler> logger)
{
    _unitOfWork = unitOfWork;
    _mapper = mapper;
    _httpContextAccessor = httpContextAccessor;
    _logger = logger;
}

public async ValueTask<OperationResult<List<GetAllSearchHotelRoomDetailQueryResult>>> Handle(GetAllSearchHotelRoomDetailQuery request, CancellationToken cancellationToken)
{
    using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
    {
        //var searchHotel = 

        ////var resultCheck = uRLs.Select(c => new GetAllProductsQueryResult(c.Id, c.Path, c.Title, c.Description)).ToList();
        //var result = _mapper.Map<List<GetAllSearchHotelRoomDetailQueryResult>>(searchHotel);
        //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
        //return OperationResult<List<GetAllSearchHotelRoomDetailQueryResult>>.SuccessResult(result);

            var response = await _unitOfWork.SearchHotelRoomDetailRepository.GetAllAsync(request.searchRequest);

            if (response.Code != 200)
            {
                return OperationResult<List<GetAllSearchHotelRoomDetailQueryResult>>.FailureResult(
                    response.Message,
                response.Code
                );
            }

            var mappedResult = _mapper.Map<List<GetAllSearchHotelRoomDetailQueryResult>>(response.Data);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            return OperationResult<List<GetAllSearchHotelRoomDetailQueryResult>>.SuccessResult(
                mappedResult,
                response.Code,
                response.Message,
                    response.TotalCount
            );
        }
}
}

