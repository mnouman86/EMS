using CleanArc.Application.Features.KBDetail.Queries.GetKBMinimalView;
using CleanArc.Application.Models.CoreArea;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.KBDetail.Queries.GetKBCoreAreasWiseMinimalView;

public class GetKBCoreAreasWiseMinimalViewQueryResult
{
    public int? CoreAreaLookupID { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public int? Type { get; set; }
    public string? ImagePathCore { get; set; }
    public List<GetKBMinimalViewQueryResult> KBMinimalDetails { get; set; }
    public List<KBAddress> KBAddresses { get; set; }

}
