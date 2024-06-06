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
    public IAgeTypeRepository AgeTypeRepository { get; set; }
    public IHotelRepository HotelRepository { get; set; }
    public IAmenityRepository AmenityRepository { get; set; }
    public IRoomTypeRepository RoomTypeRepository { get; set; }
    public IRoomDetailsRepository RoomDetailsRepository { get; set; }
    public ICategoryRepository CategoryRepository { get; set; }
    public ISearchHotelRepository SearchHotelRepository { get; set; }
    public ISearchHotelImageRepository SearchHotelImageRepository { get; set; }
    public ISearchHotelAmenitiesRepository SearchHotelAmenitiesRepository { get; set; }
    public ISearchCountryCitiesRepository SearchCountryCitiesRepository { get; set; }
    public ISearchHotelRoomDetailRepository SearchHotelRoomDetailRepository { get; set; }
    public ISearchRoomAmenitiesRepository SearchRoomAmenitiesRepository { get; set; }
    public IRoomImagesRepository RoomImagesRepository { get; set; }
    private readonly IConfiguration configuration;
    private readonly IMapper _mapper;


    public UnitOfWork(ApplicationDbContext db, IConfiguration configuration,IMapper mapper, 
        ILogger<URLRepository> logger,
        ILogger<AgeTypeRepository> _logger, 
        ILogger<HotelRepository> _loggerHotel,
        ILogger<AmenityRepository> _loggerAmenity,
        ILogger<RoomTypeRepository> _loggerRoomType,
        ILogger<RoomDetailsRepository> _loggerRoomDetails,
        ILogger<CategoryRepository> _loggerCategory,
        ILogger<SearchHotelRepository> _loggerSearchHotel,
        ILogger<SearchHotelImageRepository> _loggerSearchImage,
        ILogger<SearchHotelAmenitiesRepository> _loggerSearchHotelAmenities,
        ILogger<SearchCountryCitiesRepository> _loggerSearchCountryCities,
        ILogger<SearchHotelRoomDetailRepository> _loggerSearchHotelRoomDetail,
        ILogger<SearchRoomAmenitiesRepository> _loggerSearchRoomAmenities,
        ILogger<RoomImagesRepository> _loggerRoomImages,


        IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        UserRefreshTokenRepository = new UserRefreshTokenRepository(_db);
        OrderRepository= new OrderRepository(_db);
        URLRepository = new URLRepository(configuration,mapper,logger,httpContextAccessor);
        AgeTypeRepository = new AgeTypeRepository(configuration, mapper, _logger, httpContextAccessor);
        HotelRepository=new HotelRepository(configuration, mapper, _loggerHotel, httpContextAccessor);
        AmenityRepository=new AmenityRepository(configuration, mapper, _loggerAmenity, httpContextAccessor);
        RoomTypeRepository=new RoomTypeRepository(configuration, mapper, _loggerRoomType, httpContextAccessor);
        RoomDetailsRepository=new RoomDetailsRepository(configuration, mapper, _loggerRoomDetails, httpContextAccessor);
        CategoryRepository=new CategoryRepository(configuration, mapper, _loggerCategory, httpContextAccessor);
        SearchHotelRepository=new SearchHotelRepository(configuration, mapper, _loggerSearchHotel, httpContextAccessor);
        SearchHotelImageRepository = new SearchHotelImageRepository(configuration, mapper, _loggerSearchImage, httpContextAccessor);
        SearchHotelAmenitiesRepository=new SearchHotelAmenitiesRepository(configuration, mapper, _loggerSearchHotelAmenities, httpContextAccessor);
        SearchCountryCitiesRepository=new SearchCountryCitiesRepository(configuration, mapper, _loggerSearchCountryCities, httpContextAccessor);
        SearchHotelRoomDetailRepository=new SearchHotelRoomDetailRepository(configuration, mapper, _loggerSearchHotelRoomDetail, httpContextAccessor);
        SearchRoomAmenitiesRepository=new SearchRoomAmenitiesRepository(configuration, mapper, _loggerSearchRoomAmenities, httpContextAccessor);
        RoomImagesRepository=new RoomImagesRepository(configuration, mapper, _loggerRoomImages, httpContextAccessor);
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