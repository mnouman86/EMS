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
using CleanArc.Application.Features.KBInterested.Queries.GetKBInterestedById;

namespace CleanArc.Application.Features.KBDescription.Queries.GetKBDescriptionById
{
    internal class GetKBDescriptionByIdQueryHandler : IRequestHandler<GetKBDescriptionByIdQuery, OperationResult<GetKBDescriptionByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetKBDescriptionByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetKBDescriptionByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetKBDescriptionByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetKBDescriptionByIdQueryResult>> Handle(GetKBDescriptionByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                //var KBDescription = await _unitOfWork.KBDescriptionRepository.GetByIdAsync(request.searchRequestById);

                //if (KBDescription == null)
                //{
                //    return OperationResult<GetKBDescriptionByIdQueryResult>.NotFoundResult("KBDescription not found");
                //}

                ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                //var result = _mapper.Map<GetKBDescriptionByIdQueryResult>(KBDescription);

                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                //return OperationResult<GetKBDescriptionByIdQueryResult>.SuccessResult(result);

                var response = await _unitOfWork.KBDescriptionRepository.GetByIdAsync(request.searchRequestById);

                if (response.Code != 200)
                {
                    return OperationResult<GetKBDescriptionByIdQueryResult>.FailureResult(
                        response.Message,
                    response.Code
                    );
                }

                var mappedResult = _mapper.Map<GetKBDescriptionByIdQueryResult>(response.Data);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                return OperationResult<GetKBDescriptionByIdQueryResult>.SuccessResult(
                    mappedResult,
                    response.Code,
                    response.Message
                );
            }
        }

        //public ValueTask<OperationResult<GetKBDescriptionByIdQueryResult>> Handle(GetKBDescriptionByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
