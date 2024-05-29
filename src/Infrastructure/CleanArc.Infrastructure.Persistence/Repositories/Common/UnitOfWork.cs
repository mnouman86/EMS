using CleanArc.Application.Contracts.Persistence;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CleanArc.Infrastructure.Persistence.Repositories.Common;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;
       
    public IUserRefreshTokenRepository UserRefreshTokenRepository { get; }
    public IOrderRepository OrderRepository { get; }
    public IURLRepository URLRepository { get; set; }

    private readonly IConfiguration configuration;
    private readonly IMapper _mapper;


    public UnitOfWork(ApplicationDbContext db, IConfiguration configuration,IMapper mapper, ILogger<URLRepository> logger, IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        UserRefreshTokenRepository = new UserRefreshTokenRepository(_db);
        OrderRepository= new OrderRepository(_db);
        URLRepository = new URLRepository(configuration,mapper,logger,httpContextAccessor);
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