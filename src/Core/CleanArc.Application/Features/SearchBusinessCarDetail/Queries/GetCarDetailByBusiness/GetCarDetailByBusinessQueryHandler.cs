using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.URL.Queries.GetURLById;
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
using CleanArc.Application.Features.RoomDetails.Queries.GetHotelDetailForRoom;

namespace CleanArc.Application.Features.SearchBusinessCarDetail.Queries.GetCarDetailByBusiness
{
    internal class GetCarDetailByBusinessQueryHandler : IRequestHandler<GetCarDetailByBusinessQuery, OperationResult<GetCarDetailByBusinessQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetHotelDetailForRoomQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetCarDetailByBusinessQueryHandler(IUnitOfWork unitOfWork, ILogger<GetHotelDetailForRoomQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetCarDetailByBusinessQueryResult>> Handle(GetCarDetailByBusinessQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var response = await _unitOfWork.SearchBusinessCarDetailRepository.GetCarDetailByBusinessAsync(request.searchRequest);

                if (response.Code != 200)
                {
                    return OperationResult<GetCarDetailByBusinessQueryResult>.FailureResult(
                        response.Message,
                        response.Code
                    );
                }

                var mappedResult = _mapper.Map<GetCarDetailByBusinessQueryResult>(response.Data);

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                //return OperationResult<GetKBDetailByIdAllQueryResult>.SuccessResult(result);
                return OperationResult<GetCarDetailByBusinessQueryResult>.SuccessResult(
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
