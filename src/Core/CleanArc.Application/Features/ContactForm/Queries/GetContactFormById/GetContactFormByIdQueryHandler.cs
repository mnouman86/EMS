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
using CleanArc.Application.Features.Country.Queries.GetCountryById;

namespace CleanArc.Application.Features.ContactForm.Queries.GetContactFormById
{
    internal class GetContactFormByIdQueryHandler : IRequestHandler<GetContactFormByIdQuery, OperationResult<GetContactFormByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetContactFormByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetContactFormByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetContactFormByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetContactFormByIdQueryResult>> Handle(GetContactFormByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                //var ContactForm = await _unitOfWork.ContactFormRepository.GetByIdAsync(request.searchRequestById);

                //if (ContactForm == null)
                //{
                //    return OperationResult<GetContactFormByIdQueryResult>.NotFoundResult("ContactForm not found");
                //}

                ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                //var result = _mapper.Map<GetContactFormByIdQueryResult>(ContactForm);

                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                //return OperationResult<GetContactFormByIdQueryResult>.SuccessResult(result);

                var response = await _unitOfWork.ContactFormRepository.GetByIdAsync(request.searchRequestById);

                if (response.Code != 200)
                {
                    return OperationResult<GetContactFormByIdQueryResult>.FailureResult(
                        response.Message,
                    response.Code
                    );
                }

                var mappedResult = _mapper.Map<GetContactFormByIdQueryResult>(response.Data);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                return OperationResult<GetContactFormByIdQueryResult>.SuccessResult(
                    mappedResult,
                    response.Code,
                    response.Message
                );
            }
        }

        //public ValueTask<OperationResult<GetContactFormByIdQueryResult>> Handle(GetContactFormByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
