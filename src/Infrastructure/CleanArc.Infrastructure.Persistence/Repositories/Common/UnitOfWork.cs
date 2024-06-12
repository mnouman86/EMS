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
    public IRoomSizeUnitReposirory RoomSizeUnitReposirory { get; set; }
    public IServiceRepository ServiceRepository { get; set; }
    public IServiceCategoryRepository ServiceCategoryRepository { get; set; }
    public ICountryRepository CountryRepository { get; set; }
    public IStateRepository StateRepository { get; set; }
    public ICityRepository CityRepository { get; set; }
    public IMappingHotelAmenityRepository MappingHotelAmenityRepository { get; set; }
    public IMappingHotelLanguageRepository MappingHotelLanguageRepository { get; set; }
    public IMappingRoomAmenitiesRepository MappingRoomAmenitiesRepository { get; set; }
    public IMappingRoomImageRepository MappingRoomImageRepository { get; set; }
    public ILanguageRepository LanguageRepository { get; set; }
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
        ILogger<RoomSizeUnitReposirory> _loggerRoomSizeUnit,
                                ILogger<ServiceRepository> _loggerService,
                                ILogger<ServiceCategoryRepository> _loggerServiceCategory,
                                ILogger<CountryRepository> _loggerCountry,
                                ILogger<StateRepository> _loggerState,
                                ILogger<CityRepository> _loggerCity,
                                ILogger<LanguageRepository> _loggerLanguage,
                                ILogger<MappingHotelAmenityRepository> _loggerMappingHotelAmenity,
                                ILogger<MappingHotelLanguageRepository> _loggerMappingHotelLanguage,
        ILogger<MappingRoomAmenitiesRepository> _loggerMappingRoomAmenities,
        ILogger<MappingRoomImageRepository> _loggerMappingRoomImage,







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
        RoomSizeUnitReposirory = new RoomSizeUnitReposirory(configuration, mapper, _loggerRoomSizeUnit, httpContextAccessor);
        ServiceRepository= new ServiceRepository(configuration, mapper, _loggerService, httpContextAccessor);
        ServiceCategoryRepository=new ServiceCategoryRepository(configuration, mapper, _loggerServiceCategory, httpContextAccessor);
        CountryRepository=new CountryRepository(configuration, mapper, _loggerCountry, httpContextAccessor);
        StateRepository = new StateRepository(configuration, mapper, _loggerState, httpContextAccessor);
        CityRepository = new CityRepository(configuration, mapper, _loggerCity, httpContextAccessor);
        MappingHotelAmenityRepository=new MappingHotelAmenityRepository(configuration, mapper, _loggerMappingHotelAmenity, httpContextAccessor);
        MappingHotelLanguageRepository=new MappingHotelLanguageRepository(configuration, mapper, _loggerMappingHotelLanguage, httpContextAccessor);
        MappingRoomAmenitiesRepository=new MappingRoomAmenitiesRepository(configuration, mapper, _loggerMappingRoomAmenities, httpContextAccessor);
        MappingRoomImageRepository = new MappingRoomImageRepository(configuration, mapper, _loggerMappingRoomImage, httpContextAccessor);

        LanguageRepository = new LanguageRepository(configuration, mapper, _loggerLanguage, httpContextAccessor);
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