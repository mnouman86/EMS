using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.ProcessOrder;

public  class ProcessOrder
    
{
    public int ID { get; set; } // IDENTITY(1,1) NOT NULL
    public string? OrderNumber { get; set; } // nvarchar(50) NULL
    public string? FirstName { get; set; } // nvarchar(50) NULL
    public string? LastName { get; set; } // nvarchar(50) NULL
    public string? Email { get; set; } // nvarchar(50) NULL
    public string? PhoneNumber { get; set; }

    public string? CardHolderName { get; set; } // nvarchar(50) NULL
    public string? CardName { get; set; } // nvarchar(50) NULL
    public int? CardCVC { get; set; } // int NULL
    public int? ExpirationMonth { get; set; } // int NULL
    public int? ExpirationYear { get; set; } // int NULL
    public int? CountryID { get; set; } // int NULL
    public int? ZipCode { get; set; } // int NULL
    public int? CategoryID { get; set; } // int NULL
    public int? ServiceID { get; set; } // int NULL
    public int? SubServiceID { get; set; } // int NULL
    public decimal? Amount { get; set; } // decimal(18, 2) NULL
    public DateTime? FromDate { get; set; } // date NULL
    public DateTime? ToDate { get; set; } // date NULL
    public int? NoOfAdults { get; set; } // int NULL
    public int? NoOfChildrens { get; set; } // int NULL
    public int? NoOfRooms { get; set; } // int NULL
    public string OrderStatus { get; set; } // nvarchar(50) NULL
    public DateTime? CreditDate { get; set; } // date NULL
    public string ServiceName { get; set; } // Assuming nvarchar type
    public string ServiceCategoryName { get; set; } // Assuming nvarchar type
    public string CountryName { get; set; } // Assuming nvarchar type
    public bool? IsActive { get; set; } // bit NULL
    public bool? IsDeleted { get; set; } // bit NULL
    public int? CreatedBy { get; set; } // int NULL
    public DateTime? CreatedAt { get; set; } // datetime NULL
    public int? UpdatedBy { get; set; } // int NULL
    public DateTime? UpdatedAt { get; set; } // datetime NULL
    public int? PackageTypeID { get; set; }
    public int? CultureId { get; set; }
    public int? Code { get; set; }
    public string? Message { get; set; }

}
