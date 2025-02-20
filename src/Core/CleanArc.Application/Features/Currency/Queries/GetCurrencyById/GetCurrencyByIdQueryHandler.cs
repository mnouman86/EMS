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
using CleanArc.Application.Features.CustomerReview.Queries.GetCustomerReviewById;

namespace CleanArc.Application.Features.Currency.Queries.GetCurrencyById
{
    internal class GetCurrencyByIdQueryHandler : IRequestHandler<GetCurrencyByIdQuery, OperationResult<GetCurrencyByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetCurrencyByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetCurrencyByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetCurrencyByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetCurrencyByIdQueryResult>> Handle(GetCurrencyByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                //var Currency = await _unitOfWork.CurrencyRepository.GetByIdAsync(request.Id);

                //if (Currency == null)
                //{
                //    return OperationResult<GetCurrencyByIdQueryResult>.NotFoundResult("Currency not found");
                //}

                ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                //var result = _mapper.Map<GetCurrencyByIdQueryResult>(Currency);

                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                //return OperationResult<GetCurrencyByIdQueryResult>.SuccessResult(result);

                var response = await _unitOfWork.CurrencyRepository.GetByIdAsync(request.Id);

                if (response.Code != 200)
                {
                    return OperationResult<GetCurrencyByIdQueryResult>.FailureResult(
                        response.Message,
                    response.Code
                    );
                }

                var mappedResult = _mapper.Map<GetCurrencyByIdQueryResult>(response.Data);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                return OperationResult<GetCurrencyByIdQueryResult>.SuccessResult(
                    mappedResult,
                    response.Code,
                    response.Message
                );
            }
        }

        //public ValueTask<OperationResult<GetCurrencyByIdQueryResult>> Handle(GetCurrencyByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
