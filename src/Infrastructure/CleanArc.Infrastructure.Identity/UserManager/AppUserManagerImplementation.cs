using Azure.Core;
using CleanArc.Application.Contracts.Identity;
using CleanArc.Domain.Entities.User;
using CleanArc.Infrastructure.Identity.Identity.Dtos;
using CleanArc.Infrastructure.Identity.Identity.Manager;
using CleanArc.SharedKernel.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; using CleanArc.Domain.Common;

namespace CleanArc.Infrastructure.Identity.UserManager;

public class AppUserManagerImplementation : IAppUserManager
{
    private readonly AppUserManager _userManager;
    private readonly ILogger<AppUserManagerImplementation> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor

    public AppUserManagerImplementation(AppUserManager userManager,ILogger<AppUserManagerImplementation> logger, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IdentityResult> CreateUser(User user)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, user))
        {
            var newUser=await _userManager.CreateAsync(user);
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(newUser);
            return newUser;
        }
    }

    public Task<bool> IsExistUser(string phoneNumber)
    {
        return _userManager.Users.AnyAsync(c => c.PhoneNumber == phoneNumber);
    }

    public Task<bool> IsExistUserName(string userName)
    {
        return _userManager.Users.AnyAsync(c => c.UserName.Equals(userName));
    }

    public async Task<string> GeneratePhoneNumberConfirmationToken(User user, string phoneNumber)
    {
        return await _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);
    }

    public Task<User> GetUserByCode(string code)
    {
        return _userManager.Users.FirstOrDefaultAsync(c => c.GeneratedCode.Equals(code));
    }

    public async Task<IdentityResult> ChangePhoneNumber(User user, string phoneNumber, string code)
    {
        return await _userManager.ChangePhoneNumberAsync(user, phoneNumber, code);
    }

    public async Task<IdentityResult> VerifyUserCode(User user, string code)
    {
        var confirmationResult = await _userManager.VerifyUserTokenAsync(
            user, CustomIdentityConstants.OtpPasswordLessLoginProvider, CustomIdentityConstants.OtpPasswordLessLoginPurpose, code);

        if (confirmationResult)
            await _userManager.UpdateSecurityStampAsync(user);

        return confirmationResult
            ? IdentityResult.Success
            : IdentityResult.Failed(new IdentityError() { Description = "Incorrect Code" });
    }

    public Task<string> GenerateOtpCode(User user)
    {
        return _userManager.GenerateUserTokenAsync(
            user, CustomIdentityConstants.OtpPasswordLessLoginProvider, CustomIdentityConstants.OtpPasswordLessLoginPurpose);
    }

    public Task<User> GetUserByPhoneNumber(string phoneNumber)
    {
        return _userManager.Users.FirstOrDefaultAsync(c => c.PhoneNumber.Equals(phoneNumber));
    }

    public async Task<SignInResult> AdminLogin(User user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password) ? SignInResult.Success : SignInResult.Failed;
    }

    public Task<User> GetByUserName(string userName)
    {
        return _userManager.FindByNameAsync(userName);
    }

    public async Task<User> GetUserByIdAsync(int userId)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, userId))
        {
            var user= await _userManager.FindByIdAsync(userId.ToString());
            (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(user);
            return user;
        }
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _userManager.Users.AsNoTracking().ToListAsync();
    }

    public async Task<IdentityResult> CreateUserWithPasswordAsync(User user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<IdentityResult> AddUserToRoleAsync(User user, Role role)
    {
        ArgumentNullException.ThrowIfNull(role, nameof(role));
        ArgumentNullException.ThrowIfNull(role.Name, nameof(role.Name));

        return await _userManager.AddToRoleAsync(user, role.Name);
    }

    public async Task<IdentityResult> IncrementAccessFailedCountAsync(User user)
    {
       

        return await _userManager.AccessFailedAsync(user);
    }

    public async Task<bool> IsUserLockedOutAsync(User user)
    {
        var lockoutEndDate = await _userManager.GetLockoutEndDateAsync(user);

        return (lockoutEndDate.HasValue && lockoutEndDate.Value > DateTimeOffset.Now);
    }

    public async Task ResetUserLockoutAsync(User user)
    {
        await _userManager.SetLockoutEndDateAsync(user, null);
         await _userManager.ResetAccessFailedCountAsync(user);
    }

    public async Task UpdateUserAsync(User user)
    {
        await _userManager.UpdateAsync(user);
    }

    public async Task UpdateSecurityStampAsync(User user)
    {
        await _userManager.UpdateSecurityStampAsync(user);
    }
}