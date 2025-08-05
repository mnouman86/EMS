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

namespace CleanArc.Application.Features.FeedbackSubjectType.Queries.GetFeedbackSubjectTypeById
{
    internal class GetFeedbackSubjectTypeByIdQueryHandler : IRequestHandler<GetFeedbackSubjectTypeByIdQuery, OperationResult<GetFeedbackSubjectTypeByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetFeedbackSubjectTypeByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetFeedbackSubjectTypeByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetFeedbackSubjectTypeByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetFeedbackSubjectTypeByIdQueryResult>> Handle(GetFeedbackSubjectTypeByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                //var FeedbackSubjectType = await _unitOfWork.FeedbackSubjectTypeRepository.GetByIdAsync(request.searchRequestById);

                //if (FeedbackSubjectType == null)
                //{
                //    return OperationResult<GetFeedbackSubjectTypeByIdQueryResult>.NotFoundResult("FeedbackSubjectType not found");
                //}

                ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                //var result = _mapper.Map<GetFeedbackSubjectTypeByIdQueryResult>(FeedbackSubjectType);

                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                //return OperationResult<GetFeedbackSubjectTypeByIdQueryResult>.SuccessResult(result);

                var response = await _unitOfWork.FeedbackSubjectTypeRepository.GetByIdAsync(request.searchRequestById);

                if (response.Code != 200)
                {
                    return OperationResult<GetFeedbackSubjectTypeByIdQueryResult>.FailureResult(
                        response.Message,
                    response.Code
                    );
                }

                var mappedResult = _mapper.Map<GetFeedbackSubjectTypeByIdQueryResult>(response.Data);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                return OperationResult<GetFeedbackSubjectTypeByIdQueryResult>.SuccessResult(
                    mappedResult,
                    response.Code,
                    response.Message
                );
            }
        }

       
    }
}
