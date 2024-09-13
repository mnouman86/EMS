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

namespace CleanArc.Application.Features.CheckProfileStatus.Queries.GetCheckProfileStatusById
{
    internal class GetCheckProfileStatusByIdQueryHandler : IRequestHandler<GetCheckProfileStatusByIdQuery, OperationResult<GetCheckProfileStatusByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetCheckProfileStatusByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetCheckProfileStatusByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetCheckProfileStatusByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetCheckProfileStatusByIdQueryResult>> Handle(GetCheckProfileStatusByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var CheckProfileStatus = await _unitOfWork.CheckProfileStatusRepository.GetByIdAsync(request.Id);

                if (CheckProfileStatus == null)
                {
                    return OperationResult<GetCheckProfileStatusByIdQueryResult>.NotFoundResult("CheckProfileStatus not found");
                }

                //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                var result = _mapper.Map<GetCheckProfileStatusByIdQueryResult>(CheckProfileStatus);

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                return OperationResult<GetCheckProfileStatusByIdQueryResult>.SuccessResult(result);
            }
        }

        //public ValueTask<OperationResult<GetCheckProfileStatusByIdQueryResult>> Handle(GetCheckProfileStatusByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
