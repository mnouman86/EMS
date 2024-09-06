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

namespace CleanArc.Application.Features.KBCAttraction.Queries.GetKBCAttractionById
{
    internal class GetKBCAttractionByIdQueryHandler : IRequestHandler<GetKBCAttractionByIdQuery, OperationResult<GetKBCAttractionByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetKBCAttractionByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetKBCAttractionByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetKBCAttractionByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetKBCAttractionByIdQueryResult>> Handle(GetKBCAttractionByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var KBCAttraction = await _unitOfWork.KBCAttractionRepository.GetByIdAsync(request.Id);

                if (KBCAttraction == null)
                {
                    return OperationResult<GetKBCAttractionByIdQueryResult>.NotFoundResult("KBCAttraction not found");
                }

                //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                var result = _mapper.Map<GetKBCAttractionByIdQueryResult>(KBCAttraction);

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                return OperationResult<GetKBCAttractionByIdQueryResult>.SuccessResult(result);
            }
        }

        //public ValueTask<OperationResult<GetKBCAttractionByIdQueryResult>> Handle(GetKBCAttractionByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
