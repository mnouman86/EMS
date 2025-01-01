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

namespace CleanArc.Application.Features.Section.Queries.GetAllSections
{
    internal class GetAllSectionsQueryHandler : IRequestHandler<GetAllSectionsQuery, OperationResult<List<GetAllSectionsQueryResult>>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllSectionsQueryHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor


        public GetAllSectionsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<GetAllSectionsQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;

        }

        public async ValueTask<OperationResult<List<GetAllSectionsQueryResult>>> Handle(GetAllSectionsQuery request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {
                var Section = await _unitOfWork.SectionRepository.GetAllAsync(request.searchRequest);

                //var resultCheck = uRLs.Select(c => new GetAllProductsQueryResult(c.Id, c.Path, c.Title, c.Description)).ToList();
                var result = _mapper.Map<List<GetAllSectionsQueryResult>>(Section);
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return OperationResult<List<GetAllSectionsQueryResult>>.SuccessResult(result);
            }
        }
    }

}

