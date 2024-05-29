using CleanArc.Domain.Common;

namespace CleanArc.Domain.Entities.UserManagement;
/// <summary>
/// Represents a URL entity in the user management domain.
/// </summary>
public class URL
{
    /// <summary>
    /// Gets or sets the unique identifier for the URL.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title associated with the URL.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the path of the URL.
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// Gets or sets the description of the URL.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the user ID who created the URL.
    /// </summary>
    public int CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the user ID who last updated the URL.
    /// </summary>
    public int UpdatedBy { get; set; }
}