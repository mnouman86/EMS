using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArc.SharedKernel.Extensions;



namespace CleanArc.Application.Features.MappingCarAmenity.Queries.GetMappingCarAmenityByID;

internal class GetMappingCarAmenityByIDQueryHandler : IRequestHandler<GetMappingCarAmenityByIDQuery, OperationResult<GetMappingCarAmenityByIDQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetMappingCarAmenityByIDQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public GetMappingCarAmenityByIDQueryHandler(IUnitOfWork unitOfWork, ILogger<GetMappingCarAmenityByIDQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async ValueTask<OperationResult<GetMappingCarAmenityByIDQueryResult>> Handle(GetMappingCarAmenityByIDQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            var ageType = await _unitOfWork.AgeTypeRepository.GetByIdAsync(request.Id);

            if (ageType == null)
            {
                return OperationResult<GetMappingCarAmenityByIDQueryResult>.NotFoundResult("ageType not found");
            }

            //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
            var result = _mapper.Map<GetMappingCarAmenityByIDQueryResult>(ageType);

            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

            return OperationResult<GetMappingCarAmenityByIDQueryResult>.SuccessResult(result);
        }
    }

    //public ValueTask<OperationResult<GetAgeTypeByIdQueryResult>> Handle(GetAgeTypeByIdQuery request, CancellationToken cancellationToken)
    //{
    //    throw new NotImplementedException();
    //}
}


