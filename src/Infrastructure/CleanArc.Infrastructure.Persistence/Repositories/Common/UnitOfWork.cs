using CleanArc.Application.Contracts.Persistence;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging; 
using CleanArc.Domain.Common;
using CleanArc.Infrastructure.Persistence.Services;

namespace CleanArc.Infrastructure.Persistence.Repositories.Common;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;

    public IUserRefreshTokenRepository UserRefreshTokenRepository { get; }
    public ILanguageRepository LanguageRepository { get; set; }
    public IStartupDataRepository StartupDataRepository { get; set; }
    public ISchoolClassRepository SchoolClassRepository { get; set; }
    public ISubjectRepository SubjectRepository { get; set; }
    public IEmployeeRepository EmployeeRepository { get; set; }
    public IStudentRepository StudentRepository { get; set; }
    public IResultRepository ResultRepository { get; set; }
    public IAcademicYearRepository AcademicYearRepository { get; set; }
    public IFeeRepository FeeRepository { get; set; }
    public IInventoryRepository InventoryRepository { get; set; }
    public IExpenseRepository ExpenseRepository { get; set; }
    public IFinanceRepository FinanceRepository { get; set; }
    public IPermissionRepository PermissionRepository { get; set; }
    public IParentRepository ParentRepository { get; set; }
    public IAttendanceRepository AttendanceRepository { get; set; }
    public ITeacherScopeRepository TeacherScopeRepository { get; set; }
    public ICalendarRepository CalendarRepository { get; set; }
    public IComplaintRepository ComplaintRepository { get; set; }
    public IStaffAttendanceRepository StaffAttendanceRepository { get; set; }
    public ILeaveRepository LeaveRepository { get; set; }

    private readonly IConfiguration configuration;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;


    public UnitOfWork(ApplicationDbContext db, IEmailService emailService, IConfiguration configuration,IMapper mapper,
        ILogger<LanguageRepository> _loggerLanguage,
        ILogger<StartupDataRepository> _loggerStartupData,
        ILogger<SchoolClassRepository> _loggerSchoolClass,
        ILogger<SubjectRepository> _loggerSubject,
        ILogger<EmployeeRepository> _loggerEmployee,
        ILogger<StudentRepository> _loggerStudent,
        ILogger<ResultRepository> _loggerResult,
        ILogger<AcademicYearRepository> _loggerAcademicYear,
        ILogger<FeeRepository> _loggerFee,
        ILogger<InventoryRepository> _loggerInventory,
        ILogger<ExpenseRepository> _loggerExpense,
        ILogger<FinanceRepository> _loggerFinance,
        ILogger<PermissionRepository> _loggerPermission,
        ILogger<ParentRepository> _loggerParent,
        ILogger<AttendanceRepository> _loggerAttendance,
        ILogger<TeacherScopeRepository> _loggerTeacherScope,
        ILogger<CalendarRepository> _loggerCalendar,
        ILogger<ComplaintRepository> _loggerComplaint,
        ILogger<StaffAttendanceRepository> _loggerStaffAttendance,
        ILogger<LeaveRepository> _loggerLeave,

        IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        UserRefreshTokenRepository = new UserRefreshTokenRepository(_db);
        LanguageRepository = new LanguageRepository(configuration, mapper, _loggerLanguage, httpContextAccessor);
        StartupDataRepository = new StartupDataRepository(configuration);
        SchoolClassRepository = new SchoolClassRepository(configuration, mapper, _loggerSchoolClass, httpContextAccessor);
        SubjectRepository = new SubjectRepository(configuration, mapper, _loggerSubject, httpContextAccessor);
        EmployeeRepository = new EmployeeRepository(configuration, mapper, _loggerEmployee, httpContextAccessor);
        StudentRepository = new StudentRepository(configuration, mapper, _loggerStudent, httpContextAccessor);
        ResultRepository = new ResultRepository(configuration, _loggerResult, httpContextAccessor);
        AcademicYearRepository = new AcademicYearRepository(configuration, _loggerAcademicYear, httpContextAccessor);
        FeeRepository = new FeeRepository(configuration, _loggerFee, httpContextAccessor);
        InventoryRepository = new InventoryRepository(configuration, _loggerInventory, httpContextAccessor);
        ExpenseRepository = new ExpenseRepository(configuration, _loggerExpense, httpContextAccessor);
        FinanceRepository = new FinanceRepository(configuration, _loggerFinance, httpContextAccessor);
        PermissionRepository = new PermissionRepository(configuration, _loggerPermission, httpContextAccessor);
        ParentRepository = new ParentRepository(configuration, _loggerParent, httpContextAccessor);
        AttendanceRepository = new AttendanceRepository(configuration, _loggerAttendance, httpContextAccessor);
        TeacherScopeRepository = new TeacherScopeRepository(configuration, _loggerTeacherScope, httpContextAccessor);
        CalendarRepository = new CalendarRepository(configuration, _loggerCalendar, httpContextAccessor);
        ComplaintRepository = new ComplaintRepository(configuration, _loggerComplaint, httpContextAccessor);
        StaffAttendanceRepository = new StaffAttendanceRepository(configuration, _loggerStaffAttendance, httpContextAccessor);
        LeaveRepository = new LeaveRepository(configuration, _loggerLeave, httpContextAccessor);
        this.configuration = configuration;
    }

    public  Task CommitAsync()
    {
        return _db.SaveChangesAsync();
    }

    public ValueTask RollBackAsync()
    {
        return _db.DisposeAsync();
    }
}