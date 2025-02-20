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

namespace CleanArc.Application.Features.CarDetail.Queries.GetCarDetailById
{
    internal class GetCarDetailByIdQueryHandler : IRequestHandler<GetCarDetailByIdQuery, OperationResult<GetCarDetailByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetCarDetailByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetCarDetailByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetCarDetailByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetCarDetailByIdQueryResult>> Handle(GetCarDetailByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
          

			{
				var response = await _unitOfWork.CarDetailRepository.GetByIdAsync(request.Id);

				if (response.Code != 200)
				{
					return OperationResult<GetCarDetailByIdQueryResult>.FailureResult(
					response.Message,
						response.Code
					);
				}

				var mappedResult = _mapper.Map<GetCarDetailByIdQueryResult>(response.Data);
				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

				return OperationResult<GetCarDetailByIdQueryResult>.SuccessResult(
					mappedResult,
					response.Code,
					response.Message
				);

				if (mappedResult == null)
				{
					return OperationResult<GetCarDetailByIdQueryResult>.NotFoundResult("carDetail not found");
				}

			}
		}
    }
}

