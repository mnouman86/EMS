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
using CleanArc.Application.Features.KBDetail.Queries.GetKBMinimalView;

namespace CleanArc.Application.Features.Activity.Queries.GetActivityCheckoutDetail
{
    internal class GetActivityCheckoutDetailQueryHandler : IRequestHandler<GetActivityCheckoutDetailQuery, OperationResult<GetActivityCheckoutDetailQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetActivityCheckoutDetailQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetActivityCheckoutDetailQueryHandler(IUnitOfWork unitOfWork, ILogger<GetActivityCheckoutDetailQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetActivityCheckoutDetailQueryResult>> Handle(GetActivityCheckoutDetailQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                //var Activity = await _unitOfWork.ActivityRepository.GetActivityCheckoutDetailAsync(request.Id,request.UserId);

                //if (Activity == null)
                //{
                //    return OperationResult<GetActivityCheckoutDetailQueryResult>.NotFoundResult("Activity not found");
                //}

                ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                //var result = _mapper.Map<GetActivityCheckoutDetailQueryResult>(Activity);

                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                //return OperationResult<GetActivityCheckoutDetailQueryResult>.SuccessResult(result);

				var response = await _unitOfWork.ActivityRepository.GetActivityCheckoutDetailAsync(request);

				if (response.Code != 200)
				{
					return OperationResult<GetActivityCheckoutDetailQueryResult>.FailureResult(
						response.Message,
						response.Code
					);
				}

				var mappedResult = _mapper.Map<GetActivityCheckoutDetailQueryResult>(response.Data);
				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

				return OperationResult<GetActivityCheckoutDetailQueryResult>.SuccessResult(
					mappedResult,
					response.Code,
					response.Message
				);
			}
        }

        //public ValueTask<OperationResult<GetActivityByIdQueryResult>> Handle(GetActivityByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
