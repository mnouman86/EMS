using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.AgeType.Queries.GetAllAgeType;
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

namespace CleanArc.Application.Features.MappingHotelLanguage.Queries.GetAllMappingHotelLanguage;

internal class GetAllMappingHotelLanguageQueryHandler : IRequestHandler<GetAllMappingHotelLanguageQuery, OperationResult<List<GetAllMappingHotelLanguageQueryResult>>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllMappingHotelLanguageQueryHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor


    public GetAllMappingHotelLanguageQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<GetAllMappingHotelLanguageQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;

    }

    public async ValueTask<OperationResult<List<GetAllMappingHotelLanguageQueryResult>>> Handle(GetAllMappingHotelLanguageQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            var Product = await _unitOfWork.AgeTypeRepository.GetAllAsync(request.searchRequest);

            //var resultCheck = uRLs.Select(c => new GetAllProductsQueryResult(c.Id, c.Path, c.Title, c.Description)).ToList();
            var result = _mapper.Map<List<GetAllMappingHotelLanguageQueryResult>>(Product);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
            return OperationResult<List<GetAllMappingHotelLanguageQueryResult>>.SuccessResult(result);
        }
    }
}



