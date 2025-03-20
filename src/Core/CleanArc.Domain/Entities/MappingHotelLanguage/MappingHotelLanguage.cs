using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.MappingHotelLanguage;

public class MappingHotelLanguage
{
    public string HotelIds { get; set; }
    public string LanguageIds { get; set; }
    public int CreatedBy { get; set; }
    public int? CultureId { get; set; }
}
