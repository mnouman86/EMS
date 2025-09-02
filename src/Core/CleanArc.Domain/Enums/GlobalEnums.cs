using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Enums
{
    public enum PackageType
    {
        [Display(Name = "Flight, Stay")]
        FlightStays = 1,

        [Display(Name = "Flight, Car")]
        FlightCar = 2,

        [Display(Name = "Stay, Car")]
        StayCar = 3,

        [Display(Name = "Flight, Stay, Car")]
        FlightStayCar = 4,
    }
}
