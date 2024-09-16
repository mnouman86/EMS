using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.AgeType.Queries.GetAgeTypeById;
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

namespace CleanArc.Application.Features.Amenities.Queries.GetAmenitiesById
{
    internal class GetAmenityByIdQueryHandler : IRequestHandler<GetAmenityByIdQuery, OperationResult<GetAmenityByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAmenityByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetAmenityByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetAmenityByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetAmenityByIdQueryResult>> Handle(GetAmenityByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var amenity = await _unitOfWork.AmenityRepository.GetByIdAsync(request.Id);

                if (amenity == null)
                {
                    return OperationResult<GetAmenityByIdQueryResult>.NotFoundResult("URL not found");
                }

                //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                var result = _mapper.Map<GetAmenityByIdQueryResult>(amenity);

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                return OperationResult<GetAmenityByIdQueryResult>.SuccessResult(result);
            }
        }

       
    }
}
