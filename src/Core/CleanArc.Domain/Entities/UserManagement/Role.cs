using CleanArc.Domain.Common;

namespace CleanArc.Domain.Entities.UserManagement;
/// <summary>
/// Represents a role entity in the user management domain.
/// </summary>
public class Role
{
    /// <summary>
    /// Gets or sets the unique identifier for the role.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the role.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the associated URL.
    /// </summary>
    public int IndexURLID { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the role is marked as deleted.
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Gets or sets the user ID who created the role.
    /// </summary>
    public int CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the user ID who last updated the role.
    /// </summary>
    public int UpdatedBy { get; set; }
}