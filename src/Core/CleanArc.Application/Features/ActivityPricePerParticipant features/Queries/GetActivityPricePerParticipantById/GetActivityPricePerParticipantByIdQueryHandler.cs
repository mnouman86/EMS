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
using CleanArc.Application.Features.ActivityPrivateParticipant.Queries.GetActivityPrivateParticipantById;

namespace CleanArc.Application.Features.ActivityPricePerParticipant.Queries.GetActivityPricePerParticipantById
{
    internal class GetActivityPricePerParticipantByIdQueryHandler : IRequestHandler<GetActivityPricePerParticipantByIdQuery, OperationResult<GetActivityPricePerParticipantByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetActivityPricePerParticipantByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetActivityPricePerParticipantByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetActivityPricePerParticipantByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetActivityPricePerParticipantByIdQueryResult>> Handle(GetActivityPricePerParticipantByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                //var ActivityPricePerParticipant = await _unitOfWork.ActivityPricePerParticipantRepository.GetByIdAsync(request.searchRequestById);

                //if (ActivityPricePerParticipant == null)
                //{
                //    return OperationResult<GetActivityPricePerParticipantByIdQueryResult>.NotFoundResult("ActivityPricePerParticipant not found");
                //}

                ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                //var result = _mapper.Map<GetActivityPricePerParticipantByIdQueryResult>(ActivityPricePerParticipant);

                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                //return OperationResult<GetActivityPricePerParticipantByIdQueryResult>.SuccessResult(result);

                var response = await _unitOfWork.ActivityPricePerParticipantRepository.GetByIdAsync(request.searchRequestById);

                if (response.Code != 200)
                {
                    return OperationResult<GetActivityPricePerParticipantByIdQueryResult>.FailureResult(
                        response.Message,
                    response.Code
                    );
                }

                var mappedResult = _mapper.Map<GetActivityPricePerParticipantByIdQueryResult>(response.Data);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                return OperationResult<GetActivityPricePerParticipantByIdQueryResult>.SuccessResult(
                    mappedResult,
                    response.Code,
                    response.Message
                );
            }
        }

        //public ValueTask<OperationResult<GetActivityPricePerParticipantByIdQueryResult>> Handle(GetActivityPricePerParticipantByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
