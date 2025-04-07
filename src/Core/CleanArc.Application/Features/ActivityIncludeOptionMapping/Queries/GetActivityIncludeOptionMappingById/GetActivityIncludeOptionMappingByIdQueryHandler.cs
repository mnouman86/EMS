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
using CleanArc.Application.Features.ActivitySupervisor.Queries.GetActivitySupervisorById;

namespace CleanArc.Application.Features.ActivityIncludeOptionMapping.Queries.GetActivityIncludeOptionMappingById
{
    internal class GetActivityIncludeOptionMappingByIdQueryHandler : IRequestHandler<GetActivityIncludeOptionMappingByIdQuery, OperationResult<GetActivityIncludeOptionMappingByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetActivityIncludeOptionMappingByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetActivityIncludeOptionMappingByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetActivityIncludeOptionMappingByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetActivityIncludeOptionMappingByIdQueryResult>> Handle(GetActivityIncludeOptionMappingByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                //var ActivityIncludeOptionMapping = await _unitOfWork.ActivityIncludeOptionMappingRepository.GetByIdAsync(request.searchRequestById);

                //if (ActivityIncludeOptionMapping == null)
                //{
                //    return OperationResult<GetActivityIncludeOptionMappingByIdQueryResult>.NotFoundResult("ActivityIncludeOptionMapping not found");
                //}

                ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                //var result = _mapper.Map<GetActivityIncludeOptionMappingByIdQueryResult>(ActivityIncludeOptionMapping);

                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                //return OperationResult<GetActivityIncludeOptionMappingByIdQueryResult>.SuccessResult(result);

                var response = await _unitOfWork.ActivityIncludeOptionMappingRepository.GetByIdAsync(request.searchRequestById);

                if (response.Code != 200)
                {
                    return OperationResult<GetActivityIncludeOptionMappingByIdQueryResult>.FailureResult(
                        response.Message,
                    response.Code
                    );
                }

                var mappedResult = _mapper.Map<GetActivityIncludeOptionMappingByIdQueryResult>(response.Data);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                return OperationResult<GetActivityIncludeOptionMappingByIdQueryResult>.SuccessResult(
                    mappedResult,
                    response.Code,
                    response.Message
                );
            }
        }

        //public ValueTask<OperationResult<GetActivityIncludeOptionMappingByIdQueryResult>> Handle(GetActivityIncludeOptionMappingByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
