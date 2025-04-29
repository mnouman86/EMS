using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.RoomDetails.Queries.GetHotelDetailForRoom;
using CleanArc.Application.Features.SearchBusinessCarDetail.Queries.GetCarDetailByBusiness;
using CleanArc.Application.Models.Common;
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

namespace CleanArc.Application.Features.SearchBusinessCarDetail.Queries.GetSearchBusinessCarDetailById
{
    
    internal class GetSearchBusinessCarDetailByIdQueryHandler : IRequestHandler<GetSearchBusinessCarDetailByIdQuery, OperationResult<GetSearchBusinessCarDetailByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetSearchBusinessCarDetailByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetSearchBusinessCarDetailByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetSearchBusinessCarDetailByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetSearchBusinessCarDetailByIdQueryResult>> Handle(GetSearchBusinessCarDetailByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var response = await _unitOfWork.SearchBusinessCarDetailRepository.GetCarDetailByBusinessAsync(request.searchRequestById);

                if (response.Code != 200)
                {
                    return OperationResult<GetSearchBusinessCarDetailByIdQueryResult>.FailureResult(
                        response.Message,
                        response.Code
                    );
                }

                var mappedResult = _mapper.Map<GetSearchBusinessCarDetailByIdQueryResult>(response.Data);

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                //return OperationResult<GetKBDetailByIdAllQueryResult>.SuccessResult(result);
                return OperationResult<GetSearchBusinessCarDetailByIdQueryResult>.SuccessResult(
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
