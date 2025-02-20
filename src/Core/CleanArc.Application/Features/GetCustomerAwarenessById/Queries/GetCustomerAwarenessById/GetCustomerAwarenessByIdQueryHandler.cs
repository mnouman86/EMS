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
using CleanArc.Application.Features.GroupActivityParticipants.Queries.GetGroupActivityParticipantsById;

namespace CleanArc.Application.Features.CustomerAwareness.Queries.GetCustomerAwarenessById;

internal class GetCustomerAwarenessByIdQueryHandler : IRequestHandler<GetCustomerAwarenessByIdQuery, OperationResult<GetCustomerAwarenessByIdQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetCustomerAwarenessByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public GetCustomerAwarenessByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetCustomerAwarenessByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async ValueTask<OperationResult<GetCustomerAwarenessByIdQueryResult>> Handle(GetCustomerAwarenessByIdQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            //var CustomerAwareness = await _unitOfWork.CustomerAwarenessRepository.GetByIdAsync(request.Id);

            //if (CustomerAwareness == null)
            //{
            //    return OperationResult<GetCustomerAwarenessByIdQueryResult>.NotFoundResult("CustomerAwareness not found");
            //}

            ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
            //var result = _mapper.Map<GetCustomerAwarenessByIdQueryResult>(CustomerAwareness);

            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

            //return OperationResult<GetCustomerAwarenessByIdQueryResult>.SuccessResult(result);


            var response = await _unitOfWork.CustomerAwarenessRepository.GetByIdAsync(request.Id);

            if (response.Code != 200)
            {
                return OperationResult<GetCustomerAwarenessByIdQueryResult>.FailureResult(
                    response.Message,
                response.Code
                );
            }

            var mappedResult = _mapper.Map<GetCustomerAwarenessByIdQueryResult>(response.Data);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            return OperationResult<GetCustomerAwarenessByIdQueryResult>.SuccessResult(
                mappedResult,
                response.Code,
                response.Message
            );
        }
    }

    //public ValueTask<OperationResult<GetAgeTypeByIdQueryResult>> Handle(GetAgeTypeByIdQuery request, CancellationToken cancellationToken)
    //{
    //    throw new NotImplementedException();
    //}
}

