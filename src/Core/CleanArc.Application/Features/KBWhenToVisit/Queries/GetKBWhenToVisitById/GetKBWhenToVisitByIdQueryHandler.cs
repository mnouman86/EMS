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
using CleanArc.Application.Features.Language.Queries.GetLanguageById;

namespace CleanArc.Application.Features.KBWhenToVisit.Queries.GetKBWhenToVisitById
{
    internal class GetKBWhenToVisitByIdQueryHandler : IRequestHandler<GetKBWhenToVisitByIdQuery, OperationResult<GetKBWhenToVisitByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetKBWhenToVisitByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetKBWhenToVisitByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetKBWhenToVisitByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetKBWhenToVisitByIdQueryResult>> Handle(GetKBWhenToVisitByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                //var KBWhenToVisit = await _unitOfWork.KBWhenToVisitRepository.GetByIdAsync(request.Id);

                //if (KBWhenToVisit == null)
                //{
                //    return OperationResult<GetKBWhenToVisitByIdQueryResult>.NotFoundResult("KBWhenToVisit not found");
                //}

                ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                //var result = _mapper.Map<GetKBWhenToVisitByIdQueryResult>(KBWhenToVisit);

                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                //return OperationResult<GetKBWhenToVisitByIdQueryResult>.SuccessResult(result);

                var response = await _unitOfWork.KBWhenToVisitRepository.GetByIdAsync(request.Id);

                if (response.Code != 200)
                {
                    return OperationResult<GetKBWhenToVisitByIdQueryResult>.FailureResult(
                        response.Message,
                    response.Code
                    );
                }

                var mappedResult = _mapper.Map<GetKBWhenToVisitByIdQueryResult>(response.Data);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                return OperationResult<GetKBWhenToVisitByIdQueryResult>.SuccessResult(
                    mappedResult,
                    response.Code,
                    response.Message
                );
            }
        }

        //public ValueTask<OperationResult<GetKBWhenToVisitByIdQueryResult>> Handle(GetKBWhenToVisitByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
