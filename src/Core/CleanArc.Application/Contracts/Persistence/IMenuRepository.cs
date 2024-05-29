using CleanArc.Domain.Entities.UserManagement;

namespace CleanArc.Application.Contracts.Persistence;
/// <summary>
/// Represents a repository interface for managing Menu entities.
/// </summary>
public interface IMenuRepository : IRepository<Menu>
{
    // No additional members in this interface.
    // It inherits CRUD operations from IRepository<Menu>.
}
