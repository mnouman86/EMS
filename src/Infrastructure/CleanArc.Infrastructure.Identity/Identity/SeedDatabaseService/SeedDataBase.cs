using CleanArc.Domain.Entities.User;
using CleanArc.Infrastructure.Identity.Identity.Manager;
using Microsoft.EntityFrameworkCore;

namespace CleanArc.Infrastructure.Identity.Identity.SeedDatabaseService;

public interface ISeedDataBase
{
    Task Seed();
}

public class SeedDataBase : ISeedDataBase
{
    private readonly AppUserManager _userManager;
    private readonly AppRoleManager _roleManager;

    public SeedDataBase(AppUserManager userManager, AppRoleManager roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task Seed()
    {
        // Role names used across the EMS modules. "admin" is legacy; the others
        // mirror the actor matrix in EMS_User_Stories.md + EMS_Use_Cases_Fee_Inventory_Finance.
        var roles = new[] { "admin", "principal", "accountant", "teacher", "parent" };
        foreach (var name in roles)
        {
            if (!_roleManager.Roles.AsNoTracking().Any(r => r.Name.Equals(name)))
                await _roleManager.CreateAsync(new Role { Name = name });
        }

        if (!_userManager.Users.AsNoTracking().Any(u => u.UserName.Equals("admin")))
        {
            var user = new User
            {
                UserName = "admin",
                Email = "admin@site.com",
                PhoneNumberConfirmed = true
            };

            await  _userManager.CreateAsync(user, "qw123321");
            await _userManager.AddToRoleAsync(user,"admin");
        }
    }
}