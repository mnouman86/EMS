using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.KBDescription.Queries.GetAllKBDescription;

public class GetAllKBDescriptionQueryResult
{
    public int Id { get; set; }
    /// <summary>
    /// public int? MediaID { get; set; }
    /// </summary>
    public int? GenericTitleID { get; set; }
    //public int? SectionID { get; set; }
    public string? SubHeading { get; set; }
    public string? Content { get; set; }
    public string? KBContentType { get; set; }
    //public string? MediaType { get; set; }
    //public string? ImagePath { get; set; }
    //public string? ImageTitle { get; set; }
    //public bool? IsMain { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsDeleted { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? CultureId { get; set; }
    public int? Code { get; set; }
    public string? Message { get; set; }
}

//    public GetAllKBDescriptionQueryResult(int iD, string name, string description, bool isDeleted, bool isActive, int createdBy, DateTime createdAt, int updatedBy, DateTime updatedAt)
//    {
//        ID = iD;
//        Name = name;
//        Description = description;
//        IsDeleted = isDeleted;
//        IsActive = isActive;
//        CreatedBy = createdBy;
//        CreatedAt = createdAt;
//        UpdatedBy = updatedBy;
//        UpdatedAt = updatedAt;
//    }
//}
