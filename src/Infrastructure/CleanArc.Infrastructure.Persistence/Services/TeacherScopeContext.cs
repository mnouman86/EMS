using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Domain.Common;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace CleanArc.Infrastructure.Persistence.Services;

/// <summary>
/// Reads the caller's identity off <see cref="IHttpContextAccessor"/>, decides
/// whether to apply teacher scoping, and lazy-loads the class scope on first
/// use. The SP returns one row per (class, source) so the context can split
/// the result into three sets — CT-only, ST-only, and the union — and serve
/// each handler the slice it asked for via <see cref="TeacherScopeKind"/>.
/// Registered as Scoped → one instance per HTTP request → safe to cache.
/// </summary>
public class TeacherScopeContext : ITeacherScopeContext
{
    private readonly IHttpContextAccessor _http;
    private readonly IUnitOfWork _uow;
    private readonly IConfiguration _config;

    private HashSet<int> _ctScope = new();
    private HashSet<int> _stScope = new();
    private HashSet<int> _combinedScope = new();
    private bool _scopeLoaded;
    private readonly SemaphoreSlim _gate = new(1, 1);

    public TeacherScopeContext(IHttpContextAccessor http, IUnitOfWork uow, IConfiguration config)
    {
        _http = http;
        _uow = uow;
        _config = config;
    }

    public int CurrentUserId
    {
        get
        {
            var user = _http.HttpContext?.User;
            var idStr = user?.FindFirstValue(ClaimTypes.NameIdentifier) ?? user?.Identity?.Name;
            return int.TryParse(idStr, out var id) ? id : 0;
        }
    }

    public bool IsTeacherScoped
    {
        get
        {
            var user = _http.HttpContext?.User;
            if (user?.Identity?.IsAuthenticated != true) return false;
            if (user.IsInRole(Roles.Admin) || user.IsInRole(Roles.Principal)) return false;
            return user.IsInRole(Roles.Teacher);
        }
    }

    public async Task<HashSet<int>> GetClassScopeAsync(TeacherScopeKind kind = TeacherScopeKind.Combined)
    {
        await EnsureScopeLoadedAsync();
        return kind switch
        {
            TeacherScopeKind.ClassTeacher => _ctScope,
            TeacherScopeKind.Subject => _stScope,
            _ => _combinedScope
        };
    }

    public async Task<bool> OwnsStudentAsync(int studentId, TeacherScopeKind kind = TeacherScopeKind.Combined)
    {
        var scope = await GetClassScopeAsync(kind);
        if (scope.Count == 0) return false;

        using var conn = new SqlConnection(_config.GetConnectionString("DBConnection1"));
        await conn.OpenAsync();
        var classId = await conn.QueryFirstOrDefaultAsync<int?>(
            "SELECT AdmittedClassId FROM dbo.Student WHERE Id = @StudentId AND IsDeleted = 0",
            new { StudentId = studentId });
        return classId.HasValue && scope.Contains(classId.Value);
    }

    public async Task<HashSet<int>> FilterOwnedStudentsAsync(IEnumerable<int> studentIds, TeacherScopeKind kind = TeacherScopeKind.Combined)
    {
        var ids = studentIds?.Distinct().ToList() ?? new List<int>();
        if (ids.Count == 0) return new HashSet<int>();

        var scope = await GetClassScopeAsync(kind);
        if (scope.Count == 0) return new HashSet<int>();

        using var conn = new SqlConnection(_config.GetConnectionString("DBConnection1"));
        await conn.OpenAsync();
        var owned = await conn.QueryAsync<int>(
            @"SELECT s.Id FROM dbo.Student s
              WHERE s.Id IN @Ids AND s.IsDeleted = 0 AND s.AdmittedClassId IN @Classes",
            new { Ids = ids, Classes = scope.ToArray() });
        return owned.ToHashSet();
    }

    private async Task EnsureScopeLoadedAsync()
    {
        if (_scopeLoaded) return;
        await _gate.WaitAsync();
        try
        {
            if (_scopeLoaded) return;

            var userId = CurrentUserId;
            if (userId != 0)
            {
                var res = await _uow.TeacherScopeRepository.GetClassScopeAsync(userId);
                var rows = res.Data ?? Enumerable.Empty<CleanArc.Domain.Entities.Authorization.TeacherClassScopeRow>();
                foreach (var r in rows)
                {
                    if (string.Equals(r.Source, "CT", StringComparison.OrdinalIgnoreCase)) _ctScope.Add(r.SchoolClassId);
                    else _stScope.Add(r.SchoolClassId);
                }
                _combinedScope = new HashSet<int>(_ctScope);
                _combinedScope.UnionWith(_stScope);
            }
            _scopeLoaded = true;
        }
        finally { _gate.Release(); }
    }
}
