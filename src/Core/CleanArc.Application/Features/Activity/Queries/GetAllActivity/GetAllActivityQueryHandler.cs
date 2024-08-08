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

namespace CleanArc.Application.Features.Activity.Queries.GetAllActivity
{
    internal class GetAllActivityQueryHandler : IRequestHandler<GetAllActivityQuery, OperationResult<List<GetAllActivityQueryResult>>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllActivityQueryHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor


        public GetAllActivityQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<GetAllActivityQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;

        }

        public async ValueTask<OperationResult<List<GetAllActivityQueryResult>>> Handle(GetAllActivityQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var Product = await _unitOfWork.ActivityRepository.GetAllAsync(request.searchRequest);

                //var resultCheck = uRLs.Select(c => new GetAllProductsQueryResult(c.Id, c.Path, c.Title, c.Description)).ToList();
                var result = _mapper.Map<List<GetAllActivityQueryResult>>(Product);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return OperationResult<List<GetAllActivityQueryResult>>.SuccessResult(result);
            }
        }
    }

}
