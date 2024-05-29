using CleanArc.Domain.Common;

namespace CleanArc.Domain.Entities.UserManagement;
/// <summary>
/// Represents a menu entity in the user management domain.
/// </summary>
public class Menu
{
    /// <summary>
    /// Gets or sets the unique identifier for the menu.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the menu.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the associated URL.
    /// </summary>
    public int URLID { get; set; }

    /// <summary>
    /// Gets or sets the icon associated with the menu.
    /// </summary>
    public string MenuIcon { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the parent menu.
    /// </summary>
    public int Parent_MenuId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the menu is a parent menu.
    /// </summary>
    public bool IsParent { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the menu is marked as deleted.
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Gets or sets the user ID who created the menu.
    /// </summary>
    public int CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the user ID who last updated the menu.
    /// </summary>
    public int UpdatedBy { get; set; }
}