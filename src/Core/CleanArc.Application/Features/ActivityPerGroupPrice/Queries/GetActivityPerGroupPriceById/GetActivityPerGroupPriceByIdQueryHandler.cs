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
using CleanArc.Application.Features.ActivityPricePerParticipant.Queries.GetActivityPricePerParticipantById;

namespace CleanArc.Application.Features.ActivityPerGroupPrice.Queries.GetActivityPerGroupPriceById
{
    internal class GetActivityPerGroupPriceByIdQueryHandler : IRequestHandler<GetActivityPerGroupPriceByIdQuery, OperationResult<GetActivityPerGroupPriceByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetActivityPerGroupPriceByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetActivityPerGroupPriceByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetActivityPerGroupPriceByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetActivityPerGroupPriceByIdQueryResult>> Handle(GetActivityPerGroupPriceByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                //var ActivityPerGroupPrice = await _unitOfWork.ActivityPerGroupPriceRepository.GetByIdAsync(request.Id);

                //if (ActivityPerGroupPrice == null)
                //{
                //    return OperationResult<GetActivityPerGroupPriceByIdQueryResult>.NotFoundResult("ActivityPerGroupPrice not found");
                //}

                ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                //var result = _mapper.Map<GetActivityPerGroupPriceByIdQueryResult>(ActivityPerGroupPrice);

                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                //return OperationResult<GetActivityPerGroupPriceByIdQueryResult>.SuccessResult(result);

                var response = await _unitOfWork.ActivityPerGroupPriceRepository.GetByIdAsync(request.Id);

                if (response.Code != 200)
                {
                    return OperationResult<GetActivityPerGroupPriceByIdQueryResult>.FailureResult(
                        response.Message,
                    response.Code
                    );
                }

                var mappedResult = _mapper.Map<GetActivityPerGroupPriceByIdQueryResult>(response.Data);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                return OperationResult<GetActivityPerGroupPriceByIdQueryResult>.SuccessResult(
                    mappedResult,
                    response.Code,
                    response.Message
                );
            }
        }

       
    }
}
