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

namespace CleanArc.Application.Features.City.Queries.GetAllCities;

internal class GetAllCitiesQueryHandler : IRequestHandler<GetAllCitiesQuery, OperationResult<List<GetAllCitiesQueryResult>>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllCitiesQueryHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor


    public GetAllCitiesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<GetAllCitiesQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;

    }

    public async ValueTask<OperationResult<List<GetAllCitiesQueryResult>>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            //var cities = await _unitOfWork.CityRepository.GetAllAsync(request.searchRequest);

            ////var resultCheck = uRLs.Select(c => new GetAllProductsQueryResult(c.Id, c.Path, c.Title, c.Description)).ToList();
            //var result = _mapper.Map<List<GetAllCitiesQueryResult>>(cities);
            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
            //return OperationResult<List<GetAllCitiesQueryResult>>.SuccessResult(result);

            var response = await _unitOfWork.CityRepository.GetAllAsync(request.searchRequest);

            if (response.Code != 200)
            {
                return OperationResult<List<GetAllCitiesQueryResult>>.FailureResult(
                    response.Message,
                response.Code
                );
            }

            var mappedResult = _mapper.Map<List<GetAllCitiesQueryResult>>(response.Data);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            return OperationResult<List<GetAllCitiesQueryResult>>.SuccessResult(
                mappedResult,
                response.Code,
                response.Message
            );
        }
    }
}



