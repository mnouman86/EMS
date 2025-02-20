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
using CleanArc.Application.Features.Country.Queries.GetCountryById;
using Serilog.Core;

namespace CleanArc.Application.Features.Advertisement.Queries.GetAdvertisementById
{
    internal class GetAdvertisementByIdQueryHandler : IRequestHandler<GetAdvertisementByIdQuery, OperationResult<GetAdvertisementByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAdvertisementByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetAdvertisementByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetAdvertisementByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetAdvertisementByIdQueryResult>> Handle(GetAdvertisementByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
           

			{
				var response = await _unitOfWork.AdvertisementRepository.GetByIdAsync(request.Id);

				if (response.Code != 200)
				{
					return OperationResult<GetAdvertisementByIdQueryResult>.FailureResult(
					response.Message,
						response.Code
					);
				}

				var mappedResult = _mapper.Map<GetAdvertisementByIdQueryResult>(response.Data);
				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

				return OperationResult<GetAdvertisementByIdQueryResult>.SuccessResult(
					mappedResult,
					response.Code,
					response.Message
				);

				if (mappedResult == null)
				{
					return OperationResult<GetAdvertisementByIdQueryResult>.NotFoundResult("Ad not found");
				}

			}
		}

       
    }
}
