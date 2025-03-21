using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.SubService
{
    public class CreateSubServiceDTO
    {
       // public int Id { get; set; }
        public string? Name { get; set; }
        public int? ServiceCategoryId { get; set; }

        public string? Description { get; set; }
        //public bool? IsActive { get; set; }
        //public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public int? CultureId { get; set; }

    }
}
