using CleanArc.Application.Profiles;
using CleanArc.Domain.Entities.User;

namespace CleanArc.Application.Features.UserProfile.Queries.GetUserProfile;

public record GetUserProfileQueryResponse
{
    public string Name { get; set; }
    public string FamilyName { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public int? GenderId { get; set; }
    public int? NationalityId { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Address { get; set; }
}