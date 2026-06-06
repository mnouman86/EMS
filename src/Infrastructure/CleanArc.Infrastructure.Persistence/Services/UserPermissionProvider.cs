using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Security;
using CleanArc.Domain.Entities.Permission;
using Microsoft.Extensions.Caching.Memory;

namespace CleanArc.Infrastructure.Persistence.Services;

public class UserPermissionProvider : IUserPermissionProvider
{
    private readonly IUnitOfWork _uow;
    private readonly IMemoryCache _cache;
    private static readonly TimeSpan Ttl = TimeSpan.FromMinutes(10);

    public UserPermissionProvider(IUnitOfWork uow, IMemoryCache cache)
    {
        _uow = uow;
        _cache = cache;
    }

    public async Task<UserPermissionSet> GetForUserAsync(int userId)
    {
        if (_cache.TryGetValue(Key(userId), out UserPermissionSet? cached) && cached is not null)
            return cached;

        var rows = await _uow.PermissionRepository.GetUserEffectivePermissionsAsync(userId);
        var set = new UserPermissionSet(rows.Data ?? new List<UserEffectivePermissionRow>());
        _cache.Set(Key(userId), set, Ttl);
        return set;
    }

    public void Invalidate(int userId) => _cache.Remove(Key(userId));

    private static string Key(int userId) => $"perm:{userId}";
}
