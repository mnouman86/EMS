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
        public int? UserId { get; set; }
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
        public string? Type { get; set; }
        public string? PropertyType { get; set; }
        public string? RoomView { get; set; }
        public string? OutDoor { get; set; }
        public bool FilterByWishList { get; set; } = false;

    }

    public class CarSearchRequest : SearchRequest
    {
        public int? NoOfDays { get; set; }
        public int? Rating { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public string? Amenities { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? TransmissionType { get; set; }
        public int? MinCapacity { get; set; }
        public int? MaxCapacity { get; set; }
        public string? Manufacturer { get; set; }
        public bool FilterByWishList { get; set; }=false;

    }

    public class HotelDetailSearchRequest : SearchRequestById
    {
        public int? NoOfRooms { get; set; }
        public int? NoOfDays { get; set; }
    }
    public class CarDetailSearchRequest : SearchRequestById
    {
        public int? NoOfDays { get; set; }
    }

    public class ActivitySearchRequest : SearchRequest
    {
        public int? Rating { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public string? Amenities { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public string? SubCategory { get; set; }
        public string? Language { get; set; }
        public string? ActivityType { get; set; }
        public string? ActivityNature { get; set; }
        public string? ActivitySeason { get; set; }
        public string? ActivityDisabilityOption { get; set; }
        public string? ActivityTransportation { get; set; }
        public string? ScheduleSlot { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool FilterByWishList { get; set; } = false;

    }

}
