using CleanArc.Domain.Entities.FAQs;
using CleanArc.Domain.Entities.Language;
using CleanArc.Domain.Entities.OutDoor;
using CleanArc.Domain.Entities.RatePlan;
using CleanArc.Domain.Entities.RatePlanType;
using CleanArc.Domain.Entities.RoomView;
using CleanArc.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.RoomDetails;

public class BookingReservationRequest
{
    public APIProvider Provider { get; set; }

    public int AccommodationId { get; set; }
    public List<int> RoomIds { get; set; } = new();
    public List<int> RatePlanIds { get; set; } = new();

    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }

    public string BookerFirstName { get; set; } = string.Empty;
    public string? BookerLastName { get; set; }
    public string? BookerEmail { get; set; }
    public string? BookerTelephone { get; set; }

    public string BookerStreet { get; set; } = string.Empty;
    public string BookerCity { get; set; } = string.Empty;
    public string BookerCountry { get; set; } = string.Empty;
    public string BookerZipCode { get; set; } = string.Empty;

    public int GuestQty { get; set; }
    public List<string> GuestNames { get; set; } = new();

    public decimal TotalPrice { get; set; }
    public List<decimal> RatesByDate { get; set; } = new();

    public string PaymentMethod { get; set; } = "cash";
    public string SourceId { get; set; } = "Direct";
}

