using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.PackageType
{
    public class UpdatePackageTypeDTO
    {

        public int ID { get; set; }              // Corresponds to [ID] [int] IDENTITY(1,1) NOT NULL
        public string Title { get; set; }        // Corresponds to [Title] [varchar](max) NULL
        //public bool? IsActive { get; set; }      // Corresponds to [IsActive] [bit] NULL
        //public bool? IsDeleted { get; set; }     // Corresponds to [IsDeleted] [bit] NULL
        
        public int? UpdatedBy { get; set; }      // Corresponds to [UpdatedBy] [int] NULL
       // public DateTime? UpdatedAt { get; set; }
        public int? CultureId { get; set; }
        public int? Code { get; set; }
        public string? Message { get; set; }
    }
}
