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

namespace CleanArc.Application.Features.ActivitySupervisor.Queries.GetActivitySupervisorById
{
    internal class GetActivitySupervisorByIdQueryHandler : IRequestHandler<GetActivitySupervisorByIdQuery, OperationResult<GetActivitySupervisorByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetActivitySupervisorByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetActivitySupervisorByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetActivitySupervisorByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetActivitySupervisorByIdQueryResult>> Handle(GetActivitySupervisorByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                //var ActivitySupervisor = await _unitOfWork.ActivitySupervisorRepository.GetByIdAsync(request.searchRequestById);

                //if (ActivitySupervisor == null)
                //{
                //    return OperationResult<GetActivitySupervisorByIdQueryResult>.NotFoundResult("ActivitySupervisor not found");
                //}

                ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                //var result = _mapper.Map<GetActivitySupervisorByIdQueryResult>(ActivitySupervisor);

                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                //return OperationResult<GetActivitySupervisorByIdQueryResult>.SuccessResult(result);

                var response = await _unitOfWork.ActivitySupervisorRepository.GetByIdAsync(request.searchRequestById);

                if (response.Code != 200)
                {
                    return OperationResult<GetActivitySupervisorByIdQueryResult>.FailureResult(
                        response.Message,
                    response.Code
                    );
                }

                var mappedResult = _mapper.Map<GetActivitySupervisorByIdQueryResult>(response.Data);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                return OperationResult<GetActivitySupervisorByIdQueryResult>.SuccessResult(
                    mappedResult,
                    response.Code,
                    response.Message
                );
            }
        }

        //public ValueTask<OperationResult<GetActivitySupervisorByIdQueryResult>> Handle(GetActivitySupervisorByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
