using CleanArc.Domain.Entities.UserManagement;

namespace CleanArc.Application.Contracts.Persistence;
/// <summary>
/// Represents a repository interface for managing Page Feature entities.
/// </summary>
public interface IPageFeatureRepository : IRepository<PageFeature>
{
    // No additional members in this interface.
    // It inherits CRUD operations from IRepository<PageFeature>.
}
