using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.MappingHotelImage;

public class MappingHotelImage
{
    public int? HotelID { get; set; }

    public string? ImagePaths { get; set; }
    public string? ImageTitles { get; set; }
    public string? IsMains { get; set; }
    public int? CreatedBy { get; set; }

}
