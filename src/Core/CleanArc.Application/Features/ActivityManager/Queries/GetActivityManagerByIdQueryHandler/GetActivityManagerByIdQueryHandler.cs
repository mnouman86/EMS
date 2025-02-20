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
using CleanArc.Application.Features.ActivityNature.Queries.GetActivityNatureById;

namespace CleanArc.Application.Features.ActivityManager.Queries.GetActivityManagerById
{
    internal class GetActivityManagerByIdQueryHandler : IRequestHandler<GetActivityManagerByIdQuery, OperationResult<GetActivityManagerByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetActivityManagerByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetActivityManagerByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetActivityManagerByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetActivityManagerByIdQueryResult>> Handle(GetActivityManagerByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                //var ActivityManager = await _unitOfWork.ActivityManagerRepository.GetByIdAsync(request.Id);

                //if (ActivityManager == null)
                //{
                //    return OperationResult<GetActivityManagerByIdQueryResult>.NotFoundResult("ActivityManager not found");
                //}

                ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                //var result = _mapper.Map<GetActivityManagerByIdQueryResult>(ActivityManager);

                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                //return OperationResult<GetActivityManagerByIdQueryResult>.SuccessResult(result);

                var response = await _unitOfWork.ActivityManagerRepository.GetByIdAsync(request.Id);

                if (response.Code != 200)
                {
                    return OperationResult<GetActivityManagerByIdQueryResult>.FailureResult(
                        response.Message,
                    response.Code
                    );
                }

                var mappedResult = _mapper.Map<GetActivityManagerByIdQueryResult>(response.Data);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                return OperationResult<GetActivityManagerByIdQueryResult>.SuccessResult(
                    mappedResult,
                    response.Code,
                    response.Message
                );
            }
        }

        //public ValueTask<OperationResult<GetActivityManagerByIdQueryResult>> Handle(GetActivityManagerByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
