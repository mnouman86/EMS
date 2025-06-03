using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.AgeType.Queries.GetAllAgeType;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging; 
using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArc.Application.Features.Activity.Queries.GetAllActivity;
using CleanArc.Application.Features.LastMinuteDeal.Queries.GetAllLastMinuteDeal;
using CleanArc.Application.Features.Activity.Queries.GetAllSearchActivityDetail;
using CleanArc.Application.Features.SearchHotelDetail.Queries.GetAllSearchHotelDetail;
using CleanArc.Domain.Entities.Language;
using CleanArc.Domain.Entities.SearchHotelDetail;
using Microsoft.AspNetCore.Mvc;

namespace CleanArc.Application.Features.Language.Queries.GetAllLastMinuteDeal;

internal class GetAllLastMinuteDealQueryHandler : IRequestHandler<GetAllLastMinuteDealQuery, OperationResult<List<GetAllLastMinuteDealQueryResult>>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllLastMinuteDealQueryHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor


    public GetAllLastMinuteDealQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<GetAllLastMinuteDealQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;

    }

    public async ValueTask<OperationResult<List<GetAllLastMinuteDealQueryResult>>> Handle(GetAllLastMinuteDealQuery request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            //var language = await _unitOfWork.LanguageRepository.GetAllAsync(request.searchRequest);

            ////var resultCheck = uRLs.Select(c => new GetAllProductsQueryResult(c.Id, c.Path, c.Title, c.Description)).ToList();
            //var result = _mapper.Map<List<GetAllLastMinuteDealQueryResult>>(language);
            //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
            //return OperationResult<List<GetAllLastMinuteDealQueryResult>>.SuccessResult(result);

            var response = await _unitOfWork.LastMinuteDealRepository.GetAllAsync(request.searchRequest);

            if (response.Code != 200)
            {
                
                return OperationResult<List<GetAllLastMinuteDealQueryResult>>.FailureResult(
                    response.Message,
                response.Code
                );
            }
            var mappedResult = _mapper.Map<List<GetAllLastMinuteDealQueryResult>>(response.Data);
            //GetAllLastMinuteDealQueryResult detail = new GetAllLastMinuteDealQueryResult();

            //detail.HotelDetail =mappedResult ;//_mapper.Map<List<GetAllLastMinuteDeals>>(response);
              
            //if (detail?.HotelDetail != null && detail?.HotelDetail?.Count > 0)
            //{
            //    detail.StartDate = detail.HotelDetail.Min(x => x.StartDate);
            //    detail.EndDate = detail.HotelDetail.Max(x => x.EndDate);
            //}
            //else
            //{
            //    detail.StartDate = null; // or DateTime.MinValue or default
            //    detail.EndDate = null;
            //}
            

            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

            return OperationResult<List<GetAllLastMinuteDealQueryResult>>.SuccessResult(
                mappedResult,
                response.Code,
                response.Message,
                    response.TotalCount
            );
        }
    }
}


