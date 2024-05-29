using CleanArc.Domain.Entities.UserManagement;

namespace CleanArc.Application.Contracts.Persistence;
/// <summary>
/// Represents a repository interface for managing Role entities.
/// </summary>
public interface IRoleRepository : IRepository<Role>
{
    // No additional members in this interface.
    // It inherits CRUD operations from IRepository<Role>.
}
