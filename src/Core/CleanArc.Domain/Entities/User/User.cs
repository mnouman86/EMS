using CleanArc.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace CleanArc.Domain.Entities.User;

public class User:IdentityUser<int>,IEntity
{
    public User()
    {
        this.GeneratedCode = Guid.NewGuid().ToString().Substring(0, 8);
    }

    public string Name { get; set; }
    public string FamilyName { get; set; }
    public string GeneratedCode { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public int? NationalityId { get; set; }
    public int? GenderId { get; set; }
    public string Address { get; set; }
    public int RoleId { get; set; }

    /// <summary>
    /// True when the account was just created (or admin-reset) and the user
    /// hasn't yet chosen their own password. The login response carries this
    /// flag so the SPA can force a redirect to the change-password screen.
    /// </summary>
    public bool MustChangePassword { get; set; }

       
    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<UserLogin> Logins { get; set; }
    public ICollection<UserClaim> Claims { get; set; }
    public ICollection<UserToken> Tokens { get; set; }
    public ICollection<UserRefreshToken> UserRefreshTokens { get; set; }

    #region Navigation Properties

    public IList<Order.Order> Orders { get; set; }

    #endregion

}