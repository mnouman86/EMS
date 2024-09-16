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

namespace CleanArc.Application.Features.ActivityPrivateParticipant.Queries.GetActivityPrivateParticipantById
{
    internal class GetActivityPrivateParticipantByIdQueryHandler : IRequestHandler<GetActivityPrivateParticipantByIdQuery, OperationResult<GetActivityPrivateParticipantByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetActivityPrivateParticipantByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetActivityPrivateParticipantByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetActivityPrivateParticipantByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetActivityPrivateParticipantByIdQueryResult>> Handle(GetActivityPrivateParticipantByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var ActivityPrivateParticipant = await _unitOfWork.ActivityPrivateParticipantRepository.GetByIdAsync(request.Id);

                if (ActivityPrivateParticipant == null)
                {
                    return OperationResult<GetActivityPrivateParticipantByIdQueryResult>.NotFoundResult("ActivityPrivateParticipant not found");
                }

                //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                var result = _mapper.Map<GetActivityPrivateParticipantByIdQueryResult>(ActivityPrivateParticipant);

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                return OperationResult<GetActivityPrivateParticipantByIdQueryResult>.SuccessResult(result);
            }
        }

        //public ValueTask<OperationResult<GetActivityPrivateParticipantByIdQueryResult>> Handle(GetActivityPrivateParticipantByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
