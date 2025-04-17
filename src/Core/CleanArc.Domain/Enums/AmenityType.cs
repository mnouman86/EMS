using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Enums
{
    public enum ServiceType
    {
        [Display(Name = "Stays")]
        Stays = 1,

        [Display(Name = "Car")]
        Car = 2,

        [Display(Name = "Things To Do")]
        ThingsToDo = 5,

        [Display(Name = "Bathroom")]
        Bathroom = 4,

        [Display(Name = "Room")]
        Room = 3,

        [Display(Name = "Flights")]
        Flights = 6,

        [Display(Name = "Advertisement")]
        Advertisement = 7,
    }
}
