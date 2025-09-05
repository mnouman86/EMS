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
using CleanArc.Application.Features.ActivityAddress.Queries.GetGenericAddressById;
using System.Globalization;

namespace CleanArc.Application.Features.Activity.Queries.GetActivityDetailById
{
    internal class GetActivityDetailByIdQueryHandler : IRequestHandler<GetActivityDetailByIdQuery, OperationResult<GetActivityDetailByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetActivityDetailByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetActivityDetailByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetActivityDetailByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetActivityDetailByIdQueryResult>> Handle(GetActivityDetailByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                //var Activity = await _unitOfWork.ActivityRepository.GetByIdAsync(request.searchRequestById);

                //if (Activity == null)
                //{
                //    return OperationResult<GetActivityDetailByIdQueryResult>.NotFoundResult("Activity not found");
                //}

                ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                //var result = _mapper.Map<GetActivityDetailByIdQueryResult>(Activity);

                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                //return OperationResult<GetActivityDetailByIdQueryResult>.SuccessResult(result);

                var response = await _unitOfWork.ActivityRepository.GetActivityDetailByBusinessAsync(request.searchRequestById, request.userId);

                if (response.Code != 200)
                {
                    return OperationResult<GetActivityDetailByIdQueryResult>.FailureResult(
                        response.Message,
                    response.Code
                    );
                }

                var mappedResult = _mapper.Map<GetActivityDetailByIdQueryResult>(response.Data);
                if (mappedResult != null)
                {
                    mappedResult.StartTime = response.Data.StartTime.HasValue
    ? response.Data.StartTime.Value.ToString("hh:mm tt", CultureInfo.InvariantCulture)
    : string.Empty;

                    mappedResult.EndTime = response.Data.EndTime.HasValue
    ? response.Data.EndTime.Value.ToString("hh:mm tt", CultureInfo.InvariantCulture)
    : string.Empty;
                    //            public string? CheckInFromTime { get; set; }
                    //public string? CheckInToTime { get; set; }
                    //public string? CheckOutFromTime { get; set; }
                    //public string? CheckOutToTime { get; set; }
                }
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                return OperationResult<GetActivityDetailByIdQueryResult>.SuccessResult(
                    mappedResult,
                    response.Code,
                    response.Message
                );
            }
        }

        //public ValueTask<OperationResult<GetActivityDetailByIdQueryResult>> Handle(GetActivityDetailByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
