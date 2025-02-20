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
using CleanArc.Application.Features.Bank.Queries.GetBankById;
using Serilog.Core;

namespace CleanArc.Application.Features.ActivityImageMapping.Queries.GetActivityImageMappingById
{
    internal class GetActivityImageMappingByIdQueryHandler : IRequestHandler<GetActivityImageMappingByIdQuery, OperationResult<GetActivityImageMappingByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetActivityImageMappingByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetActivityImageMappingByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetActivityImageMappingByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetActivityImageMappingByIdQueryResult>> Handle(GetActivityImageMappingByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            

			{
				var response = await _unitOfWork.ActivityImageMappingRepository.GetByIdAsync(request.Id);

				if (response.Code != 200)
				{
					return OperationResult<GetActivityImageMappingByIdQueryResult>.FailureResult(
					response.Message,
						response.Code
					);
				}

				var mappedResult = _mapper.Map<GetActivityImageMappingByIdQueryResult>(response.Data);
				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

				return OperationResult<GetActivityImageMappingByIdQueryResult>.SuccessResult(
					mappedResult,
					response.Code,
					response.Message
				);

				if (mappedResult == null)
				{
					return OperationResult<GetActivityImageMappingByIdQueryResult>.NotFoundResult("Image  not found");
				}

			}
		}

       
    }
}
