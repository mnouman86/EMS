namespace CleanArc.Application.Contracts.Persistence;

public interface IUnitOfWork
{
    public IUserRefreshTokenRepository UserRefreshTokenRepository { get; }
    public IOrderRepository OrderRepository { get; }
    public IURLRepository URLRepository { get; }
    public IAgeTypeRepository AgeTypeRepository { get; }
    public IHotelRepository HotelRepository { get; }
    public IAmenityRepository AmenityRepository { get; }
    public IRoomTypeRepository RoomTypeRepository { get; }
    public IRoomDetailsRepository RoomDetailsRepository { get; }
    public ICategoryRepository CategoryRepository { get; }
    public ISearchHotelRepository SearchHotelRepository { get; }
    public ISearchHotelImageRepository SearchHotelImageRepository { get; }
    public ISearchHotelAmenitiesRepository SearchHotelAmenitiesRepository { get; }
    public ISearchCountryCitiesRepository SearchCountryCitiesRepository { get; }
    public ISearchHotelRoomDetailRepository SearchHotelRoomDetailRepository { get; }
    public ISearchRoomAmenitiesRepository SearchRoomAmenitiesRepository { get; }
    public IRoomImagesRepository RoomImagesRepository { get; }
    public IRoomSizeUnitReposirory RoomSizeUnitReposirory { get; }
    public IServiceRepository ServiceRepository { get; }
    public IServiceCategoryRepository ServiceCategoryRepository { get; }
    public ICountryRepository CountryRepository { get; }
    public IStateRepository StateRepository { get; }
    public ICityRepository CityRepository { get; }
    public ILanguageRepository LanguageRepository { get; }
    public IMappingHotelAmenityRepository MappingHotelAmenityRepository { get; }
    public IMappingHotelLanguageRepository MappingHotelLanguageRepository { get; }
    public IMappingRoomAmenitiesRepository MappingRoomAmenitiesRepository { get; }
    public IMappingRoomImageRepository MappingRoomImageRepository { get; }
    public IBusinessRepository BusinessRepository { get; }
    public IBusinessTypeRepository BusinessTypeRepository { get; }
    public IBankRepository BankRepository { get; }
    public IBusinessBankAccountRepository BusinessBankAccountRepository { get; }
    public ICarDetailRepository CarDetailRepository { get; }
    public ISearchCarImageRepository SearchCarImageRepository { get; }
    public ISearchCarAmenitiesRepository SearchCarAmenitiesRepository { get; }
    public IMappingCarAmenityRepository MappingCarAmenityRepository { get; }
    public ISearchBusinessCarDetailRepository SearchBusinessCarDetailRepository { get; }
    public ISearchBusinessDetailRepository SearchBusinessDetailRepository { get; }
    public ICarImageRepository CarImageRepository { get; }
    public IHotelImageRepository HotelImageRepository { get; }
    public IRoomVisualRepository RoomVisualRepository { get; }
    public IAdvertisementRepository AdvertisementRepository { get; }
    public IAdvertisementPlaceRepository AdvertisementPlaceRepository { get; }
    public IAdvertisementPageRepository AdvertisementPageRepository { get; }
    public IActivityTypeRepository ActivityTypeRepository { get; }
    public IActivityNatureRepository ActivityNatureRepository { get; }
    public IActivityManagerRepository ActivityManagerRepository { get; }
    public IActivityIncludedOptionRepository ActivityIncludedOptionRepository { get; }
    public IDisabilityOptionRepository DisabilityOptionRepository { get; }
    public ISubServiceRepository SubServiceRepository { get; }
    public IActivityPrivateParticipantRepository ActivityPrivateParticipantRepository { get; }
    public IActivitySeasonRepository ActivitySeasonRepository { get; }
    public IActivityTransportationRepository ActivityTransportationRepository { get; }
    public IActivityAddressRepository ActivityAddressRepository { get; }
    public IActivityRepository ActivityRepository { get; }
    public IBusinessProfileRepository BusinessProfileRepository { get; }
    public IActivityScheduleRepository ActivityScheduleRepository { get; }

    public IActivityDisabilityOptionRepository ActivityDisabilityOptionRepository { get; }
    public IActivityImageMappingRepository ActivityImageMappingRepository { get; }
    public IActivitySeasonMappingRepository ActivitySeasonMappingRepository { get; }





    Task CommitAsync();
    ValueTask RollBackAsync();
}