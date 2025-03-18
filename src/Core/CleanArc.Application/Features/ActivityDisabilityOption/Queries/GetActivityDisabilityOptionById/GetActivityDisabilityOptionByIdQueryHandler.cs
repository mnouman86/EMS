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
using CleanArc.Application.Features.ActivityGroup.Queries.GetActivityGroupById;

namespace CleanArc.Application.Features.ActivityDisabilityOption.Queries.GetActivityDisabilityOptionById
{
    internal class GetActivityDisabilityOptionByIdQueryHandler : IRequestHandler<GetActivityDisabilityOptionByIdQuery, OperationResult<GetActivityDisabilityOptionByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetActivityDisabilityOptionByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetActivityDisabilityOptionByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetActivityDisabilityOptionByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetActivityDisabilityOptionByIdQueryResult>> Handle(GetActivityDisabilityOptionByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                //var ActivityDisabilityOption = await _unitOfWork.ActivityDisabilityOptionRepository.GetByIdAsync(request.searchRequestById);

                //if (ActivityDisabilityOption == null)
                //{
                //    return OperationResult<GetActivityDisabilityOptionByIdQueryResult>.NotFoundResult("ActivityDisabilityOption not found");
                //}

                ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                //var result = _mapper.Map<GetActivityDisabilityOptionByIdQueryResult>(ActivityDisabilityOption);

                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                //return OperationResult<GetActivityDisabilityOptionByIdQueryResult>.SuccessResult(result);

                var response = await _unitOfWork.ActivityDisabilityOptionRepository.GetByIdAsync(request.searchRequestById);

                if (response.Code != 200)
                {
                    return OperationResult<GetActivityDisabilityOptionByIdQueryResult>.FailureResult(
                        response.Message,
                    response.Code
                    );
                }

                var mappedResult = _mapper.Map<GetActivityDisabilityOptionByIdQueryResult>(response.Data);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                return OperationResult<GetActivityDisabilityOptionByIdQueryResult>.SuccessResult(
                    mappedResult,
                    response.Code,
                    response.Message
                );
            }
        }

        //public ValueTask<OperationResult<GetActivityDisabilityOptionByIdQueryResult>> Handle(GetActivityDisabilityOptionByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
