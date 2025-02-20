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
using CleanArc.Application.Features.RoomVisual.Queries.GetRoomVisualById;

namespace CleanArc.Application.Features.RoomType.Queries.GetRoomTypeById;

internal class GetRoomTypeByIdQueryHandler : IRequestHandler<GetRoomTypeByIdQuery, OperationResult<GetRoomTypeByIdQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetRoomTypeByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public GetRoomTypeByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetRoomTypeByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async ValueTask<OperationResult<GetRoomTypeByIdQueryResult>> Handle(GetRoomTypeByIdQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            //var roomType = await _unitOfWork.RoomTypeRepository.GetByIdAsync(request.Id);

            //if (roomType == null)
            //{
            //    return OperationResult<GetRoomTypeByIdQueryResult>.NotFoundResult("roomType not found");
            //}

            ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
            //var result = _mapper.Map<GetRoomTypeByIdQueryResult>(roomType);

            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

            //return OperationResult<GetRoomTypeByIdQueryResult>.SuccessResult(result);

            var response = await _unitOfWork.RoomTypeRepository.GetByIdAsync(request.Id);

            if (response.Code != 200)
            {
                return OperationResult<GetRoomTypeByIdQueryResult>.FailureResult(
                    response.Message,
                response.Code
                );
            }

            var mappedResult = _mapper.Map<GetRoomTypeByIdQueryResult>(response.Data);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            return OperationResult<GetRoomTypeByIdQueryResult>.SuccessResult(
                mappedResult,
                response.Code,
                response.Message
            );
        }
    }

   
}

