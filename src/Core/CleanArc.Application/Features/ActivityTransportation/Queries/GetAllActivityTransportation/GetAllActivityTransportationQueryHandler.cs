using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ActivityTransportation.Queries.GetAllActivityTransportation
{
    internal class GetAllActivityTransportationQueryHandler : IRequestHandler<GetAllActivityTransportationQuery, OperationResult<List<GetAllActivityTransportationQueryResult>>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllActivityTransportationQueryHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor


        public GetAllActivityTransportationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<GetAllActivityTransportationQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;

        }

        public async ValueTask<OperationResult<List<GetAllActivityTransportationQueryResult>>> Handle(GetAllActivityTransportationQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var Product = await _unitOfWork.ActivityTransportationRepository.GetAllAsync(request.searchRequest);

                //var resultCheck = uRLs.Select(c => new GetAllProductsQueryResult(c.Id, c.Path, c.Title, c.Description)).ToList();
                var result = _mapper.Map<List<GetAllActivityTransportationQueryResult>>(Product);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return OperationResult<List<GetAllActivityTransportationQueryResult>>.SuccessResult(result);
            }
        }
    }

}
