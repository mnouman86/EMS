using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.CheckProfileStatus
{
    public class CreateCheckProfileStatusDTO
    {
        // public int ID { get; set; }

        public int? UserID { get; set; } // Foreign Key or reference to another table
        public string? UserIntrestIDs { get; set; } // Stores interest IDs as a string (nvarchar(max))

        //public bool? IsActive { get; set; }
        //public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public int? CultureId { get; set; }
        public int? Code { get; set; }
        public string? Message { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public int? UpdatedBy { get; set; }
        //public DateTime? UpdatedAt { get; set; }

    }
}
