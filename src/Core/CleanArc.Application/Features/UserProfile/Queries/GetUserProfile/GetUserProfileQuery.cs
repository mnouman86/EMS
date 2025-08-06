using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.UserProfile;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.UserProfile.Queries.GetUserProfile;

public record GetUserProfileQuery : IRequest<OperationResult<GetUserProfileQueryResponse>>
{
    [JsonIgnore]
    public int UserId { get; set; }
};
