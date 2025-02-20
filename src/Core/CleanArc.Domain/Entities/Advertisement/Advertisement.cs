namespace CleanArc.Domain.Entities.Advertisement;

/// <summary>
/// Represents an advertisement entity.
/// </summary>
public class Advertisement
{
    /// <summary>
    /// Gets or sets the identifier of the advertisement.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public int ID { get; set; }
    /// <summary>
    /// Gets or sets the page identifier.
    /// </summary>
    /// <value>
    /// The page identifier.
    /// </value>
    public int? PageID { get; set; }
    public string? PageName { get; set; }
    public int? PlaceID { get; set; }
    public string? PlaceName { get; set; }
    public string? ImageTitle { get; set; }
    /// <summary>
    /// Gets or sets the image path.
    /// </summary>
    /// <value>
    /// The image path.
    /// </value>
    public string? ImagePath { get; set; }
    /// <summary>
    /// Gets or sets the URL.
    /// </summary>
    /// <value>
    /// The URL.
    /// </value>
    public string? Url { get; set; }
    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    /// <value>
    /// The start date.
    /// </value>
    public DateTime? StartDate { get; set; }
    /// <summary>
    /// Gets or sets the end date.
    /// </summary>
    /// <value>
    /// The end date.
    /// </value>
    public DateTime? EndDate { get; set; }
    /// <summary>
    /// Gets or sets the is active.
    /// </summary>
    /// <value>
    /// The is active.
    /// </value>
    public bool? IsActive { get; set; }
    /// <summary>
    /// Gets or sets the is show.
    /// </summary>
    /// <value>
    /// The is show.
    /// </value>
    public bool? IsShow { get; set; }
    /// <summary>
    /// Gets or sets the is deleted.
    /// </summary>
    /// <value>
    /// The is deleted.
    /// </value>
    public bool? IsDeleted { get; set; }
    /// <summary>
    /// Gets or sets the created by.
    /// </summary>
    /// <value>
    /// The created by.
    /// </value>
    public int? CreatedBy { get; set; }
    /// <summary>
    /// Gets or sets the created at.
    /// </summary>
    /// <value>
    /// The created at.
    /// </value>
    public DateTime? CreatedAt { get; set; }
    /// <summary>
    /// Gets or sets the identifier of the user who last updated the advertisement.
    /// </summary>
    /// <value>
    /// The updated by.
    /// </value>
    public int? UpdatedBy { get; set; }
    /// <summary>
    /// Gets or sets the date and time when the advertisement was last updated.
    /// </summary>
    /// <value>
    /// The updated at.
    /// </value>
    public DateTime? UpdatedAt { get; set; }
    //public int CultureId { get; set; }


}
