using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
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
using CleanArc.Application.Features.DrivingAvailabilityOptions.Queries.GetDrivingAvailabilityOptionsById;

namespace CleanArc.Application.Features.DrivingAvailabilityOptions.Queries.GetDrivingAvailabilityOptionsById;

internal class GetDrivingAvailabilityOptionsByIdQueryHandler : IRequestHandler<GetDrivingAvailabilityOptionsByIdQuery, OperationResult<GetDrivingAvailabilityOptionsByIdQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetDrivingAvailabilityOptionsByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public GetDrivingAvailabilityOptionsByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetDrivingAvailabilityOptionsByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async ValueTask<OperationResult<GetDrivingAvailabilityOptionsByIdQueryResult>> Handle(GetDrivingAvailabilityOptionsByIdQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            //var DrivingAvailabilityOptions = await _unitOfWork.DrivingAvailabilityOptionsRepository.GetByIdAsync(request.searchRequestById);

            //if (DrivingAvailabilityOptions == null)
            //{
            //    return OperationResult<GetDrivingAvailabilityOptionsByIdQueryResult>.NotFoundResult("DrivingAvailabilityOptions not found");
            //}

            ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
            //var result = _mapper.Map<GetDrivingAvailabilityOptionsByIdQueryResult>(DrivingAvailabilityOptions);

            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

            //return OperationResult<GetDrivingAvailabilityOptionsByIdQueryResult>.SuccessResult(result);

            var response = await _unitOfWork.DrivingAvailabilityOptionsRepository.GetByIdAsync(request.searchRequestById);

            if (response.Code != 200)
            {
                return OperationResult<GetDrivingAvailabilityOptionsByIdQueryResult>.FailureResult(
                    response.Message,
                response.Code
                );
            }

            var mappedResult = _mapper.Map<GetDrivingAvailabilityOptionsByIdQueryResult>(response.Data);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            return OperationResult<GetDrivingAvailabilityOptionsByIdQueryResult>.SuccessResult(
                mappedResult,
                response.Code,
                response.Message
            );
        }
    }

   
}

