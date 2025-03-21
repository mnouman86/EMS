using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.HomeSlider;

public class CreateHomeSliderDTO
{
    //public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Url { get; set; }
    public string? Image { get; set; }
    //public bool? IsActive { get; set; }
    //public bool? IsDeleted { get; set; }
    public int? CreatedBy { get; set; }
    //public DateTime? CreatedAt { get; set; }
    //public int? UpdatedBy { get; set; }
    //public DateTime? UpdatedAt { get; set; }
    public int? CultureId { get; set; }
    public int? Code { get; set; }
    public string? Message { get; set; }

}
