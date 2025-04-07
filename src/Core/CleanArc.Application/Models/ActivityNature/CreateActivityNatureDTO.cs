using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.ActivityNature
{
    public class CreateActivityNatureDTO
    {
       // public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        //public bool? IsActive { get; set; }
        //public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public int? CultureId { get; set; }

    }
}
