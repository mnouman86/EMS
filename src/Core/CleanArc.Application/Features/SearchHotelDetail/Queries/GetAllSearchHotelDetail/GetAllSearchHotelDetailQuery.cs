using CleanArc.Application.Features.SearchHotel.Commands.CreateSearchHotelCommand;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchHotelDetail.Queries.GetAllSearchHotelDetail;

public record GetAllSearchHotelDetailQuery(CustomizedSearchRequest SearchRequest)
    : IRequest<OperationResult<GetAllSearchHotelDetailQueryResult>>,
      IValidatableModel<GetAllSearchHotelDetailQuery>
{
    public IValidator<GetAllSearchHotelDetailQuery>
    ValidateApplicationModel(
        ApplicationBaseValidationModelProvider<GetAllSearchHotelDetailQuery> validator)
    {
        // EndDate > StartDate
        validator.RuleFor(x => x.SearchRequest)
            .Must(x => !x.StartDate.HasValue
                    || !x.EndDate.HasValue
                    || x.EndDate > x.StartDate)
            .WithMessage("End date must be greater than Start date.");

        // StartDate >= Today
        validator.RuleFor(x => x.SearchRequest.StartDate)
            .GreaterThanOrEqualTo(DateTime.Today)
            .When(x => x.SearchRequest.StartDate.HasValue)
            .WithMessage("Start date cannot be earlier than today.");

        // EndDate >= Today
        validator.RuleFor(x => x.SearchRequest.EndDate)
            .GreaterThanOrEqualTo(DateTime.Today)
            .When(x => x.SearchRequest.EndDate.HasValue)
            .WithMessage("End date cannot be earlier than today.");

        // NoOfAdults > 0
        validator.RuleFor(x => x.SearchRequest.NoOfAdults)
            .GreaterThan(0)
            .When(x => x.SearchRequest.NoOfAdults.HasValue)
            .WithMessage("No of adults must be greater than zero.");

        // NoOfRooms > 0
        validator.RuleFor(x => x.SearchRequest.NoOfRooms)
            .GreaterThan(0)
            .When(x => x.SearchRequest.NoOfRooms.HasValue)
            .WithMessage("No of rooms must be greater than zero.");

        return validator;
    }
}


