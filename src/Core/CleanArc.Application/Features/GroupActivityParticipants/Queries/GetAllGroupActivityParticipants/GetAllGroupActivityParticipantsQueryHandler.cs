using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging; using CleanArc.Domain.Common;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.GroupActivityParticipants.Queries.GetAllGroupActivityParticipants
{
    internal class GetAllGroupActivityParticipantsQueryHandler : IRequestHandler<GetAllGroupActivityParticipantsQuery, OperationResult<List<GetAllGroupActivityParticipantsQueryResult>>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllGroupActivityParticipantsQueryHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor


        public GetAllGroupActivityParticipantsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<GetAllGroupActivityParticipantsQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;

        }

        public async ValueTask<OperationResult<List<GetAllGroupActivityParticipantsQueryResult>>> Handle(GetAllGroupActivityParticipantsQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var Product = await _unitOfWork.GroupActivityParticipantsRepository.GetAllAsync(request.searchRequest);

                //var resultCheck = uRLs.Select(c => new GetAllProductsQueryResult(c.Id, c.Path, c.Title, c.Description)).ToList();
                var result = _mapper.Map<List<GetAllGroupActivityParticipantsQueryResult>>(Product);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return OperationResult<List<GetAllGroupActivityParticipantsQueryResult>>.SuccessResult(result);
            }
        }
    }

}
