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

namespace CleanArc.Application.Features.FAQs.Queries.GetFAQsById
{
    internal class GetFAQsByIdQueryHandler : IRequestHandler<GetFAQsByIdQuery, OperationResult<GetFAQsByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetFAQsByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetFAQsByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetFAQsByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetFAQsByIdQueryResult>> Handle(GetFAQsByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var FAQs = await _unitOfWork.FAQsRepository.GetByIdAsync(request.Id);

                if (FAQs == null)
                {
                    return OperationResult<GetFAQsByIdQueryResult>.NotFoundResult("FAQs not found");
                }

                //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                var result = _mapper.Map<GetFAQsByIdQueryResult>(FAQs);

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                return OperationResult<GetFAQsByIdQueryResult>.SuccessResult(result);
            }
        }

        //public ValueTask<OperationResult<GetFAQsByIdQueryResult>> Handle(GetFAQsByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
