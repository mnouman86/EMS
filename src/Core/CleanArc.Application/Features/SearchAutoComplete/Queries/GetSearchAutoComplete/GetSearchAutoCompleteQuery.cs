using CleanArc.Application.Features.SearchCarImage.Queries.GetByIdSearchCarImage;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using CleanArc.Application.Models.UserProfile;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.SearchAutoComplete.Queries.GetSearchAutoComplete;

public record GetSearchAutoCompleteQuery(SearchRequestById searchRequestById) : IRequest<OperationResult<List<GetSearchAutoCompleteQueryResult>>>;

