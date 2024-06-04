using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.SearchHotel.Queries.GetAllSearchHotels;
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

namespace CleanArc.Application.Features.SearchHotelAmenities.Queries.GetAllSearchHotelAmenities;

internal class GetAllSearchHotelAmenitiesQueryHandler : IRequestHandler<GetAllSearchHotelAmenitiesQuery, OperationResult<List<GetAllSearchHotelAmenitiesQueryResult>>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllSearchHotelAmenitiesQueryHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor


    public GetAllSearchHotelAmenitiesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<GetAllSearchHotelAmenitiesQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async ValueTask<OperationResult<List<GetAllSearchHotelAmenitiesQueryResult>>> Handle(GetAllSearchHotelAmenitiesQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            var searchHotelAmenity = await _unitOfWork.SearchHotelAmenitiesRepository.GetAllAsync(request.searchRequest);

            //var resultCheck = uRLs.Select(c => new GetAllProductsQueryResult(c.Id, c.Path, c.Title, c.Description)).ToList();
            var result = _mapper.Map<List<GetAllSearchHotelAmenitiesQueryResult>>(searchHotelAmenity);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
            return OperationResult<List<GetAllSearchHotelAmenitiesQueryResult>>.SuccessResult(result);
        }
    }
}

