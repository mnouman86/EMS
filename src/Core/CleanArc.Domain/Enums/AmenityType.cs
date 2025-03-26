using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Enums
{
    public enum AmenityType
    {
        [Display(Name = "Stays")]
        Stays = 1,

        [Display(Name = "Car")]
        Car = 2,

        [Display(Name = "Things To Do")]
        ThingsToDo = 3,

        [Display(Name = "Bathroom")]
        Bathroom = 4,

        [Display(Name = "Room")]
        Room = 5
    }
}
