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
using CleanArc.Application.Features.KBRelatedUrlLink.Queries.GetKBRelatedUrlLinkById;

namespace CleanArc.Application.Features.KBMedia.Queries.GetKBMediaById
{
    internal class GetKBMediaByIdQueryHandler : IRequestHandler<GetKBMediaByIdQuery, OperationResult<GetKBMediaByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetKBMediaByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetKBMediaByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetKBMediaByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetKBMediaByIdQueryResult>> Handle(GetKBMediaByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                //var KBMedia = await _unitOfWork.KBMediaRepository.GetByIdAsync(request.searchRequestById);

                //if (KBMedia == null)
                //{
                //    return OperationResult<GetKBMediaByIdQueryResult>.NotFoundResult("KBMedia not found");
                //}

                ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                //var result = _mapper.Map<GetKBMediaByIdQueryResult>(KBMedia);

                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                //return OperationResult<GetKBMediaByIdQueryResult>.SuccessResult(result);


                var response = await _unitOfWork.KBMediaRepository.GetByIdAsync(request.searchRequestById);

                if (response.Code != 200)
                {
                    return OperationResult<GetKBMediaByIdQueryResult>.FailureResult(
                        response.Message,
                    response.Code
                    );
                }

                var mappedResult = _mapper.Map<GetKBMediaByIdQueryResult>(response.Data);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                return OperationResult<GetKBMediaByIdQueryResult>.SuccessResult(
                    mappedResult,
                    response.Code,
                    response.Message
                );
            }
        }

        //public ValueTask<OperationResult<GetKBMediaByIdQueryResult>> Handle(GetKBMediaByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
