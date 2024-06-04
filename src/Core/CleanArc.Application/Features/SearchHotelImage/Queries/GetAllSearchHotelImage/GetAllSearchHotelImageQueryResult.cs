using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchHotelImage.Queries.GetAllSearchHotelImage;

public record GetAllSearchHotelImageQueryResult(int HotelID,string HotelName, string ImageTitle, string ImagePath, bool IsMain);

