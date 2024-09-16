using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.MappingHotelAmenity.Queries.GetMappingHotelAmenityByID;
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

namespace CleanArc.Application.Features.MappingRoomAmenity.Query.GetMappingRoomAmenityById
{
    internal class GetMappingRoomAmenityByIdQueryHandler : IRequestHandler<GetMappingRoomAmenityByIdQuery, OperationResult<GetMappingRoomAmenityByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetMappingRoomAmenityByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetMappingRoomAmenityByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetMappingRoomAmenityByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetMappingRoomAmenityByIdQueryResult>> Handle(GetMappingRoomAmenityByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var ageType = await _unitOfWork.AgeTypeRepository.GetByIdAsync(request.Id);

                if (ageType == null)
                {
                    return OperationResult<GetMappingRoomAmenityByIdQueryResult>.NotFoundResult("ageType not found");
                }

                //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                var result = _mapper.Map<GetMappingRoomAmenityByIdQueryResult>(ageType);

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                return OperationResult<GetMappingRoomAmenityByIdQueryResult>.SuccessResult(result);
            }
        }

        //public ValueTask<OperationResult<GetAgeTypeByIdQueryResult>> Handle(GetAgeTypeByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
