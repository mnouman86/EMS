using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchCarImage.Queries.GetAllSearchCarImage;

public class GetAllSearchCarImageQueryResult
{
    public int ID { get; set; }
    public bool IsMain { get; set; }
    public int CarID { get; set; }
    public string ImagePath { get; set; }
    public string ImageTitle { get; set; }
    public string CarModelName { get; set; }
}

