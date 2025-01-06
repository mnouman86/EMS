using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.KBDetail;

public  class KBMinimalDetail
{
    public int? KBDetailID { get; set; }
    public int? GenericTitleID { get; set; }
    public int? KBDescriptionID { get; set; }
    public string? Title { get; set; }
    public int? SectionID { get; set; }
    public string? Section { get; set; }
    public string? Content { get; set; }
    public int? ServiceID { get; set; }
    public string? ServiceCategory { get; set; }
    public int? CoreAreaLookupID { get; set; }
    public string? CoreArea { get; set; }
    public string? RelatedAreasLookupIDs { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string? ImageTitle { get; set; }
    public string? ImagePath { get; set; }


}