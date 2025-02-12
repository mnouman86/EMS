using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging; using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArc.SharedKernel.Extensions;
using CleanArc.Application.Features.Activity.Queries.GetAllActivity;

namespace CleanArc.Application.Features.MappingHotelAmenity.Queries.GetAllMappingHotelAmenity;

internal class GetAllMappingHotelAmenityQueryHandler : IRequestHandler<GetAllMappingHotelAmenityQuery, OperationResult<List<GetAllMappingHotelAmenityQueryResult>>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllMappingHotelAmenityQueryHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor


    public GetAllMappingHotelAmenityQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<GetAllMappingHotelAmenityQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;

    }

    public async ValueTask<OperationResult<List<GetAllMappingHotelAmenityQueryResult>>> Handle(GetAllMappingHotelAmenityQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            //var Product = await _unitOfWork.AgeTypeRepository.GetAllAsync(request.searchRequest);

            //var resultCheck = uRLs.Select(c => new GetAllProductsQueryResult(c.Id, c.Path, c.Title, c.Description)).ToList();
            //var result = _mapper.Map<List<GetAllMappingHotelAmenityQueryResult>>(Product);
            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
            //return OperationResult<List<GetAllMappingHotelAmenityQueryResult>>.SuccessResult(result);

            var response = await _unitOfWork.AgeTypeRepository.GetAllAsync(request.searchRequest);

            if (response.Code != 200)
            {
                return OperationResult<List<GetAllMappingHotelAmenityQueryResult>>.FailureResult(
                    response.Message,
                response.Code
                );
            }

            var mappedResult = _mapper.Map<List<GetAllMappingHotelAmenityQueryResult>>(response.Data);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            return OperationResult<List<GetAllMappingHotelAmenityQueryResult>>.SuccessResult(
                mappedResult,
                response.Code,
                response.Message
            );
        }
    }
}



