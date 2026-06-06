using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Result;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArc.Application.Features.Result.Command.CreateResultSessionCommand;

internal class CreateResultSessionCommandHandler : IRequestHandler<CreateResultSessionCommand, OperationResult<ResponseEntity>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly ILogger<CreateResultSessionCommandHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateResultSessionCommandHandler(
        IUnitOfWork unitOfWork,
        IAppUserManager userManager,
        ILogger<CreateResultSessionCommandHandler> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async ValueTask<OperationResult<ResponseEntity>> Handle(CreateResultSessionCommand request, CancellationToken cancellationToken)
    {
        using var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, request);

        var user = await _userManager.GetUserByIdAsync(request.UserId);
        if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");

        var result = await _unitOfWork.ResultRepository.CreateSessionAsync(new CreateResultSessionDTO
        {
            Name = request.Name,
            MaxWritten = request.MaxWritten,
            MaxOral = request.MaxOral,
            MaxAttribute = request.MaxAttribute,
            WeightWritten = request.WeightWritten,
            WeightOral = request.WeightOral,
            WeightPerformance = request.WeightPerformance,
            RoundingDecimals = request.RoundingDecimals,
            CreatedBy = user.Id
        });

        await _unitOfWork.CommitAsync();
        (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(true);
        return OperationResult<ResponseEntity>.SuccessResult(result);
    }
}
