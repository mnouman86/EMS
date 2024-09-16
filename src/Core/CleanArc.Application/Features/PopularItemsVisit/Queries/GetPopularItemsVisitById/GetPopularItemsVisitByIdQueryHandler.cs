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

namespace CleanArc.Application.Features.PopularItemsVisit.Queries.GetPopularItemsVisitById
{
    internal class GetPopularItemsVisitByIdQueryHandler : IRequestHandler<GetPopularItemsVisitByIdQuery, OperationResult<GetPopularItemsVisitByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetPopularItemsVisitByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetPopularItemsVisitByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetPopularItemsVisitByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetPopularItemsVisitByIdQueryResult>> Handle(GetPopularItemsVisitByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var PopularItemsVisit = await _unitOfWork.PopularItemsVisitRepository.GetByIdAsync(request.Id);

                if (PopularItemsVisit == null)
                {
                    return OperationResult<GetPopularItemsVisitByIdQueryResult>.NotFoundResult("PopularItemsVisit not found");
                }

                //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                var result = _mapper.Map<GetPopularItemsVisitByIdQueryResult>(PopularItemsVisit);

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                return OperationResult<GetPopularItemsVisitByIdQueryResult>.SuccessResult(result);
            }
        }

        //public ValueTask<OperationResult<GetPopularItemsVisitByIdQueryResult>> Handle(GetPopularItemsVisitByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
