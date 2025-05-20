using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.ProcessOrder
{
    public class CreateProcessOrderDTO
    {
        public int? CultureId { get; set; }
        public int? GenericTitleId { get; set; }
        public int? ServiceTypeEnumId { get; set; }
        public string? OrderNumber { get; set; } // nvarchar(50) NULL
        public DateTime? FromDate { get; set; } // date NULL
        public DateTime? ToDate { get; set; } // date NULL
        public int? NoOfAdults { get; set; } // int NULL
        public int? NoOfChildren { get; set; } // int NULL
        public int? NoOfRooms { get; set; } // int NULL
        public int? ParticipantSize { get; set; }
        public int? OrderStatusEnumId { get; set; }
        public int? CreatedBy { get; set; } // int NULL
        public decimal? Amount { get; set; } // decimal(18, 2) NULL
        public decimal? Tax { get; set; }
        public string? FirstName { get; set; } // nvarchar(50) NULL
        public string? LastName { get; set; } // nvarchar(50) NULL
        public string? Email { get; set; } // nvarchar(50) NULL
        public string? PhoneNumber { get; set; }
        public string? CardHolderName { get; set; } // nvarchar(50) NULL
        public string? CardNumber { get; set; } // nvarchar(50) NULL
        public string? CardName { get; set; } // nvarchar(50) NULL
        public int? CardCVC { get; set; } // int NULL
        public int? ExpirationMonth { get; set; } // int NULL
        public int? ExpirationYear { get; set; } // int NULL
        public int? ZipCode { get; set; } // int NULL
        public int? CountryLookUpId { get; set; }
        public string? PaymentStatus { get; set; }

    }
}
