using System;
using System.Collections.Generic;
using CleanArc.Domain.Entities.Permission;

namespace CleanArc.Application.Security;

/// <summary>Immutable snapshot of one user's effective feature permissions (cached for fast lookups).</summary>
public sealed class UserPermissionSet
{
    private readonly Dictionary<string, UserEffectivePermissionRow> _map;

    public UserPermissionSet(IEnumerable<UserEffectivePermissionRow> rows)
    {
        _map = new Dictionary<string, UserEffectivePermissionRow>(StringComparer.OrdinalIgnoreCase);
        foreach (var r in rows)
            if (!string.IsNullOrEmpty(r.FeatureCode))
                _map[r.FeatureCode!] = r;
    }

    public bool Has(string feature, PermissionAction action)
    {
        if (!_map.TryGetValue(feature, out var p)) return false;
        return action switch
        {
            PermissionAction.Read    => p.CanRead,
            PermissionAction.Create  => p.CanCreate,
            PermissionAction.Update  => p.CanUpdate,
            PermissionAction.Delete  => p.CanDelete,
            PermissionAction.Export  => p.CanExport,
            PermissionAction.Approve => p.CanApprove,
            PermissionAction.Print   => p.CanPrint,
            _ => false
        };
    }
}
