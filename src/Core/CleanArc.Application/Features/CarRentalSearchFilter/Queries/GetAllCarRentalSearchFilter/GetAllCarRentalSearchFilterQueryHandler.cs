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

namespace CleanArc.Application.Features.CarRentalSearchFilter.Queries.GetAllCarRentalSearchFilter;

internal class GetAllCarRentalSearchFilterQueryHandler: IRequestHandler<GetAllCarRentalSearchFilterQuery, OperationResult<List<GetAllCarRentalSearchFilterQueryResult>>>
    {

        private readonly IUnitOfWork _unitOfWork;
private readonly IMapper _mapper;
private readonly ILogger<GetAllCarRentalSearchFilterQueryHandler> _logger;
private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor


public GetAllCarRentalSearchFilterQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<GetAllCarRentalSearchFilterQueryHandler> logger)
{
    _unitOfWork = unitOfWork;
    _mapper = mapper;
    _httpContextAccessor = httpContextAccessor;
    _logger = logger;
}

public async ValueTask<OperationResult<List<GetAllCarRentalSearchFilterQueryResult>>> Handle(GetAllCarRentalSearchFilterQuery request, CancellationToken cancellationToken)
{
    using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
    {
        //var searchcar = await _unitOfWork.CarRentalSearchFilterRepository.GetAllWithParamAsync(request.searchRequest);

        ////var resultCheck = uRLs.Select(c => new GetAllProductsQueryResult(c.Id, c.Path, c.Title, c.Description)).ToList();
        //var result = _mapper.Map<List<GetAllCarRentalSearchFilterQueryResult>>(searchcar);
        //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
        //return OperationResult<List<GetAllCarRentalSearchFilterQueryResult>>.SuccessResult(result);

            var response = await _unitOfWork.CarRentalSearchFilterRepository.GetAllWithParamAsync(request.searchRequest);

            //if (response.Code != 200)
            //{
            //    return OperationResult<List<GetAllCarRentalSearchFilterQueryResult>>.FailureResult(
            //        response.Message,
            //    response.Code
            //    );
            //}

            var mappedResult = _mapper.Map<List<GetAllCarRentalSearchFilterQueryResult>>(response.Data);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            return OperationResult<List<GetAllCarRentalSearchFilterQueryResult>>.SuccessResult(
                mappedResult,
                response.Code,
                response.Message,
                    response.TotalCount
            );
        }
}
}

