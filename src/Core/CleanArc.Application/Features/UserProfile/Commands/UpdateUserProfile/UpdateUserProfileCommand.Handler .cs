using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Features.Users.Queries.GetUsers;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.UserSignUpRewards;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.User;
using CleanArc.SharedKernel.Extensions;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 
using System.Security.Claims;

namespace CleanArc.Application.Features.UserProfile.Commands.UpdateUserProfile;

internal class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, OperationResult<ResponseEntity>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppUserManager _userManager;
    private readonly ILogger<UpdateUserProfileCommandHandler> _logger;
    private readonly IHttpContextAccessor _contextAccessor;

    public UpdateUserProfileCommandHandler(IUnitOfWork unitOfWork, IAppUserManager userRepository, 
        ILogger<UpdateUserProfileCommandHandler> logger, IHttpContextAccessor contextAccessor)
    {
        _userManager = userRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
    }

    public async ValueTask<OperationResult<ResponseEntity>> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var userId = int.Parse(_contextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var user = await _userManager.GetUserByIdAsync(request.UserId);
        if (user == null)
            return OperationResult<ResponseEntity>.FailureResult("Unauthorized Access.");

        user.Name = request.Name;
        user.FamilyName = request.FamilyName;
        user.PhoneNumber = request.PhoneNumber;
        user.GenderId = request.GenderId;
        user.NationalityId = request.NationalityId;
        user.DateOfBirth = request.DateOfBirth;
        user.Address = request.Address;

       var result= await _userManager.UpdateUserAsync(user);
        //return OperationResult<bool>.SuccessResult(true, 200, "User Created Successfully");
        ResponseEntity entity = new ResponseEntity();
        if (result.Succeeded)
        {
            entity.Code = 200;
            entity.IsSuccess = result.Succeeded;
            entity.Message = "Profile updated successfully.";
            return OperationResult<ResponseEntity>.SuccessResult(entity);
        }

        return OperationResult<ResponseEntity>.FailureResult("Something went wrong.");
    }
}