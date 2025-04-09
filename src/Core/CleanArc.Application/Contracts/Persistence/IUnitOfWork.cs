namespace CleanArc.Application.Contracts.Persistence;

public interface IUnitOfWork
{
    public IUserRefreshTokenRepository UserRefreshTokenRepository { get; }
    public IOrderRepository OrderRepository { get; }
    public IUserSignUpRewardsRepository UserSignUpRewardsRepository { get; }
    public IUserAssignRewardsRepository UserAssignRewardsRepository { get; }

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
    public ISectionRepository SectionRepository { get; }
    public ICountryRepository CountryRepository { get; }
    public IStateRepository StateRepository { get; }
    public ICityRepository CityRepository { get; }
    public ILanguageRepository LanguageRepository { get; }
    public IAmenityMappingRepository AmenityMappingRepository { get; }
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
    public IGenericMediaRepository GenericMediaRepository { get; }
    public IRoomVisualRepository RoomVisualRepository { get; }
    public IAdvertisementRepository AdvertisementRepository { get; }
    public IAdvertisementPlaceRepository AdvertisementPlaceRepository { get; }
    public IAdvertisementPageRepository AdvertisementPageRepository { get; }
    public IActivityTypeRepository ActivityTypeRepository { get; }
    public IActivityNatureRepository ActivityNatureRepository { get; }
    public IActivitySupervisorRepository ActivitySupervisorRepository { get; }
    public IActivityIncludedOptionRepository ActivityIncludedOptionRepository { get; }
    public IDisabilityOptionRepository DisabilityOptionRepository { get; }
    public ISubServiceRepository SubServiceRepository { get; }
    public IActivityPrivateParticipantRepository ActivityPrivateParticipantRepository { get; }
    public IActivitySeasonRepository ActivitySeasonRepository { get; }
    public IActivityTransportationRepository ActivityTransportationRepository { get; }
    public IGenericAddressRepository ActivityAddressRepository { get; }
    public IActivityRepository ActivityRepository { get; }
    public IBusinessProfileRepository BusinessProfileRepository { get; }
    public IActivityScheduleRepository ActivityScheduleRepository { get; }

    public IActivityDisabilityOptionRepository ActivityDisabilityOptionRepository { get; }
    public IActivityImageMappingRepository ActivityImageMappingRepository { get; }
    public IActivitySeasonMappingRepository ActivitySeasonMappingRepository { get; }
    public IActivityDisabilityMappingRepository ActivityDisabilityMappingRepository { get; }

    public IActivityGroupRepository ActivityGroupRepository { get; }

    public ICurrencyRepository CurrencyRepository { get; }
    public IActivityPricePerParticipantRepository ActivityPricePerParticipantRepository { get; }
    public IActivityPerGroupPriceRepository ActivityPerGroupPriceRepository { get; }
    public IActivityIncludeOptionMappingRepository ActivityIncludeOptionMappingRepository { get; }
    public IActivityAddressMappingRepository ActivityAddressMappingRepository { get; }
    public IActivityIDImageMappingRepository ActivityIDImageMappingRepository { get; }

    public IProcessOrderRepository ProcessOrderRepository { get; }
    public IPackageTypeRepository PackageTypeRepository { get; }
    public IPackageDetailRepository PackageDetailRepository { get; }
    public IFAQsRepository FAQsRepository { get; }
    public ISearchFilterStayRepository SearchFilterStayRepository { get; }
    public ISearchFilterThingsToDoRepository SearchFilterThingsToDoRepository { get; }
    public ICarRentalSearchFilterRepository CarRentalSearchFilterRepository { get; }
    public IKBDetailRepository KBDetailRepository { get; }
    public IKBRelatedUrlLinkRepository KBRelatedUrlLinkRepository { get; }
    public IKBDescriptionRepository KBDescriptionRepository { get; }
    public IKBMediaRepository KBMediaRepository { get; }
    public IKBTimingRepository KBTimingRepository { get; }
    public ICoreAreaRepository CoreAreaRepository { get; }
    public IKBAddressRepository KBAddressRepository { get; }
    public IKBWhenToVisitRepository KBWhenToVisitRepository { get; }
    public IKBInterestedRepository KBInterestedRepository { get; }
    public IUserExperienceRepository UserExperienceRepository { get; }

    public ICheckProfileStatusRepository CheckProfileStatusRepository { get; }
    public IPopularItemsVisitRepository PopularItemsVisitRepository { get; }
    public ICustomerReviewRepository CustomerReviewRepository { get; }
    public ICustomerAwarenessRepository CustomerAwarenessRepository { get; }
    public ICampaignRepository CampaignRepository { get; }
    public ICampaignTargetRepository CampaignTargetRepository { get; }
    public ICampaignTargetItemsRepository CampaignTargetItemsRepository { get; }


    public IGroupActivityParticipantsRepository GroupActivityParticipantsRepository { get; }
    public ICampaignScheduleRepository CampaignScheduleRepository { get; }
    public IHomeSliderRepository HomeSliderRepository { get; }






    Task CommitAsync();
    ValueTask RollBackAsync();
}