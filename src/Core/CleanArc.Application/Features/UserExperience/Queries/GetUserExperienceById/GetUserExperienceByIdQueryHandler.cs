using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.URL.Queries.GetURLById;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.UserExperience.Queries.GetUserExperienceById
{
    internal class GetUserExperienceByIdQueryHandler : IRequestHandler<GetUserExperienceByIdQuery, OperationResult<GetUserExperienceByIdQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetUserExperienceByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



        public GetUserExperienceByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetUserExperienceByIdQueryHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async ValueTask<OperationResult<GetUserExperienceByIdQueryResult>> Handle(GetUserExperienceByIdQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var UserExperience = await _unitOfWork.UserExperienceRepository.GetByIdAsync(request.Id);

                if (UserExperience == null)
                {
                    return OperationResult<GetUserExperienceByIdQueryResult>.NotFoundResult("UserExperience not found");
                }

                //var result = new GetURLByIdQueryResult(url.Id, url.Path, url.Title, url.Description);
                var result = _mapper.Map<GetUserExperienceByIdQueryResult>(UserExperience);

                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

                return OperationResult<GetUserExperienceByIdQueryResult>.SuccessResult(result);
            }
        }

        //public ValueTask<OperationResult<GetUserExperienceByIdQueryResult>> Handle(GetUserExperienceByIdQuery request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
