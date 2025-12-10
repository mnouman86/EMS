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

namespace CleanArc.Application.Features.RoomDiscountByUserType.Queries.GetRoomDiscountByUserTypeById;

internal class GetRoomDiscountByUserTypeByIdQueryHandler : IRequestHandler<GetRoomDiscountByUserTypeByIdQuery, OperationResult<GetRoomDiscountByUserTypeByIdQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetRoomDiscountByUserTypeByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public GetRoomDiscountByUserTypeByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetRoomDiscountByUserTypeByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async ValueTask<OperationResult<GetRoomDiscountByUserTypeByIdQueryResult>> Handle(GetRoomDiscountByUserTypeByIdQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            //var roomDiscountByUserType = await _unitOfWork.RoomDiscountByUserTypeRepository.GetByIdAsync(request.searchRequestById);

            //if (roomDiscountByUserType == null)
            //{
            //    return OperationResult<GetRoomDiscountByUserTypeByIdQueryResult>.NotFoundResult("roomDiscountByUserType not found");
            //}

            ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
            //var result = _mapper.Map<GetRoomDiscountByUserTypeByIdQueryResult>(roomDiscountByUserType);

            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

            //return OperationResult<GetRoomDiscountByUserTypeByIdQueryResult>.SuccessResult(result);

            var response = await _unitOfWork.RoomDiscountByUserTypeRepository.GetByIdAsync(request.searchRequestById);

            if (response.Code != 200)
            {
                return OperationResult<GetRoomDiscountByUserTypeByIdQueryResult>.FailureResult(
                    response.Message,
                response.Code
                );
            }

            var mappedResult = _mapper.Map<GetRoomDiscountByUserTypeByIdQueryResult>(response.Data);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            return OperationResult<GetRoomDiscountByUserTypeByIdQueryResult>.SuccessResult(
                mappedResult,
                response.Code,
                response.Message
            );
        }
    }

   
}

