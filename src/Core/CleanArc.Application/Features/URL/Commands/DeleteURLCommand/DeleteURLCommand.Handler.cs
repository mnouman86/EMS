using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging; using CleanArc.Domain.Common;

namespace CleanArc.Application.Features.URL.Commands.DeleteURLCommand;

internal class DeleteURLCommandHandler : IRequestHandler<DeleteURLCommand, OperationResult<ResponseEntity>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly IConfiguration configuration;
    private readonly ILogger<DeleteURLCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor


    public DeleteURLCommandHandler(IUnitOfWork unitOfWork, IAppUserManager userManager, IConfiguration configuration, ILogger<DeleteURLCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        this.configuration = configuration;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async ValueTask<OperationResult<ResponseEntity>> Handle(DeleteURLCommand request, CancellationToken cancellationToken)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request))
        {
            var user = await _userManager.GetUserByIdAsync(request.UserId);

            if (user == null)
                return OperationResult<ResponseEntity>.FailureResult("User Not Found");

            var result = await _unitOfWork.URLRepository.DeleteAsync(request.deleteRequest, user.Id);

            await _unitOfWork.CommitAsync();

            return OperationResult<ResponseEntity>.SuccessResult(result);
        }
    }
}