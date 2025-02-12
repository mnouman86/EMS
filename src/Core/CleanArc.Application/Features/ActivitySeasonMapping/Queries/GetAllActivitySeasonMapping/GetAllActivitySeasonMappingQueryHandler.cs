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

namespace CleanArc.Application.Features.ActivitySeasonMapping.Queries.GetAllActivitySeasonMapping
{
    internal class GetAllActivitySeasonMappingQueryHandler : IRequestHandler<GetAllActivitySeasonMappingQuery, OperationResult<List<GetAllActivitySeasonMappingQueryResult>>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllActivitySeasonMappingQueryHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor


        public GetAllActivitySeasonMappingQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<GetAllActivitySeasonMappingQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;

        }

        public async ValueTask<OperationResult<List<GetAllActivitySeasonMappingQueryResult>>> Handle(GetAllActivitySeasonMappingQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                //var Product = await _unitOfWork.ActivitySeasonMappingRepository.GetAllAsync(request.searchRequest);

                ////var resultCheck = uRLs.Select(c => new GetAllProductsQueryResult(c.Id, c.Path, c.Title, c.Description)).ToList();
                //var result = _mapper.Map<List<GetAllActivitySeasonMappingQueryResult>>(Product);
                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                //return OperationResult<List<GetAllActivitySeasonMappingQueryResult>>.SuccessResult(result);

                var response = await _unitOfWork.ActivitySeasonMappingRepository.GetAllAsync(request.searchRequest);

                if (response.Code != 200)
                {
                    return OperationResult<List<GetAllActivitySeasonMappingQueryResult>>.FailureResult(
                        response.Message,
                    response.Code
                    );
                }

                var mappedResult = _mapper.Map<List<GetAllActivitySeasonMappingQueryResult>>(response.Data);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                return OperationResult<List<GetAllActivitySeasonMappingQueryResult>>.SuccessResult(
                    mappedResult,
                    response.Code,
                    response.Message
                );
            }
        }
    }

}
