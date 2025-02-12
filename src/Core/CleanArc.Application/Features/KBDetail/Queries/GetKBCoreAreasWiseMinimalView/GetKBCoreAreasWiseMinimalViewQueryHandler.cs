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
using CleanArc.Application.Features.Activity.Queries.GetAllActivity;

namespace CleanArc.Application.Features.KBDetail.Queries.GetKBCoreAreasWiseMinimalView
{
    internal class GetKBCoreAreasWiseMinimalViewQueryHandler : IRequestHandler<GetKBCoreAreasWiseMinimalViewQuery, OperationResult<List<GetKBCoreAreasWiseMinimalViewQueryResult>>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetKBCoreAreasWiseMinimalViewQueryHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor


        public GetKBCoreAreasWiseMinimalViewQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<GetKBCoreAreasWiseMinimalViewQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;

        }

        public async ValueTask<OperationResult<List<GetKBCoreAreasWiseMinimalViewQueryResult>>> Handle(GetKBCoreAreasWiseMinimalViewQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                //var Product = await _unitOfWork.KBDetailRepository.GetKBCoreAreasMinimalViewAsync(request.searchRequest);

                ////var resultCheck = uRLs.Select(c => new GetAllProductsQueryResult(c.Id, c.Path, c.Title, c.Description)).ToList();
                //var result = _mapper.Map<List<GetKBCoreAreasWiseMinimalViewQueryResult>>(Product);
                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                //return OperationResult<List<GetKBCoreAreasWiseMinimalViewQueryResult>>.SuccessResult(result);

                var response = await _unitOfWork.KBDetailRepository.GetKBCoreAreasMinimalViewAsync(request.searchRequest);

                if (response.Code != 200)
                {
                    return OperationResult<List<GetKBCoreAreasWiseMinimalViewQueryResult>>.FailureResult(
                        response.Message,
                    response.Code
                    );
                }

                var mappedResult = _mapper.Map<List<GetKBCoreAreasWiseMinimalViewQueryResult>>(response.Data);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                return OperationResult<List<GetKBCoreAreasWiseMinimalViewQueryResult>>.SuccessResult(
                    mappedResult,
                    response.Code,
                    response.Message
                );
            }
        }
    }

}
