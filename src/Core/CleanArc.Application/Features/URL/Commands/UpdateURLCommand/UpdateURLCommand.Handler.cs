using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.URL.Commands.DeleteURLCommand;
using CleanArc.Application.Features.URL.Queries.GetURLById;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Entities.UserManagement;
using CleanArc.SharedKernel.Extensions;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging; using CleanArc.Domain.Common;

namespace CleanArc.Application.Features.URL.Commands.UpdateURLCommand;

internal class UpdateURLCommandHandler : IRequestHandler<UpdateURLCommand, OperationResult<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly IConfiguration configuration;
    private readonly ILogger<UpdateURLCommandHandler> _logger;
    private readonly IMapper _mapper; 
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor



    public UpdateURLCommandHandler(IUnitOfWork unitOfWork, IAppUserManager userManager, IConfiguration configuration, ILogger<UpdateURLCommandHandler> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        this.configuration = configuration;
        _logger = logger;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;

    }

    public async ValueTask<OperationResult<bool>> Handle(UpdateURLCommand request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            var user = await _userManager.GetUserByIdAsync(request.UserId);

            if (user == null)
                return OperationResult<bool>.FailureResult("User Not Found");

          var result=  await _unitOfWork.URLRepository.UpdateAsync(new Domain.Entities.UserManagement.URL()
            { Id = request.Id, UpdatedBy = user.Id, Path = request.Path, Title = request.Title, Description = request.Description/*, CreatedTime=DateTime.Now*/ });
            
            await _unitOfWork.CommitAsync();
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);

            return OperationResult<bool>.SuccessResult(true);
        }
    }
}