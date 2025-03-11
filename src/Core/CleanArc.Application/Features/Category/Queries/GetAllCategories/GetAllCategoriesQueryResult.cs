using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Category.Queries.GetAllCategories;

public  class GetAllCategoriesQueryResult
    //(int Id, string Name, string Description, bool IsDeleted, bool IsActive, int CreatedBy, DateTime CreatedAt, int UpdatedBy, DateTime UpdatedAt);

{ public int ID { get; set; }
public string Name { get; set; }
public string Description { get; set; }
public int? ServiceCategoryID { get; set; } // Maps to A.[ServiceCategoryID]
public string? ServiceName { get; set; } // Maps to A.[ServiceCategoryID]

public bool IsDeleted { get; set; }
public bool IsActive { get; set; }
public int CreatedBy { get; set; }
public DateTime CreatedAt { get; set; }
public int UpdatedBy { get; set; }
public DateTime UpdatedAt { get; set; }}