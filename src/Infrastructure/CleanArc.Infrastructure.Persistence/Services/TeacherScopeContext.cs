using CleanArc.Application.Contracts.Identity;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Domain.Common;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Security.Claims;

namespace CleanArc.Infrastructure.Persistence.Services;

/// <summary>
/// Reads the caller's identity off <see cref="IHttpContextAccessor"/>, decides
/// whether to apply teacher scoping, and lazy-loads the class scope on first use.
/// Registered as Scoped → one instance per HTTP request → safe to cache results
/// for the request's lifetime.
/// </summary>
public class TeacherScopeContext : ITeacherScopeContext
{
    private readonly IHttpContextAccessor _http;
    private readonly IUnitOfWork _uow;
    private readonly IConfiguration _config;
    private HashSet<int>? _scope;
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

    /// <summary>
    /// True only when the caller is a teacher AND not admin/principal. Admins and
    /// principals always see everything; this property short-circuits handlers
    /// before they bother computing scope.
    /// </summary>
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

    public async Task<HashSet<int>> GetClassScopeAsync()
    {
        if (_scopeLoaded) return _scope!;
        await _gate.WaitAsync();
        try
        {
            if (_scopeLoaded) return _scope!;
            var userId = CurrentUserId;
            if (userId == 0)
            {
                _scope = new HashSet<int>();
            }
            else
            {
                var res = await _uow.TeacherScopeRepository.GetClassScopeAsync(userId);
                _scope = (res.Data ?? Enumerable.Empty<CleanArc.Domain.Entities.Authorization.TeacherClassScopeRow>())
                    .Select(r => r.SchoolClassId).ToHashSet();
            }
            _scopeLoaded = true;
            return _scope!;
        }
        finally { _gate.Release(); }
    }

    /// <summary>
    /// One small ad-hoc lookup of the student's current AdmittedClassId. Kept
    /// here (vs. going through StudentRepository) so we don't pull in heavy
    /// student-DTO mapping just for an int. Returns false for soft-deleted
    /// students, missing IDs, or class IDs outside the scope.
    /// </summary>
    public async Task<bool> OwnsStudentAsync(int studentId)
    {
        var scope = await GetClassScopeAsync();
        if (scope.Count == 0) return false;

        using var conn = new SqlConnection(_config.GetConnectionString("DBConnection1"));
        await conn.OpenAsync();
        var classId = await conn.QueryFirstOrDefaultAsync<int?>(
            "SELECT AdmittedClassId FROM dbo.Student WHERE Id = @StudentId AND IsDeleted = 0",
            new { StudentId = studentId });
        return classId.HasValue && scope.Contains(classId.Value);
    }

    public async Task<HashSet<int>> FilterOwnedStudentsAsync(IEnumerable<int> studentIds)
    {
        var ids = studentIds?.Distinct().ToList() ?? new List<int>();
        if (ids.Count == 0) return new HashSet<int>();

        var scope = await GetClassScopeAsync();
        if (scope.Count == 0) return new HashSet<int>();

        using var conn = new SqlConnection(_config.GetConnectionString("DBConnection1"));
        await conn.OpenAsync();
        // Single roundtrip — Dapper expands the two IN-list params for us.
        var owned = await conn.QueryAsync<int>(
            @"SELECT s.Id FROM dbo.Student s
              WHERE s.Id IN @Ids AND s.IsDeleted = 0 AND s.AdmittedClassId IN @Classes",
            new { Ids = ids, Classes = scope.ToArray() });
        return owned.ToHashSet();
    }
}
