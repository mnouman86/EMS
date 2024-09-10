using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.URL.Queries.GetURLById;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.CoreArea.Queries.GetCoreAreaById
{
    internal class GetCoreAreaByIdQueryHandler : IRequestHandler<GetCoreAreaByIdQuery, OperationResult<GetCoreAreaByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetCoreAreaByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetCoreAreaByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetCoreAreaByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetCoreAreaByIdQueryResult>> Handle(GetCoreAreaByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var CoreArea = await _unitOfWork.CoreAreaRepository.GetByIdAsync(request.Id);

                if (CoreArea == null)
                {
                    return OperationResult<GetCoreAreaByIdQueryResult>.NotFoundResult("CoreArea not found");
                }

                //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                var result = _mapper.Map<GetCoreAreaByIdQueryResult>(CoreArea);

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                return OperationResult<GetCoreAreaByIdQueryResult>.SuccessResult(result);
            }
        }

        //public ValueTask<OperationResult<GetCoreAreaByIdQueryResult>> Handle(GetCoreAreaByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
