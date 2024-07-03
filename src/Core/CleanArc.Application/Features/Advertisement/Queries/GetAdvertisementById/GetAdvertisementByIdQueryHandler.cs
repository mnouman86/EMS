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

namespace CleanArc.Application.Features.Advertisement.Queries.GetAdvertisementById
{
    internal class GetAdvertisementByIdQueryHandler : IRequestHandler<GetAdvertisementByIdQuery, OperationResult<GetAdvertisementByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAdvertisementByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetAdvertisementByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetAdvertisementByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetAdvertisementByIdQueryResult>> Handle(GetAdvertisementByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var Advertisement = await _unitOfWork.AdvertisementRepository.GetByIdAsync(request.Id);

                if (Advertisement == null)
                {
                    return OperationResult<GetAdvertisementByIdQueryResult>.NotFoundResult("Advertisement not found");
                }

                //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                var result = _mapper.Map<GetAdvertisementByIdQueryResult>(Advertisement);

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                return OperationResult<GetAdvertisementByIdQueryResult>.SuccessResult(result);
            }
        }

        //public ValueTask<OperationResult<GetAdvertisementByIdQueryResult>> Handle(GetAdvertisementByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
