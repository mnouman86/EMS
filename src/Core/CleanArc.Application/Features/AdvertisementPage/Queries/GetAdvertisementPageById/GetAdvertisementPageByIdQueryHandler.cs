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

namespace CleanArc.Application.Features.AdvertisementPage.Queries.GetAdvertisementPageById
{
    internal class GetAdvertisementPageByIdQueryHandler : IRequestHandler<GetAdvertisementPageByIdQuery, OperationResult<GetAdvertisementPageByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAdvertisementPageByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetAdvertisementPageByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetAdvertisementPageByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetAdvertisementPageByIdQueryResult>> Handle(GetAdvertisementPageByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
          

			{
				var response = await _unitOfWork.AdvertisementPageRepository.GetByIdAsync(request.Id);

				if (response.Code != 200)
				{
					return OperationResult<GetAdvertisementPageByIdQueryResult>.FailureResult(
					response.Message,
						response.Code
					);
				}

				var mappedResult = _mapper.Map<GetAdvertisementPageByIdQueryResult>(response.Data);
				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

				return OperationResult<GetAdvertisementPageByIdQueryResult>.SuccessResult(
					mappedResult,
					response.Code,
					response.Message
				);

				if (mappedResult == null)
				{
					return OperationResult<GetAdvertisementPageByIdQueryResult>.NotFoundResult("Page not found");
				}

			}
		}



    }
}
