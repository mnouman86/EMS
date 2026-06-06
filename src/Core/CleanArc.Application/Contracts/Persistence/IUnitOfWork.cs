namespace CleanArc.Application.Contracts.Persistence;

public interface IUnitOfWork
{
    public IUserRefreshTokenRepository UserRefreshTokenRepository { get; }
    public ILanguageRepository LanguageRepository { get; }
    public IStartupDataRepository StartupDataRepository { get; }
    public ISchoolClassRepository SchoolClassRepository { get; }
    public ISubjectRepository SubjectRepository { get; }
    public IEmployeeRepository EmployeeRepository { get; }
    public IStudentRepository StudentRepository { get; }
    public IResultRepository ResultRepository { get; }
    public IAcademicYearRepository AcademicYearRepository { get; }
    public IFeeRepository FeeRepository { get; }
    public IInventoryRepository InventoryRepository { get; }
    public IExpenseRepository ExpenseRepository { get; }
    public IFinanceRepository FinanceRepository { get; }
    public IPermissionRepository PermissionRepository { get; }
    public IParentRepository ParentRepository { get; }
    public IAttendanceRepository AttendanceRepository { get; }
    public ITeacherScopeRepository TeacherScopeRepository { get; }

    Task CommitAsync();
    ValueTask RollBackAsync();
}