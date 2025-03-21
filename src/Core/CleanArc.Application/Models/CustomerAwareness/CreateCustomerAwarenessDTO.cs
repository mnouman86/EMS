using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.CustomerAwareness;

public class CreateCustomerAwarenessDTO
{
    //public int Id { get; set; }
    public int? ID { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? KeyNumber { get; set; }
    public string? Link { get; set; }
    public string? Status { get; set; }
    public int? StatusApprovedBy { get; set; }
    public string? ApprovedDate { get; set; }
    //public string? ApprovedDate { get; set; }
    //public bool? IsActive { get; set; }
    //public bool? IsDeleted { get; set; }
    public int? CreatedBy { get; set; }
    //public DateTime? CreatedAt { get; set; }
    //public int? UpdatedBy { get; set; }
    //public DateTime? UpdatedAt { get; set; }

    public int? CultureId { get; set; }


}
