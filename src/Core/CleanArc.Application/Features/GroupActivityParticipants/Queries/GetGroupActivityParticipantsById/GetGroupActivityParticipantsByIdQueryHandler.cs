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
using CleanArc.Application.Features.HomeSlider.Queries.GetHomeSliderById;

namespace CleanArc.Application.Features.GroupActivityParticipants.Queries.GetGroupActivityParticipantsById
{
    internal class GetGroupActivityParticipantsByIdQueryHandler : IRequestHandler<GetGroupActivityParticipantsByIdQuery, OperationResult<GetGroupActivityParticipantsByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetGroupActivityParticipantsByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetGroupActivityParticipantsByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetGroupActivityParticipantsByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetGroupActivityParticipantsByIdQueryResult>> Handle(GetGroupActivityParticipantsByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                //var GroupActivityParticipants = await _unitOfWork.GroupActivityParticipantsRepository.GetByIdAsync(request.searchRequestById);

                //if (GroupActivityParticipants == null)
                //{
                //    return OperationResult<GetGroupActivityParticipantsByIdQueryResult>.NotFoundResult("GroupActivityParticipants not found");
                //}

                ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                //var result = _mapper.Map<GetGroupActivityParticipantsByIdQueryResult>(GroupActivityParticipants);

                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                //return OperationResult<GetGroupActivityParticipantsByIdQueryResult>.SuccessResult(result);

                var response = await _unitOfWork.GroupActivityParticipantsRepository.GetByIdAsync(request.searchRequestById);

                if (response.Code != 200)
                {
                    return OperationResult<GetGroupActivityParticipantsByIdQueryResult>.FailureResult(
                        response.Message,
                    response.Code
                    );
                }

                var mappedResult = _mapper.Map<GetGroupActivityParticipantsByIdQueryResult>(response.Data);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                return OperationResult<GetGroupActivityParticipantsByIdQueryResult>.SuccessResult(
                    mappedResult,
                    response.Code,
                    response.Message
                );
            }
        }

        //public ValueTask<OperationResult<GetGroupActivityParticipantsByIdQueryResult>> Handle(GetGroupActivityParticipantsByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
