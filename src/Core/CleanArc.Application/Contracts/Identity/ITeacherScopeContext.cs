namespace CleanArc.Application.Contracts.Identity
{
    /// <summary>
    /// Per-request view of "what data the calling teacher is allowed to see".
    /// Admins and principals never look teacher-scoped (their <c>IsTeacherScoped</c>
    /// is always false); for everyone else (including pure teachers) the context
    /// resolves the set of <see cref="GetClassScopeAsync"/> they're responsible
    /// for — class teacher + every TeacherAssignment row they appear in.
    ///
    /// Handlers use this for two things:
    ///   1. List filtering — drop rows whose class isn't in scope.
    ///   2. Per-student ownership — <see cref="OwnsStudentAsync"/>.
    ///
    /// One instance per HTTP request; results are cached for the request's
    /// lifetime so repeated checks don't re-hit the DB.
    /// </summary>
    public interface ITeacherScopeContext
    {
        /// <summary>True ⇒ teacher role only (NOT admin, NOT principal).</summary>
        bool IsTeacherScoped { get; }

        /// <summary>Caller's UserId (0 if unauthenticated / unparseable).</summary>
        int CurrentUserId { get; }

        /// <summary>
        /// Class IDs the teacher is responsible for. Returns an empty set when
        /// the user is not a teacher (callers normally guard with <see cref="IsTeacherScoped"/>
        /// first). A teacher with no assignments also returns an empty set — they
        /// see nothing, which is the safe default.
        /// </summary>
        Task<HashSet<int>> GetClassScopeAsync();

        /// <summary>
        /// True if the student's current AdmittedClassId is in the teacher's class scope.
        /// Used by per-student endpoints (ledger, receipt, result card, attendance history).
        /// </summary>
        Task<bool> OwnsStudentAsync(int studentId);

        /// <summary>
        /// Batched ownership filter — single DB roundtrip. Returns the subset of
        /// the supplied student IDs whose AdmittedClassId is in the caller's class
        /// scope. Use this to filter list responses that carry StudentId but no ClassId
        /// (e.g. the ageing report).
        /// </summary>
        Task<HashSet<int>> FilterOwnedStudentsAsync(IEnumerable<int> studentIds);
    }
}
