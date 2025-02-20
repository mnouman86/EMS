using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.URL.Commands.DeleteURLCommand;
using CleanArc.Application.Features.URL.Queries.GetAllURLs;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Entities.UserManagement;
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
using CleanArc.Application.Features.UserExperience.Queries.GetUserExperienceById;

namespace CleanArc.Application.Features.URL.Queries.GetURLById
{
    internal class GetURLByIdQueryHandler : IRequestHandler<GetURLByIdQuery, OperationResult<GetURLByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetURLByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetURLByIdQueryHandler(IUnitOfWork unitOfWork,ILogger<GetURLByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;

        }

        public async ValueTask<OperationResult<GetURLByIdQueryResult>> Handle(GetURLByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                //var url = await _unitOfWork.URLRepository.GetByIdAsync(request.Id);

                //if (url == null)
                //{
                //    return OperationResult<GetURLByIdQueryResult>.NotFoundResult("URL not found");
                //}

                ////var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                //var result = _mapper.Map<GetURLByIdQueryResult>(url);

                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                //return OperationResult<GetURLByIdQueryResult>.SuccessResult(result);

                var response = await _unitOfWork.URLRepository.GetByIdAsync(request.Id);

                if (response.Code != 200)
                {
                    return OperationResult<GetURLByIdQueryResult>.FailureResult(
                        response.Message,
                    response.Code
                    );
                }

                var mappedResult = _mapper.Map<GetURLByIdQueryResult>(response.Data);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(mappedResult);

                return OperationResult<GetURLByIdQueryResult>.SuccessResult(
                    mappedResult,
                    response.Code,
                    response.Message
                );
            }
        }
    }
}
