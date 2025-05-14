using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.SearchHotelRoomDetail.Queries.GetAllSearchHotelRoomDetail;
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

namespace CleanArc.Application.Features.SearchRoomAmenities.Queries.GetAllSearchRoomAmenities;

internal class GetAllSearchRoomAmenitiesQueryHandler: IRequestHandler<GetAllSearchRoomAmenitiesQuery, OperationResult<List<GetAllSearchRoomAmenitiesQueryResult>>>
    {

        private readonly IUnitOfWork _unitOfWork;
private readonly IMapper _mapper;
private readonly ILogger<GetAllSearchRoomAmenitiesQueryHandler> _logger;
private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor


public GetAllSearchRoomAmenitiesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<GetAllSearchRoomAmenitiesQueryHandler> logger)
{
    _unitOfWork = unitOfWork;
    _mapper = mapper;
    _httpContextAccessor = httpContextAccessor;
    _logger = logger;
}

public async ValueTask<OperationResult<List<GetAllSearchRoomAmenitiesQueryResult>>> Handle(GetAllSearchRoomAmenitiesQuery request, CancellationToken cancellationToken)
{
    using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
    {
        //var roomAmenities = 

        ////var resultCheck = uRLs.Select(c => new GetAllProductsQueryResult(c.Id, c.Path, c.Title, c.Description)).ToList();
        //var result = _mapper.Map<List<GetAllSearchRoomAmenitiesQueryResult>>(roomAmenities);
        //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
        //return OperationResult<List<GetAllSearchRoomAmenitiesQueryResult>>.SuccessResult(result);

            var response = await _unitOfWork.SearchRoomAmenitiesRepository.GetAllAsync(request.searchRequest);

            if (response.Code != 200)
            {
                return OperationResult<List<GetAllSearchRoomAmenitiesQueryResult>>.FailureResult(
                    response.Message,
                response.Code
                );
            }

            var mappedResult = _mapper.Map<List<GetAllSearchRoomAmenitiesQueryResult>>(response.Data);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            return OperationResult<List<GetAllSearchRoomAmenitiesQueryResult>>.SuccessResult(
                mappedResult,
                response.Code,
                response.Message,
                    response.TotalCount
            );
        }
}

    //public ValueTask<OperationResult<List<GetAllSearchRoomAmenitiesQueryResult>>> Handle(GetAllSearchRoomAmenitiesQuery request, CancellationToken cancellationToken)
    //{
    //    throw new NotImplementedException();
    //}
}
