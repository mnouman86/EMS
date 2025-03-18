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
using CleanArc.Application.Features.ActivityTransportation.Queries.GetActivityTransportationById;

namespace CleanArc.Application.Features.ActivitySeasonMapping.Queries.GetActivitySeasonMappingById
{
    internal class GetActivitySeasonMappingByIdQueryHandler : IRequestHandler<GetActivitySeasonMappingByIdQuery, OperationResult<GetActivitySeasonMappingByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetActivitySeasonMappingByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetActivitySeasonMappingByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetActivitySeasonMappingByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetActivitySeasonMappingByIdQueryResult>> Handle(GetActivitySeasonMappingByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                //var ActivitySeasonMapping = await _unitOfWork.ActivitySeasonMappingRepository.GetByIdAsync(request.searchRequestById);

                //if (ActivitySeasonMapping == null)
                //{
                //    return OperationResult<GetActivitySeasonMappingByIdQueryResult>.NotFoundResult("ActivitySeasonMapping not found");
                //}

                ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                //var result = _mapper.Map<GetActivitySeasonMappingByIdQueryResult>(ActivitySeasonMapping);

                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                //return OperationResult<GetActivitySeasonMappingByIdQueryResult>.SuccessResult(result);

                var response = await _unitOfWork.ActivitySeasonMappingRepository.GetByIdAsync(request.searchRequestById);

                if (response.Code != 200)
                {
                    return OperationResult<GetActivitySeasonMappingByIdQueryResult>.FailureResult(
                        response.Message,
                    response.Code
                    );
                }

                var mappedResult = _mapper.Map<GetActivitySeasonMappingByIdQueryResult>(response.Data);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                return OperationResult<GetActivitySeasonMappingByIdQueryResult>.SuccessResult(
                    mappedResult,
                    response.Code,
                    response.Message
                );
            }
        }

        //public ValueTask<OperationResult<GetActivitySeasonMappingByIdQueryResult>> Handle(GetActivitySeasonMappingByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
