using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.ContactForm.Queries.GetAllContactForm;

public record GetAllContactFormQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllContactFormQueryResult>>>;

