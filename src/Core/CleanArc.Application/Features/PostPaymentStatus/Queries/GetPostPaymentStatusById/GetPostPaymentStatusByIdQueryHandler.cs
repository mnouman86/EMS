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
using CleanArc.Application.Features.RoomDetails.Queries.GetRoomDetailById;

namespace CleanArc.Application.Features.PostPaymentStatus.Queries.GetPostPaymentStatusById
{
    internal class GetPostPaymentStatusByIdQueryHandler : IRequestHandler<GetPostPaymentStatusByIdQuery, OperationResult<GetPostPaymentStatusByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetPostPaymentStatusByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetPostPaymentStatusByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetPostPaymentStatusByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetPostPaymentStatusByIdQueryResult>> Handle(GetPostPaymentStatusByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                //var PostPaymentStatus = await _unitOfWork.PostPaymentStatusRepository.GetByIdAsync(request.searchRequestById);

                //if (PostPaymentStatus == null)
                //{
                //    return OperationResult<GetPostPaymentStatusByIdQueryResult>.NotFoundResult("PostPaymentStatus not found");
                //}

                ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                //var result = _mapper.Map<GetPostPaymentStatusByIdQueryResult>(PostPaymentStatus);

                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                //return OperationResult<GetPostPaymentStatusByIdQueryResult>.SuccessResult(result);

                var response = await _unitOfWork.PostPaymentStatusRepository.GetByIdAsync(request.searchRequestById);

                if (response.Code != 200)
                {
                    return OperationResult<GetPostPaymentStatusByIdQueryResult>.FailureResult(
                        response.Message,
                    response.Code
                    );
                }

                var mappedResult = _mapper.Map<GetPostPaymentStatusByIdQueryResult>(response.Data);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                return OperationResult<GetPostPaymentStatusByIdQueryResult>.SuccessResult(
                    mappedResult,
                    response.Code,
                    response.Message
                );
            }
        }

        //public ValueTask<OperationResult<GetPostPaymentStatusByIdQueryResult>> Handle(GetPostPaymentStatusByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
