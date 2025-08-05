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
using CleanArc.Application.Features.Amenities.Queries.GetAmenitiesById;

namespace CleanArc.Application.Features.FeedbackStatus.Queries.GetFeedbackStatusById
{
    internal class GetFeedbackStatusByIdQueryHandler : IRequestHandler<GetFeedbackStatusByIdQuery, OperationResult<GetFeedbackStatusByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetFeedbackStatusByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetFeedbackStatusByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetFeedbackStatusByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetFeedbackStatusByIdQueryResult>> Handle(GetFeedbackStatusByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                //var FeedbackStatus = await _unitOfWork.FeedbackStatusRepository.GetByIdAsync(request.searchRequestById);

                //if (FeedbackStatus == null)
                //{
                //    return OperationResult<GetFeedbackStatusByIdQueryResult>.NotFoundResult("FeedbackStatus not found");
                //}

                ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                //var result = _mapper.Map<GetFeedbackStatusByIdQueryResult>(FeedbackStatus);

                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                //return OperationResult<GetFeedbackStatusByIdQueryResult>.SuccessResult(result);

                var response = await _unitOfWork.FeedbackStatusRepository.GetByIdAsync(request.searchRequestById);

                if (response.Code != 200)
                {
                    return OperationResult<GetFeedbackStatusByIdQueryResult>.FailureResult(
                        response.Message,
                    response.Code
                    );
                }

                var mappedResult = _mapper.Map<GetFeedbackStatusByIdQueryResult>(response.Data);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                return OperationResult<GetFeedbackStatusByIdQueryResult>.SuccessResult(
                    mappedResult,
                    response.Code,
                    response.Message
                );
            }
        }

       
    }
}
