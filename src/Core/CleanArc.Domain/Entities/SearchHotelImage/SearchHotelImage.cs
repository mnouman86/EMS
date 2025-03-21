using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.SearchHotelImage;

public class SearchHotelImage
{
    public int Id { get; set; }
    public string HotelName { get; set; }
    public string ImageTitle { get; set; }
    public string ImagePath { get; set; }
    public int HotelID { get; set; }
    public bool IsMain { get; set; }
}

