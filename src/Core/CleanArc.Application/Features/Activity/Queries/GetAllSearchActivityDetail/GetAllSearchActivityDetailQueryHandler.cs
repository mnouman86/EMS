using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging; using CleanArc.Domain.Common;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using CleanArc.Application.Features.KBDetail.Queries.GetKBMinimalView;
using CleanArc.Application.Features.Activity.Queries.GetAllSearchActivityDetail;

namespace CleanArc.Application.Features.Activity.Queries.GetAllActivity
{
    internal class GetAllSearchActivityDetailQueryHandler : IRequestHandler<GetAllSearchActivityDetailQuery, OperationResult<GetAllSearchActivityDetailQueryResult>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllSearchActivityDetailQueryHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor


        public GetAllSearchActivityDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<GetAllSearchActivityDetailQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;

        }

        public async ValueTask<OperationResult<GetAllSearchActivityDetailQueryResult>> Handle(GetAllSearchActivityDetailQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var response = await _unitOfWork.ActivityRepository.GetAllActivitySearchDetailAsync(request.searchRequest);

                if (response.Code != 200)
                {
                    return OperationResult<GetAllSearchActivityDetailQueryResult>.FailureResult(
                        response.Message,
                    response.Code
                    );
                }

                var mappedResult = _mapper.Map<GetAllSearchActivityDetailQueryResult>(response.Data);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                return OperationResult<GetAllSearchActivityDetailQueryResult>.SuccessResult(
                    mappedResult,
                    response.Code,
                    response.Message
                );
            }
        }
    }

}
