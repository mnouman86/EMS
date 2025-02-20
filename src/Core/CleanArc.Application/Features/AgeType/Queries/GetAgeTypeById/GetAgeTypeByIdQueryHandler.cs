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
using CleanArc.Application.Features.RoomType.Queries.GetRoomTypeById;
using Serilog.Core;

namespace CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById
{
    internal class GetAgeTypeByIdQueryHandler : IRequestHandler<GetAgeTypeByIdQuery, OperationResult<GetAgeTypeByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAgeTypeByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetAgeTypeByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetAgeTypeByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetAgeTypeByIdQueryResult>> Handle(GetAgeTypeByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
       

			{
				var response = await _unitOfWork.AgeTypeRepository.GetByIdAsync(request.Id);

				if (response.Code != 200)
				{
					return OperationResult<GetAgeTypeByIdQueryResult>.FailureResult(
					response.Message,
						response.Code
					);
				}

				var mappedResult = _mapper.Map<GetAgeTypeByIdQueryResult>(response.Data);
				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

				return OperationResult<GetAgeTypeByIdQueryResult>.SuccessResult(
					mappedResult,
					response.Code,
					response.Message
				);

				if (mappedResult == null)
				{
					return OperationResult<GetAgeTypeByIdQueryResult>.NotFoundResult("Age Type not found");
				}

			}
		}

       
    }
}
