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
using CleanArc.Application.Features.KBDetail.Queries.GetKBDetailByIdAll;

namespace CleanArc.Application.Features.KBDetail.Queries.GetKBDetailByAllId
{
    internal class GetKBDetailByIdAllQueryHandler : IRequestHandler<GetKBDetailByIdAllQuery, OperationResult<GetKBDetailByIdAllQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetKBDetailByIdAllQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetKBDetailByIdAllQueryHandler(IUnitOfWork unitOfWork, ILogger<GetKBDetailByIdAllQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetKBDetailByIdAllQueryResult>> Handle(GetKBDetailByIdAllQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var KBDetail = await _unitOfWork.KBDetailRepository.GetByIdAllAsync(request.Id);

                if (KBDetail == null)
                {
                    return OperationResult<GetKBDetailByIdAllQueryResult>.NotFoundResult("KBDetail not found");
                }

                //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                var result = _mapper.Map<GetKBDetailByIdAllQueryResult>(KBDetail);

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                return OperationResult<GetKBDetailByIdAllQueryResult>.SuccessResult(result);
            }
        }

        //public ValueTask<OperationResult<GetKBDetailByIdQueryResult>> Handle(GetKBDetailByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
