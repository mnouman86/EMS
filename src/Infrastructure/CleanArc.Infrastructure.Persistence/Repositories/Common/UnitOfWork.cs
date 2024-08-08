using CleanArc.Application.Contracts.Persistence;
using CleanArc.Domain.Entities.ActivityDisabilityOption;
using CleanArc.Domain.Entities.ActivityTransportation;
using CleanArc.Domain.Entities.SearchCarAmenities;
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
    public IBusinessRepository BusinessRepository { get; set; }
    public IBusinessTypeRepository BusinessTypeRepository { get; set; }
    public IBankRepository BankRepository { get; set; }
    public IBusinessBankAccountRepository BusinessBankAccountRepository { get; set; }
    public ICarDetailRepository CarDetailRepository { get; set; }
    public ISearchCarImageRepository SearchCarImageRepository {  get; set; }
    public ISearchCarAmenitiesRepository SearchCarAmenitiesRepository { get; set; }
    public IMappingCarAmenityRepository MappingCarAmenityRepository { get; set; }
    public ISearchBusinessCarDetailRepository SearchBusinessCarDetailRepository { get; set; }
    public ISearchBusinessDetailRepository SearchBusinessDetailRepository { get; set; }
    public ICarImageRepository CarImageRepository { get; set; }
    public IHotelImageRepository HotelImageRepository { get; set; }
    public IRoomVisualRepository RoomVisualRepository { get; set; }
    public IAdvertisementRepository AdvertisementRepository { get; set; }
    public IAdvertisementPlaceRepository AdvertisementPlaceRepository { get; set; }
    public IAdvertisementPageRepository AdvertisementPageRepository { get; set; }
    public IActivityTypeRepository ActivityTypeRepository { get; set; }
    public IActivityNatureRepository ActivityNatureRepository { get; set; }
    public IActivityManagerRepository ActivityManagerRepository { get; set; }
    public IActivityIncludedOptionRepository ActivityIncludedOptionRepository { get; set; }
    public IDisabilityOptionRepository DisabilityOptionRepository { get; set; }
    public ISubServiceRepository SubServiceRepository { get; set; }
    public IActivityPrivateParticipantRepository ActivityPrivateParticipantRepository { get; set; }
    public IActivitySeasonRepository ActivitySeasonRepository { get; set; }
    public IActivityTransportationRepository ActivityTransportationRepository { get; set; }
    public IActivityAddressRepository ActivityAddressRepository { get; set; }
    public IActivityRepository ActivityRepository { get; set; }
    public IBusinessProfileRepository BusinessProfileRepository { get; set; }
    public IActivityScheduleRepository ActivityScheduleRepository { get; set; }
    public IActivityDisabilityOptionRepository ActivityDisabilityOptionRepository { get; set; }


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
        ILogger<BusinessRepository> _loggerBusiness,
        ILogger<BusinessTypeRepository> _loggerBusinessType,

        ILogger<BankRepository> _loggerBank,
        ILogger<BusinessBankAccountRepository> _loggerBusinessBankAccount,
        ILogger<CarDetailRepository> _loggerCarDetail,
        ILogger<SearchCarImageRepository> _loggerSearchCarImage,
        ILogger<SearchCarAmenitiesRepository> _loggerSearchCarAmenities,
        ILogger<MappingCarAmenityRepository> _loggerMappingCarAmenity,
        ILogger<SearchBusinessCarDetailRepository> _loggerSearchBusinessCarDetail,
                                ILogger<SearchBusinessDetailRepository> _loggerSearchBusinessDetail,
                                ILogger<CarImageRepository> _loggerCarImage,
                                ILogger<HotelImageRepository> _loggerHotelImage,
                                ILogger<RoomVisualRepository> _loggerRoomVisual,
                                ILogger<AdvertisementRepository> _loggerAdvertisement,
                                ILogger<AdvertisementPlaceRepository> _loggerAdvertisementPlace,
                                ILogger<AdvertisementPageRepository> _loggerAdvertisementPage,
                                ILogger<ActivityTypeRepository> _loggerActivityType,
                                ILogger<ActivityNatureRepository> _loggerActivityNature,
                                ILogger<ActivityManagerRepository> _loggerActivityManager,
                                ILogger<ActivityIncludedOptionRepository> _loggerActivityIncludedOption,
                                ILogger<DisabilityOptionRepository> _loggerDisabilityOption,
                                ILogger<SubServiceRepository> _loggerSubService,
        ILogger<ActivityPrivateParticipantRepository> _loggerActivityPrivateParticipant,
        ILogger<ActivitySeasonRepository> _loggerActivitySeason,
        ILogger<ActivityTransportationRepository> _loggerActivityTransportation,
        ILogger<ActivityAddressRepository> _loggerActivityAddress,
        ILogger<ActivityRepository> _loggerActivity,
        ILogger<BusinessProfileRepository> _loggerBusinessProfile,
        ILogger<ActivityScheduleRepository> _loggerActivitySchedule,
        ILogger<ActivityDisabilityOptionRepository> _loggerActivityDisabilityOption,




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
        BusinessRepository = new BusinessRepository(configuration, mapper, _loggerBusiness, httpContextAccessor);
        BusinessTypeRepository = new BusinessTypeRepository(configuration, mapper, _loggerBusinessType, httpContextAccessor);

        BankRepository = new BankRepository(configuration, mapper, _loggerBank, httpContextAccessor);
        BusinessBankAccountRepository = new BusinessBankAccountRepository(configuration, mapper, _loggerBusinessBankAccount, httpContextAccessor);
        CarDetailRepository = new CarDetailRepository(configuration, mapper, _loggerCarDetail, httpContextAccessor);
        SearchCarImageRepository = new SearchCarImageRepository(configuration, mapper, _loggerSearchCarImage, httpContextAccessor);
        SearchCarAmenitiesRepository = new SearchCarAmenitiesRepository(configuration, mapper, _loggerSearchCarAmenities, httpContextAccessor);
        MappingCarAmenityRepository = new MappingCarAmenityRepository(configuration, mapper, _loggerMappingCarAmenity, httpContextAccessor);
        SearchBusinessCarDetailRepository = new SearchBusinessCarDetailRepository(configuration, mapper, _loggerSearchBusinessCarDetail, httpContextAccessor);
        SearchBusinessDetailRepository = new SearchBusinessDetailRepository(configuration, mapper, _loggerSearchBusinessDetail, httpContextAccessor);
        CarImageRepository = new CarImageRepository(configuration, mapper, _loggerCarImage, httpContextAccessor);
        HotelImageRepository = new HotelImageRepository(configuration, mapper, _loggerHotelImage, httpContextAccessor);
        RoomVisualRepository = new RoomVisualRepository(configuration, mapper, _loggerRoomVisual, httpContextAccessor);
        AdvertisementRepository = new AdvertisementRepository(configuration, mapper, _loggerAdvertisement, httpContextAccessor);
        AdvertisementPlaceRepository = new AdvertisementPlaceRepository(configuration, mapper, _loggerAdvertisementPlace, httpContextAccessor);
        AdvertisementPageRepository = new AdvertisementPageRepository(configuration, mapper, _loggerAdvertisementPage, httpContextAccessor);
        ActivityTypeRepository = new ActivityTypeRepository(configuration, mapper, _loggerActivityType, httpContextAccessor);
        ActivityNatureRepository = new ActivityNatureRepository(configuration, mapper, _loggerActivityNature, httpContextAccessor);
        ActivityManagerRepository = new ActivityManagerRepository(configuration, mapper, _loggerActivityManager, httpContextAccessor);
        ActivityIncludedOptionRepository = new ActivityIncludedOptionRepository(configuration, mapper, _loggerActivityIncludedOption, httpContextAccessor);
        DisabilityOptionRepository = new DisabilityOptionRepository(configuration, mapper, _loggerDisabilityOption, httpContextAccessor);
        SubServiceRepository = new SubServiceRepository(configuration, mapper, _loggerSubService, httpContextAccessor);
        ActivityPrivateParticipantRepository = new ActivityPrivateParticipantRepository(configuration, mapper, _loggerActivityPrivateParticipant, httpContextAccessor);
        ActivitySeasonRepository = new ActivitySeasonRepository(configuration, mapper, _loggerActivitySeason, httpContextAccessor);
        ActivityTransportationRepository = new ActivityTransportationRepository(configuration, mapper, _loggerActivityTransportation, httpContextAccessor);
        ActivityAddressRepository = new ActivityAddressRepository(configuration, mapper, _loggerActivityAddress, httpContextAccessor);
        ActivityRepository = new ActivityRepository(configuration, mapper, _loggerActivity, httpContextAccessor);
        BusinessProfileRepository = new BusinessProfileRepository(configuration, mapper, _loggerBusinessProfile, httpContextAccessor);
        ActivityScheduleRepository = new ActivityScheduleRepository(configuration, mapper, _loggerActivitySchedule, httpContextAccessor);
        ActivityDisabilityOptionRepository = new ActivityDisabilityOptionRepository(configuration, mapper, _loggerActivityDisabilityOption, httpContextAccessor);

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