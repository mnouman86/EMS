using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.URL.Queries.GetURLById;
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
using CleanArc.Application.Features.ActivityType.Queries.GetActivityTypeById;

namespace CleanArc.Application.Features.ActivityTransportation.Queries.GetActivityTransportationById
{
    internal class GetActivityTransportationByIdQueryHandler : IRequestHandler<GetActivityTransportationByIdQuery, OperationResult<GetActivityTransportationByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetActivityTransportationByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetActivityTransportationByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetActivityTransportationByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetActivityTransportationByIdQueryResult>> Handle(GetActivityTransportationByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                //var ActivityTransportation = await _unitOfWork.ActivityTransportationRepository.GetByIdAsync(request.searchRequestById);

                //if (ActivityTransportation == null)
                //{
                //    return OperationResult<GetActivityTransportationByIdQueryResult>.NotFoundResult("ActivityTransportation not found");
                //}

                ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                //var result = _mapper.Map<GetActivityTransportationByIdQueryResult>(ActivityTransportation);

                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                //return OperationResult<GetActivityTransportationByIdQueryResult>.SuccessResult(result);

                var response = await _unitOfWork.ActivityTransportationRepository.GetByIdAsync(request.searchRequestById);

                if (response.Code != 200)
                {
                    return OperationResult<GetActivityTransportationByIdQueryResult>.FailureResult(
                        response.Message,
                    response.Code
                    );
                }

                var mappedResult = _mapper.Map<GetActivityTransportationByIdQueryResult>(response.Data);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                return OperationResult<GetActivityTransportationByIdQueryResult>.SuccessResult(
                    mappedResult,
                    response.Code,
                    response.Message
                );
            }
        }

        //public ValueTask<OperationResult<GetActivityTransportationByIdQueryResult>> Handle(GetActivityTransportationByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
