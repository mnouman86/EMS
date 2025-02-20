using CleanArc.Application.Contracts.Persistence;
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
using CleanArc.Application.Features.Currency.Queries.GetCurrencyById;

namespace CleanArc.Application.Features.Country.Queries.GetCountryById;

internal class GetCountryByIdQueryHandler : IRequestHandler<GetCountryByIdQuery, OperationResult<GetCountryByIdQueryResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetCountryByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public GetCountryByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetCountryByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    public async ValueTask<OperationResult<GetCountryByIdQueryResult>> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            //var country
            //    = await _unitOfWork.CountryRepository.GetByIdAsync(request.Id);

            //if (country == null)
            //{
            //    return OperationResult<GetCountryByIdQueryResult>.NotFoundResult("country not found");
            //}

            ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
            //var result = _mapper.Map<GetCountryByIdQueryResult>(country);

            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

            //return OperationResult<GetCountryByIdQueryResult>.SuccessResult(result);

            var response = await _unitOfWork.CountryRepository.GetByIdAsync(request.Id);

            if (response.Code != 200)
            {
                return OperationResult<GetCountryByIdQueryResult>.FailureResult(
                    response.Message,
                response.Code
                );
            }

            var mappedResult = _mapper.Map<GetCountryByIdQueryResult>(response.Data);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            return OperationResult<GetCountryByIdQueryResult>.SuccessResult(
                mappedResult,
                response.Code,
                response.Message
            );
        }
    }

}



