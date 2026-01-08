using CleanArc.Domain.Entities.FAQs;
using CleanArc.Domain.Entities.Language;
using CleanArc.Domain.Entities.OutDoor;
using CleanArc.Domain.Entities.RatePlan;
using CleanArc.Domain.Entities.RatePlanType;
using CleanArc.Domain.Entities.RoomView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Entities.RoomDetails;

public class BookingReservationResult
{
    public bool IsSuccess { get; set; }
    public string? BookingId { get; set; }
    public string? PinCode { get; set; }
    public string? Message { get; set; }
}
