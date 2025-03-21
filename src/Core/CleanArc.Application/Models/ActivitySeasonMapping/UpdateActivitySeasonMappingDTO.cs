using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.ActivitySeasonMapping
{
    public class UpdateActivitySeasonMappingDTO
    {

        public int Id { get; set; }
        public String? SeasonIDs { get; set; }
        public int? ActivityID { get; set; }
        //public bool? IsActive { get; set; }
        //public bool? IsDeleted { get; set; }
        // public int? CreatedBy { get; set; }
        //public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CultureId { get; set; }
        public int? Code { get; set; }
        public string? Message { get; set; }
        //public DateTime? UpdatedAt { get; set; }
    }
}
