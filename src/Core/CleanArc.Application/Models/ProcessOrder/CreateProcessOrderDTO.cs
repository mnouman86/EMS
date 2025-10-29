using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.ProcessOrder
{
    public class CreateProcessOrderDTO
    {
        

        //public int Id { get; set; } // IDENTITY(1,1) NOT NULL
        //public string? OrderNumber { get; set; } // nvarchar(50) NULL
        public string? FirstName { get; set; } // nvarchar(50) NULL
        public string? LastName { get; set; } // nvarchar(50) NULL
        public string? Email { get; set; } // nvarchar(50) NULL
        public string? PhoneNumber { get; set; }
        //1. public int? PackageTypeEnumID { get; set; }

        public string? CardHolderName { get; set; } // nvarchar(50) NULL
        public string? CardNumber { get; set; } // nvarchar(50) NULL
        public string? CardName { get; set; } // nvarchar(50) NULL
        public int? CardCVC { get; set; } // int NULL
        public int? ExpirationMonth { get; set; } // int NULL
        public int? ExpirationYear { get; set; } // int NULL
        public int? ZipCode { get; set; } // int NULL
        public DateTime? FromDate { get; set; } // date NULL
        public DateTime? ToDate { get; set; } // date NULL
        public int? NoOfAdults { get; set; } // int NULL
        public int? NoOfChildren { get; set; } // int NULL
        public int? NoOfRooms { get; set; } // int NULL
        public int? ParticipantSize { get; set; }

        //2. public string CountryName { get; set; } // Assuming nvarchar type
        //3. public string OrderStatus { get; set; } // Assuming nvarchar type
        public int? CreatedBy { get; set; } // int NULL
        public int? OrderStatusEnumId { get; set; }
        public int? CountryLookUpId { get; set; }
        public int? CityLookUpId { get; set; }
        public int? PaymentStatusEnumId { get; set; }


        public int? CultureId { get; set; }
        //public CreateProcessOrdersDTO Stays { get; set; }
        //public CreateProcessOrdersDTO Flights { get; set; }
        //public CreateProcessOrdersDTO CarRental { get; set; }
        //public CreateProcessOrdersDTO Activities { get; set; }
        //public string? City { get; set; }

        public decimal? Amount { get; set; } = 0; // decimal(18, 2) NULL
        public decimal? DiscountAmount { get; set; } = 0; // decimal(18, 2) NULL
        public int? GenericTitleId { get; set; } = -1;
        public int? SubTitleID { get; set; } = -1;
        public int? ServiceTypeEnumId { get; set; } = -1;
        public int? PackageDetailID { get; set; } = -1;
        public decimal? Tax { get; set; } = 0;
        //public string? Title { get; set; } = null;
        //public string? SubTitle { get; set; } = null;

    }

   
}
