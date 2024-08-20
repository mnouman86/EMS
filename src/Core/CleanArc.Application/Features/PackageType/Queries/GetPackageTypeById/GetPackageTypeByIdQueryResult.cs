using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.PackageType.Queries.GetPackageTypeById
{
    public class GetPackageTypeByIdQueryResult
    //(int Id, string Name, string Description, bool IsDeleted, bool IsActive, int CreatedBy, DateTime CreatedAt, int UpdatedBy, DateTime UpdatedAt);
    {
        public int ID { get; set; }              // Corresponds to [ID] [int] IDENTITY(1,1) NOT NULL
        public string Title { get; set; }        // Corresponds to [Title] [varchar](max) NULL
        public bool? IsActive { get; set; }      // Corresponds to [IsActive] [bit] NULL
        public bool? IsDeleted { get; set; }     // Corresponds to [IsDeleted] [bit] NULL
        public int? CreatedBy { get; set; }      // Corresponds to [CreatedBy] [int] NULL
        public DateTime? CreatedAt { get; set; } // Corresponds to [CreatedAt] [datetime] NULL
        public int? UpdatedBy { get; set; }      // Corresponds to [UpdatedBy] [int] NULL
        public DateTime? UpdatedAt { get; set; } // Corresponds to [UpdatedAt] [datetime] NULL
    
        public int? CultureId { get; set; }
        public int? Code { get; set; }
        public int? Message { get; set; }
    }
}
