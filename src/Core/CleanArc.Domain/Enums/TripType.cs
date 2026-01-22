using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Enums
{
    public enum TripType
    {
        [Display(Name = "Within City")]
        WithInCity = 1,

        [Display(Name = "Out of City")]
        OutOfCity = 2,
    }
}
