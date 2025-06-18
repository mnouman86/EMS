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
using CleanArc.Application.Features.KBDetail.Queries.GetKBDetailByIdAll;
using Azure;
using CleanArc.Application.Features.KBDetail.Queries.GetKBMinimalView;

namespace CleanArc.Application.Features.RoomDetails.Queries.GetHotelDetailForRoom
{
    internal class GetHotelDetailForRoomQueryHandler : IRequestHandler<GetHotelDetailForRoomQuery, OperationResult<GetHotelDetailForRoomQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetHotelDetailForRoomQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetHotelDetailForRoomQueryHandler(IUnitOfWork unitOfWork, ILogger<GetHotelDetailForRoomQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async ValueTask<OperationResult<GetHotelDetailForRoomQueryResult>> Handle(GetHotelDetailForRoomQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var response = await _unitOfWork.RoomDetailsRepository.GetHotelDetailForRoomAsync(request.searchRequest);

                if (response.Code != 200)
                {
                    return OperationResult<GetHotelDetailForRoomQueryResult>.FailureResult(
                        response.Message,
                        response.Code
                    );
                }

                var mappedResult = _mapper.Map<GetHotelDetailForRoomQueryResult>(response.Data);

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                //return OperationResult<GetKBDetailByIdAllQueryResult>.SuccessResult(result);
                return OperationResult<GetHotelDetailForRoomQueryResult>.SuccessResult(
                    mappedResult,
                    response.Code,
                    response.Message
                );
            }
        }

        //public ValueTask<OperationResult<GetKBDetailByIdQueryResult>> Handle(GetKBDetailByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
