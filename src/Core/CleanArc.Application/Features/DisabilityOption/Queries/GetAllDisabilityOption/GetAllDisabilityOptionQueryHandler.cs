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

namespace CleanArc.Application.Features.DisabilityOption.Queries.GetAllDisabilityOption
{
    internal class GetAllDisabilityOptionQueryHandler : IRequestHandler<GetAllDisabilityOptionQuery, OperationResult<List<GetAllDisabilityOptionQueryResult>>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllDisabilityOptionQueryHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor


        public GetAllDisabilityOptionQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<GetAllDisabilityOptionQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;

        }

        public async ValueTask<OperationResult<List<GetAllDisabilityOptionQueryResult>>> Handle(GetAllDisabilityOptionQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                //var Product = await _unitOfWork.DisabilityOptionRepository.GetAllAsync(request.searchRequest);

                ////var resultCheck = uRLs.Select(c => new GetAllProductsQueryResult(c.Id, c.Path, c.Title, c.Description)).ToList();
                //var result = _mapper.Map<List<GetAllDisabilityOptionQueryResult>>(Product);
                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                //return OperationResult<List<GetAllDisabilityOptionQueryResult>>.SuccessResult(result);

                var response = await _unitOfWork.DisabilityOptionRepository.GetAllAsync(request.searchRequest);

                if (response.Code != 200)
                {
                    return OperationResult<List<GetAllDisabilityOptionQueryResult>>.FailureResult(
                        response.Message,
                    response.Code
                    );
                }

                var mappedResult = _mapper.Map<List<GetAllDisabilityOptionQueryResult>>(response.Data);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                return OperationResult<List<GetAllDisabilityOptionQueryResult>>.SuccessResult(
                    mappedResult,
                    response.Code,
                    response.Message
                );
            }
        }
    }

}
