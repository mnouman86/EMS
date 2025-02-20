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
using CleanArc.Application.Features.CoreArea.Queries.GetCoreAreaById;
using Azure;
using CleanArc.Application.Features.Activity.Queries.GetActivityCheckoutDetail;

namespace CleanArc.Application.Features.RoomDetails.Queries.GetRoomDetailById;

internal class GetRoomDetailByIdQueryHandler : IRequestHandler<GetRoomDetailByIdQuery, OperationResult<GetRoomDetailByIdQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetRoomDetailByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public GetRoomDetailByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetRoomDetailByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async ValueTask<OperationResult<GetRoomDetailByIdQueryResult>> Handle(GetRoomDetailByIdQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
	

		{
			var response = await _unitOfWork.RoomDetailsRepository.GetByIdAsync(request.Id);

			if (response.Code != 200)
			{
				return OperationResult<GetRoomDetailByIdQueryResult>.FailureResult(
				response.Message,
					response.Code
				);
			}

			var mappedResult = _mapper.Map<GetRoomDetailByIdQueryResult>(response.Data);
			(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

			return OperationResult<GetRoomDetailByIdQueryResult>.SuccessResult(
				mappedResult,
				response.Code,
				response.Message
			);

			if (mappedResult == null)
			{
				return OperationResult<GetRoomDetailByIdQueryResult>.NotFoundResult("CoreArea not found");
			}

		}
	}

   
}

