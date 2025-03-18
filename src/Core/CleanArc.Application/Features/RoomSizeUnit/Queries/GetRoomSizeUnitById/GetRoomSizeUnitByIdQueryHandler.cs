using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArc.Application.Features.RoomType.Queries.GetRoomTypeById;

namespace CleanArc.Application.Features.RoomSizeUnit.Queries.GetRoomSizeUnitById;

internal class GetRoomSizeUnitByIdQueryHandler : IRequestHandler<GetRoomSizeUnitByIdQuery, OperationResult<GetRoomSizeUnitByIdQueryResult>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ILogger<GetRoomSizeUnitByIdQueryHandler> _logger;
	private readonly IMapper _mapper;
	private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public GetRoomSizeUnitByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetRoomSizeUnitByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async ValueTask<OperationResult<GetRoomSizeUnitByIdQueryResult>> Handle(GetRoomSizeUnitByIdQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            //var roomSizeUnit = await _unitOfWork.RoomSizeUnitReposirory.GetByIdAsync(request.searchRequestById);

            //if (roomSizeUnit == null)
            //{
            //    return OperationResult<GetRoomSizeUnitByIdQueryResult>.NotFoundResult("roomSizeUnit not found");
            //}

            ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
            //var result = _mapper.Map<GetRoomSizeUnitByIdQueryResult>(roomSizeUnit);

            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

            //return OperationResult<GetRoomSizeUnitByIdQueryResult>.SuccessResult(result);

            var response = await _unitOfWork.RoomSizeUnitReposirory.GetByIdAsync(request.searchRequestById);

            if (response.Code != 200)
            {
                return OperationResult<GetRoomSizeUnitByIdQueryResult>.FailureResult(
                    response.Message,
                response.Code
                );
            }

            var mappedResult = _mapper.Map<GetRoomSizeUnitByIdQueryResult>(response.Data);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            return OperationResult<GetRoomSizeUnitByIdQueryResult>.SuccessResult(
                mappedResult,
                response.Code,
                response.Message
            );
        }
    }


}


