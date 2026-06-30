namespace CleanArc.Application.Contracts.Identity
{
    /// <summary>
    /// Kind of scope a handler asks for:
    ///   ClassTeacher → only classes where the teacher is in <c>SchoolClass.ClassTeacherId</c>.
    ///                  Used by Attendance + Fee per spec (own-class responsibility).
    ///   Subject      → only classes the teacher teaches a subject in (<c>TeacherAssignment</c>).
    ///                  Used by Results per spec (assigned-subject classes).
    ///   Combined     → CT ∪ ST. Used by generic per-student ownership reads where any
    ///                  relationship justifies access (default).
    /// </summary>
    public enum TeacherScopeKind { ClassTeacher, Subject, Combined }

    /// <summary>
    /// Per-request view of "what data the calling teacher is allowed to see".
    /// Admins and principals never look teacher-scoped (their <c>IsTeacherScoped</c>
    /// is always false); for teachers the context resolves three sets — CT-only,
    /// ST-only, and the union — and dispatches based on the <see cref="TeacherScopeKind"/>
    /// each handler asks for.
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
        /// Class IDs the teacher is responsible for under <paramref name="kind"/>.
        /// Returns an empty set when the user is not a teacher (callers normally
        /// guard with <see cref="IsTeacherScoped"/> first). A teacher with no
        /// matching relationships also returns an empty set — they see nothing,
        /// which is the safe default.
        /// </summary>
        Task<HashSet<int>> GetClassScopeAsync(TeacherScopeKind kind = TeacherScopeKind.Combined);

        /// <summary>
        /// True if the student's current AdmittedClassId is in the teacher's
        /// scope of the requested kind.
        /// </summary>
        Task<bool> OwnsStudentAsync(int studentId, TeacherScopeKind kind = TeacherScopeKind.Combined);

        /// <summary>
        /// Batched ownership filter — single DB roundtrip. Returns the subset of
        /// the supplied student IDs whose AdmittedClassId is in the caller's
        /// scope of the requested kind. Use this to filter list responses that
        /// carry StudentId but no ClassId (e.g. the ageing report).
        /// </summary>
        Task<HashSet<int>> FilterOwnedStudentsAsync(IEnumerable<int> studentIds, TeacherScopeKind kind = TeacherScopeKind.Combined);
    }
}
