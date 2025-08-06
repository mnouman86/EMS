using AutoMapper;
using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.UserProfile;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.User;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CleanArc.Application.Features.UserProfile.Queries.GetUserProfile;

internal class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, OperationResult<GetUserProfileQueryResponse>>
{

    private readonly IAppUserManager _userManager;
    private readonly IMapper _mapper;

    private readonly IHttpContextAccessor _contextAccessor;

    public GetUserProfileQueryHandler(IAppUserManager userManager, IMapper mapper, IHttpContextAccessor contextAccessor)
    {
        _userManager = userManager;
        _contextAccessor = contextAccessor;
        _mapper = mapper;

    }

   
    public async ValueTask<OperationResult<GetUserProfileQueryResponse>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        
      //string id=  _contextAccessor.HttpContext?.User?.Identity?.Name;
        var userId = int.Parse(_contextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var user = await _userManager.GetUserByIdAsync(request.UserId);
        if (user == null)
        return OperationResult<GetUserProfileQueryResponse>.FailureResult("Unauthorized Access.");

        var profile= new GetUserProfileQueryResponse
        {
            Name = user.Name,
            FamilyName = user.FamilyName,
            Email = user.Email!,
            PhoneNumber = user.PhoneNumber,
            GenderId = user.GenderId,
            NationalityId = user.NationalityId,
            DateOfBirth = user.DateOfBirth,
            Address = user.Address
        };
        

        return OperationResult<GetUserProfileQueryResponse>.SuccessResult(profile);
    }
}