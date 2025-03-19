using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.ServiceCategory;

public class UpdateServiceCategoryDTO
{
     public int ID { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? ServiceID { get; set; }
    public string? Icon { get; set; }
    public int? CultureId { get; set; }
    public int? UpdatedBy { get; set; }
    // public DateTime? UpdatedAt { get; set; }
}
