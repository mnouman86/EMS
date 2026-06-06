using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Inventory;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Inventory.Command.CatalogueCommands
{
    /* INV-01: Upsert category */
    public record UpsertInventoryCategoryCommand(int? Id, string? Name, bool IsActive)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<UpsertInventoryCategoryCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<UpsertInventoryCategoryCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpsertInventoryCategoryCommand> v)
        { v.RuleFor(c => c.Name).NotEmpty().MaximumLength(100); return v; }
    }

    internal class UpsertInventoryCategoryCommandHandler : IRequestHandler<UpsertInventoryCategoryCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public UpsertInventoryCategoryCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(UpsertInventoryCategoryCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.InventoryRepository.UpsertCategoryAsync(new UpsertCategoryDTO
            { Id = r.Id, Name = r.Name, IsActive = r.IsActive, UpdatedBy = user.Id });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* INV-01: Upsert item */
    public record UpsertInventoryItemCommand(
        int? Id, string? Name, string? Code, int CategoryId, string? UnitOfMeasure,
        string? Description, decimal ReorderLevel, bool IsActive)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<UpsertInventoryItemCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<UpsertInventoryItemCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpsertInventoryItemCommand> v)
        {
            v.RuleFor(c => c.Name).NotEmpty().MaximumLength(100);
            v.RuleFor(c => c.CategoryId).GreaterThan(0);
            v.RuleFor(c => c.UnitOfMeasure).NotEmpty();
            v.RuleFor(c => c.ReorderLevel).GreaterThanOrEqualTo(0);
            return v;
        }
    }

    internal class UpsertInventoryItemCommandHandler : IRequestHandler<UpsertInventoryItemCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public UpsertInventoryItemCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(UpsertInventoryItemCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.InventoryRepository.UpsertItemAsync(new UpsertItemDTO
            {
                Id = r.Id, Name = r.Name, Code = r.Code, CategoryId = r.CategoryId,
                UnitOfMeasure = r.UnitOfMeasure, Description = r.Description,
                ReorderLevel = r.ReorderLevel, IsActive = r.IsActive, UpdatedBy = user.Id
            });
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }

    /* INV-01: Delete item (soft / hard via DeleteRequest.ForceHard) */
    public record DeleteInventoryItemCommand(DeleteRequest deleteRequest)
        : IRequest<OperationResult<ResponseEntity>>, IValidatableModel<DeleteInventoryItemCommand>
    {
        [JsonIgnore] public int UserId { get; set; }
        public IValidator<DeleteInventoryItemCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<DeleteInventoryItemCommand> v)
        { v.RuleFor(c => c.deleteRequest.SelectedIds).NotEmpty(); return v; }
    }

    internal class DeleteInventoryItemCommandHandler : IRequestHandler<DeleteInventoryItemCommand, OperationResult<ResponseEntity>>
    {
        private readonly IUnitOfWork _u; private readonly IAppUserManager _m;
        public DeleteInventoryItemCommandHandler(IUnitOfWork u, IAppUserManager m) { _u = u; _m = m; }
        public async ValueTask<OperationResult<ResponseEntity>> Handle(DeleteInventoryItemCommand r, CancellationToken ct)
        {
            var user = await _m.GetUserByIdAsync(r.UserId);
            if (user == null) return OperationResult<ResponseEntity>.FailureResult("User Not Found");
            var res = await _u.InventoryRepository.DeleteItemAsync(r.deleteRequest, user.Id);
            await _u.CommitAsync();
            return OperationResult<ResponseEntity>.SuccessResult(res);
        }
    }
}
