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

namespace CleanArc.Application.Features.SearchHotelImage.Queries.GetAllSearchHotelImage;

internal class GetAllSearchHotelImageQueryHandler : IRequestHandler<GetAllSearchHotelImageQuery, OperationResult<List<GetAllSearchHotelImageQueryResult>>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllSearchHotelImageQueryHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor


    public GetAllSearchHotelImageQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<GetAllSearchHotelImageQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async ValueTask<OperationResult<List<GetAllSearchHotelImageQueryResult>>> Handle(GetAllSearchHotelImageQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            var searchHotel = await _unitOfWork.SearchHotelImageRepository.GetAllAsync(request.searchRequest);

            //var resultCheck = uRLs.Select(c => new GetAllProductsQueryResult(c.Id, c.Path, c.Title, c.Description)).ToList();
            //var result = _mapper.Map<List<GetAllSearchHotelImageQueryResult>>(searchHotel);
            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
            //return OperationResult<List<GetAllSearchHotelImageQueryResult>>.SuccessResult(result);

            var response = await _unitOfWork.SearchHotelImageRepository.GetAllAsync(request.searchRequest);

            if (response.Code != 200)
            {
                return OperationResult<List<GetAllSearchHotelImageQueryResult>>.FailureResult(
                    response.Message,
                response.Code
                );
            }

            var mappedResult = _mapper.Map<List<GetAllSearchHotelImageQueryResult>>(response.Data);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            return OperationResult<List<GetAllSearchHotelImageQueryResult>>.SuccessResult(
                mappedResult,
                response.Code,
                response.Message
            );
        }
    }
}



