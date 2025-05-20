using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ProcessOrder.Queries.GetAllProcessOrder;

public class GetAllProcessOrderQueryResult
{
    public int Id { get; set; } // IDENTITY(1,1) NOT NULL
    public string? OrderNumber { get; set; } // nvarchar(50) NULL
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
    public decimal? Amount { get; set; } // decimal(18, 2) NULL
    public DateTime? FromDate { get; set; } // date NULL
    public DateTime? ToDate { get; set; } // date NULL
    public int? NoOfAdults { get; set; } // int NULL
    public int? NoOfChildren { get; set; } // int NULL
    public int? NoOfRooms { get; set; } // int NULL
    public string CountryName { get; set; } // Assuming nvarchar type
    public string OrderStatus { get; set; } // Assuming nvarchar type
    public bool? IsActive { get; set; } // bit NULL
    public bool? IsDeleted { get; set; } // bit NULL
    public int? CreatedBy { get; set; } // int NULL
    public DateTime? CreatedAt { get; set; } // datetime NULL
    public int? UpdatedBy { get; set; } // int NULL
    public DateTime? UpdatedAt { get; set; } // datetime NULL
    public int? CultureId { get; set; }
    public int? GenericTitleId { get; set; }
    public int? ServiceTypeEnumId { get; set; }
    public int? ParticipantSize { get; set; }
    public int? OrderStatusEnumId { get; set; }
    public decimal? Tax { get; set; }
    public int? CountryLookUpId { get; set; }
    public string? PaymentStatus { get; set; }
}

//    public GetAllProcessOrderQueryResult(int iD, string name, string description, bool isDeleted, bool isActive, int createdBy, DateTime createdAt, int updatedBy, DateTime updatedAt)
//    {
//        ID = iD;
//        Name = name;
//        Description = description;
//        IsDeleted = isDeleted;
//        IsActive = isActive;
//        CreatedBy = createdBy;
//        CreatedAt = createdAt;
//        UpdatedBy = updatedBy;
//        UpdatedAt = updatedAt;
//    }
//}
