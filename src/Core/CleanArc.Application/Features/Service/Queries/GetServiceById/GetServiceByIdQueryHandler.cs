using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
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

namespace CleanArc.Application.Features.Service.Queries.GetServiceById;

internal class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, OperationResult<GetServiceByIdQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetServiceByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public GetServiceByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetServiceByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async ValueTask<OperationResult<GetServiceByIdQueryResult>> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))

		{
			var response = await _unitOfWork.ServiceRepository.GetByIdAsync(request.Id);

			if (response.Code != 200)
			{
				return OperationResult<GetServiceByIdQueryResult>.FailureResult(
				response.Message,
					response.Code
				);
			}

			var mappedResult = _mapper.Map<GetServiceByIdQueryResult>(response.Data);
			(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

			return OperationResult<GetServiceByIdQueryResult>.SuccessResult(
				mappedResult,
				response.Code,
				response.Message
			);

			if (mappedResult == null)
			{
				return OperationResult<GetServiceByIdQueryResult>.NotFoundResult("Service not found");
			}

		}
	}

   
}

