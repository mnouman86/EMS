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

namespace CleanArc.Application.Features.ActivityDisabilityMapping.Queries.GetActivityDisabilityMappingById
{
    internal class GetActivityDisabilityMappingByIdQueryHandler : IRequestHandler<GetActivityDisabilityMappingByIdQuery, OperationResult<GetActivityDisabilityMappingByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetActivityDisabilityMappingByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetActivityDisabilityMappingByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetActivityDisabilityMappingByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetActivityDisabilityMappingByIdQueryResult>> Handle(GetActivityDisabilityMappingByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var ActivityDisabilityMapping = await _unitOfWork.ActivityDisabilityMappingRepository.GetByIdAsync(request.Id);

                if (ActivityDisabilityMapping == null)
                {
                    return OperationResult<GetActivityDisabilityMappingByIdQueryResult>.NotFoundResult("ActivityDisabilityMapping not found");
                }

                //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                var result = _mapper.Map<GetActivityDisabilityMappingByIdQueryResult>(ActivityDisabilityMapping);

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                return OperationResult<GetActivityDisabilityMappingByIdQueryResult>.SuccessResult(result);
            }
        }

        //public ValueTask<OperationResult<GetActivityDisabilityMappingByIdQueryResult>> Handle(GetActivityDisabilityMappingByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
