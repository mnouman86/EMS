using Azure;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.KBDetail.Queries.GetKBDetailByIdAll;
using CleanArc.Application.Features.KBDetail.Queries.GetKBMinimalView;
using CleanArc.Application.Features.RoomDetails.Queries.GetHotelDetailForRoom;
using CleanArc.Application.Features.URL.Queries.GetURLById;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.RoomDetails;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.RoomDetails.Queries.BookingReservation
{
    internal class BookingReservationQueryHandler : IRequestHandler<BookingReservationQuery, OperationResult<BookingReservationResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BookingReservationQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public BookingReservationQueryHandler(IUnitOfWork unitOfWork, ILogger<BookingReservationQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async ValueTask<OperationResult<BookingReservationResult>> Handle(BookingReservationQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var response = await _unitOfWork.RoomDetailsRepository.BookingReservationAsync(request.request);

                if (response.Code != 200)
                {
                    return OperationResult<BookingReservationResult>.FailureResult(
                        response.Message,
                        response.Code
                    );
                }

                var mappedResult = _mapper.Map<BookingReservationResult>(response.Data);

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                //return OperationResult<GetKBDetailByIdAllQueryResult>.SuccessResult(result);
                return OperationResult<BookingReservationResult>.SuccessResult(
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
