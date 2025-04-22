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
        public List<FilterParameter> FilterArray { get; set; }
        public List<SortingParameter> SortingArray { get; set; }
    }
    public class DeleteRequest
    {
        public string SelectedIds { get; set; }
        public int? CultureId { get; set; }
        public bool isDeleted { get; set; }
    }
    public class SearchRequestById
    {
        public int Id { get; set; }
        public int? CultureId { get; set; }
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

    public class CustomizedSearchRequest : SearchRequest
    {
        public int? NoOfRooms { get; set; }
        public int? NoOfDays { get; set; }
        public int? Rating { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public string? Amenities { get; set; }
        public string? Name { get; set; }
    
    }

    public class HotelDetailSearchRequest : SearchRequestById
    {
        public int? NoOfRooms { get; set; }
        public int? NoOfDays { get; set; }
    }
}
