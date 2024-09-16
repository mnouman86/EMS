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

namespace CleanArc.Application.Features.SubService.Queries.GetSubServiceById
{
    internal class GetSubServiceByIdQueryHandler : IRequestHandler<GetSubServiceByIdQuery, OperationResult<GetSubServiceByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetSubServiceByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetSubServiceByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetSubServiceByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetSubServiceByIdQueryResult>> Handle(GetSubServiceByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var SubService = await _unitOfWork.SubServiceRepository.GetByIdAsync(request.Id);

                if (SubService == null)
                {
                    return OperationResult<GetSubServiceByIdQueryResult>.NotFoundResult("SubService not found");
                }

                //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                var result = _mapper.Map<GetSubServiceByIdQueryResult>(SubService);

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                return OperationResult<GetSubServiceByIdQueryResult>.SuccessResult(result);
            }
        }

        //public ValueTask<OperationResult<GetSubServiceByIdQueryResult>> Handle(GetSubServiceByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
