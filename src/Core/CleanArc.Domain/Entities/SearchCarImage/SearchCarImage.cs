using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.SearchCarImage;

public class SearchCarImage
{
    public int Id { get; set; }
    public string CarModelName { get; set; }
    public string ImageTitle { get; set; }
    public string ImagePath { get; set; }
    public int CarID { get; set; }
    public bool IsMain { get; set; }
}

