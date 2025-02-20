using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Models.Request
{
    public class SearchRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? CultureId { get; set; }
        public int? Code { get; set; }
        public string? Message { get; set; }
        public int? Id { get; set; }
        public List<FilterParameter> FilterArray { get; set; }
        public List<SortingParameter> SortingArray { get; set; }
    }
    public class FilterParameter
    {
        public string ParameterName { get; set; }
        public string ParameterValue { get; set; }
    }

    public class SortingParameter
    {
        public string SortingColumnName { get; set; }
        public string SortingColumnDirection { get; set; }
    }
}
