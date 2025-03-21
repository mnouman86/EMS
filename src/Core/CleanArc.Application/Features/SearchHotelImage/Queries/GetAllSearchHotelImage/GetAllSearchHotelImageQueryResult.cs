using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchHotelImage.Queries.GetAllSearchHotelImage;

public class GetAllSearchHotelImageQueryResult
{
    public int Id { get; set; }
    public int HotelID { get; set; }
    public string HotelName { get; set; }
    public string ImageTitle { get; set; }
    public string ImagePath { get; set; }
    public bool IsMain { get; set; }
}

