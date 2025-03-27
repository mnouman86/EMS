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
using CleanArc.Application.Features.MappingHotelLanguage.Queries.GetMappingHotelLanguageById;



namespace CleanArc.Application.Features.AmenityMapping.Queries.GetAmenityMappingByID;

internal class GetAmenityMappingByIDQueryHandler : IRequestHandler<GetAmenityMappingByIDQuery, OperationResult<GetAmenityMappingByIDQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAmenityMappingByIDQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public GetAmenityMappingByIDQueryHandler(IUnitOfWork unitOfWork, ILogger<GetAmenityMappingByIDQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async ValueTask<OperationResult<GetAmenityMappingByIDQueryResult>> Handle(GetAmenityMappingByIDQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            //var ageType = await _unitOfWork.AgeTypeRepository.GetByIdAsync(request.searchRequestById);

            //if (ageType == null)
            //{
            //    return OperationResult<GetAmenityMappingByIDQueryResult>.NotFoundResult("ageType not found");
            //}

            ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
            //var result = _mapper.Map<GetAmenityMappingByIDQueryResult>(ageType);

            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

            //return OperationResult<GetAmenityMappingByIDQueryResult>.SuccessResult(result);

            var response = await _unitOfWork.AmenityMappingRepository.GetByIdAsync(request.searchRequestById);

            if (response.Code != 200)
            {
                return OperationResult<GetAmenityMappingByIDQueryResult>.FailureResult(
                    response.Message,
                response.Code
                );
            }

            var mappedResult = _mapper.Map<GetAmenityMappingByIDQueryResult>(response.Data);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            return OperationResult<GetAmenityMappingByIDQueryResult>.SuccessResult(
                mappedResult,
                response.Code,
                response.Message
            );
        }
    }

    //public ValueTask<OperationResult<GetAgeTypeByIdQueryResult>> Handle(GetAgeTypeByIdQuery request, CancellationToken cancellationToken)
    //{
    //    throw new NotImplementedException();
    //}
}


