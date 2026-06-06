using System.Threading.Tasks;
using CleanArc.Application.Security;

namespace CleanArc.Application.Contracts.Identity;

/// <summary>Loads (and caches) a user's effective permission set for authorization checks.</summary>
public interface IUserPermissionProvider
{
    Task<UserPermissionSet> GetForUserAsync(int userId);
    void Invalidate(int userId);
}
