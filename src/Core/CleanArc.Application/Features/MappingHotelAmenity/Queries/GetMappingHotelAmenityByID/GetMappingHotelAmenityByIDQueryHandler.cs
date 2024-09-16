using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging; using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArc.SharedKernel.Extensions;



namespace CleanArc.Application.Features.MappingHotelAmenity.Queries.GetMappingHotelAmenityByID;

internal class GetMappingHotelAmenityByIDQueryHandler : IRequestHandler<GetMappingHotelAmenityByIDQuery, OperationResult<GetMappingHotelAmenityByIDQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetMappingHotelAmenityByIDQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public GetMappingHotelAmenityByIDQueryHandler(IUnitOfWork unitOfWork, ILogger<GetMappingHotelAmenityByIDQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async ValueTask<OperationResult<GetMappingHotelAmenityByIDQueryResult>> Handle(GetMappingHotelAmenityByIDQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            var ageType = await _unitOfWork.AgeTypeRepository.GetByIdAsync(request.Id);

            if (ageType == null)
            {
                return OperationResult<GetMappingHotelAmenityByIDQueryResult>.NotFoundResult("ageType not found");
            }

            //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
            var result = _mapper.Map<GetMappingHotelAmenityByIDQueryResult>(ageType);

            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

            return OperationResult<GetMappingHotelAmenityByIDQueryResult>.SuccessResult(result);
        }
    }

    //public ValueTask<OperationResult<GetAgeTypeByIdQueryResult>> Handle(GetAgeTypeByIdQuery request, CancellationToken cancellationToken)
    //{
    //    throw new NotImplementedException();
    //}
}


