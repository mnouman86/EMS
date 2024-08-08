using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Business.Command.DeleteBusinessCommand
{
    internal class DeleteBusinessCommandHandler : IRequestHandler<DeleteBusinessCommand, OperationResult<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppUserManager _userManager;
        private readonly IConfiguration configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteBusinessCommandHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor
                                                                    //private readonly IUnitOfWork _unitOfWork;
                                                                    //private readonly IAppUserManager _userManager;


        public DeleteBusinessCommandHandler(IUnitOfWork unitOfWork, IAppUserManager userManager, IConfiguration configuration, IMapper mapper, ILogger<DeleteBusinessCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            this.configuration = configuration;
            _mapper = mapper;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            //_unitOfWork = unitOfWork;
            //_userManager = userManager;
        }
        public async ValueTask<OperationResult<bool>> Handle(DeleteBusinessCommand request, CancellationToken cancellationToken)
        {
            using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
            {

                var user = await _userManager.GetUserByIdAsync(request.UserId);
                if (user == null)
                    return OperationResult<bool>.FailureResult("User Not Found");

                //await _unitOfWork.URLRepository.AddAsync(new Domain.Entities.UserManagement.URL()
                //{ CreatedBy = user.Id, Path = request.Path, Title = request.Title, Description = request.Description/*, CreatedTime=DateTime.Now*/ });

                //await _unitOfWork.CommitAsync();
                //(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);

                //return OperationResult<bool>.SuccessResult(true);
                //await _unitOfWork.AgeTypeRepository.DeleteAsync(new Domain.Entities.AgeType.AgeType()
                // { UpdatedBy = user.Id, ID = request.ID });
                await _unitOfWork.BusinessRepository.DeleteAsync(request.SelectedIds, user.Id, request.CultureId);
                await _unitOfWork.CommitAsync();
                //  (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
                return OperationResult<bool>.SuccessResult(true);
            }
        }


    }
}
