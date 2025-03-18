using CleanArc.Application.Common;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Common;

namespace CleanArc.Application.Contracts.Persistence;
/// <summary>
/// Generic repository interface for CRUD operations on entities.
/// </summary>
/// <typeparam name="T">The type of entity managed by the repository.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Retrieves all entities based on the provided search criteria asynchronously.
    /// </summary>
    /// <param name="request">The search criteria.</param>
    /// <returns>A task representing the asynchronous operation, returning a list of entities.</returns>
    Task<ListResponseWrapper<T>> GetAllAsync(SearchRequest searchRequest);

    /// <summary>
    /// Retrieves an entity by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>A task representing the asynchronous operation, returning the entity.</returns>
    /// 
    //Task<IReadOnlyList<T>> GetAllSearchDetailAsync(String SearchText,DateTime DateFrom,DateTime DateTo,int Adult,int Children);
    Task<SingleResponseWrapper<T>> GetByIdAsync(SearchRequestById searchRequestById);

    /// <summary>
    /// Adds a new entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to be added.</param>
    /// <returns>A task representing the asynchronous operation, returning a message or identifier.</returns>
    Task<ResponseEntity> AddAsync(T entity);

    /// <summary>
    /// Updates an existing entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to be updated.</param>
    /// <returns>A task representing the asynchronous operation, returning a message or identifier.</returns>
    Task<ResponseEntity> UpdateAsync(T entity);

    /// <summary>
    /// Deletes entities based on the provided identifiers asynchronously.
    /// </summary>
    /// <param name="selectedIds">The identifiers of entities to be deleted.</param>
    /// <param name="updatedBy">The user ID who initiated the deletion.</param>
    /// <returns>A task representing the asynchronous operation, returning a message or identifier.</returns>
    Task<ResponseEntity> DeleteAsync(string selectedIds, int updatedBy,int? CultureId);
}