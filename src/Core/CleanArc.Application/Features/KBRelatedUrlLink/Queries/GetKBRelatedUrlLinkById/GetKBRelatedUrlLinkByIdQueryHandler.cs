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
using CleanArc.Application.Features.KBTiming.Queries.GetKBTimingById;

namespace CleanArc.Application.Features.KBRelatedUrlLink.Queries.GetKBRelatedUrlLinkById
{
    internal class GetKBRelatedUrlLinkByIdQueryHandler : IRequestHandler<GetKBRelatedUrlLinkByIdQuery, OperationResult<GetKBRelatedUrlLinkByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetKBRelatedUrlLinkByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetKBRelatedUrlLinkByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetKBRelatedUrlLinkByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetKBRelatedUrlLinkByIdQueryResult>> Handle(GetKBRelatedUrlLinkByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                //var KBRelatedUrlLink = await _unitOfWork.KBRelatedUrlLinkRepository.GetByIdAsync(request.Id);

                //if (KBRelatedUrlLink == null)
                //{
                //    return OperationResult<GetKBRelatedUrlLinkByIdQueryResult>.NotFoundResult("KBRelatedUrlLink not found");
                //}

                ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                //var result = _mapper.Map<GetKBRelatedUrlLinkByIdQueryResult>(KBRelatedUrlLink);

                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                //return OperationResult<GetKBRelatedUrlLinkByIdQueryResult>.SuccessResult(result);
                var response = await _unitOfWork.KBRelatedUrlLinkRepository.GetByIdAsync(request.Id);

                if (response.Code != 200)
                {
                    return OperationResult<GetKBRelatedUrlLinkByIdQueryResult>.FailureResult(
                        response.Message,
                    response.Code
                    );
                }

                var mappedResult = _mapper.Map<GetKBRelatedUrlLinkByIdQueryResult>(response.Data);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                return OperationResult<GetKBRelatedUrlLinkByIdQueryResult>.SuccessResult(
                    mappedResult,
                    response.Code,
                    response.Message
                );
            }
        }

        //public ValueTask<OperationResult<GetKBRelatedUrlLinkByIdQueryResult>> Handle(GetKBRelatedUrlLinkByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
