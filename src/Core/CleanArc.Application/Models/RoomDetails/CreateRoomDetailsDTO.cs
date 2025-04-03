using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.RoomDetails;

public class CreateRoomDetailsDTO
{
    // public int Id { get; set; }
    public int? GenericTitleId { get; set; }
    public int? RoomTypeLookUpId { get; set; }
    public int? RoomSizeUnitLookUpId { get; set; }
    public string? RoomSize { get; set; }
    public string? Description { get; set; }
    public bool? IsSharedBathroom { get; set; }
    public bool? IsPartiallyRefundable { get; set; }
    public bool? IsFullyRefundable { get; set; }
    public decimal? Price { get; set; }
    public decimal? AdditionalMatricCharges { get; set; }
    public string? RoomNumber { get; set; }
    public bool? IsAvailable { get; set; }
    //public bool? IsActive { get; set; }
    //public bool? IsDeleted { get; set; }
    public int? CreatedBy { get; set; }
    public int? CultureId { get; set; }

}
