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

namespace CleanArc.Application.Features.AdvertisementPlace.Queries.GetAdvertisementPlaceById
{
    internal class GetAdvertisementPlaceByIdQueryHandler : IRequestHandler<GetAdvertisementPlaceByIdQuery, OperationResult<GetAdvertisementPlaceByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAdvertisementPlaceByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetAdvertisementPlaceByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetAdvertisementPlaceByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetAdvertisementPlaceByIdQueryResult>> Handle(GetAdvertisementPlaceByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var AdvertisementPlace = await _unitOfWork.AdvertisementPlaceRepository.GetByIdAsync(request.Id);

                if (AdvertisementPlace == null)
                {
                    return OperationResult<GetAdvertisementPlaceByIdQueryResult>.NotFoundResult("AdvertisementPlace not found");
                }

                //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                var result = _mapper.Map<GetAdvertisementPlaceByIdQueryResult>(AdvertisementPlace);

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                return OperationResult<GetAdvertisementPlaceByIdQueryResult>.SuccessResult(result);
            }
        }

        //public ValueTask<OperationResult<GetAdvertisementPlaceByIdQueryResult>> Handle(GetAdvertisementPlaceByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
