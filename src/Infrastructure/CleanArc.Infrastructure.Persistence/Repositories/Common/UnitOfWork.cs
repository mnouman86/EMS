using CleanArc.Application.Contracts.Persistence;
using CleanArc.Domain.Entities.ActivityDisabilityOption;
using CleanArc.Domain.Entities.ActivityIncludeOptionMapping;
using CleanArc.Domain.Entities.ActivityTransportation;
using CleanArc.Domain.Entities.SearchCarAmenities;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging; 
using CleanArc.Domain.Common;
using CleanArc.Application.Services.Aggregators;
using CleanArc.Infrastructure.Persistence.Services;

namespace CleanArc.Infrastructure.Persistence.Repositories.Common;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;
    private readonly HotelProviderAggregator _hotelProviderAggregator;

    public IUserRefreshTokenRepository UserRefreshTokenRepository { get; }
    public IUserSignUpRewardsRepository  UserSignUpRewardsRepository { get; }
    public IUserAssignRewardsRepository UserAssignRewardsRepository { get; }

    public IOrderRepository OrderRepository { get; }
    public IURLRepository URLRepository { get; set; }
    public ISearchAutoCompleteRepository SearchAutoCompleteRepository { get; set; }
    public IOneBillPaymentRepository OneBillPaymentRepository { get; set; }
    public IStartupDataRepository StartupDataRepository { get; set; }
    public IAgeTypeRepository AgeTypeRepository { get; set; }
    public IGenderRepository GenderRepository { get; set; }
    public IFeedbackSubjectTypeRepository FeedbackSubjectTypeRepository { get; set; }
    public IFeedbackStatusRepository FeedbackStatusRepository { get; set; }
    public IHotelRepository HotelRepository { get; set; }
    public IAmenityRepository AmenityRepository { get; set; }
    public IRoomTypeRepository RoomTypeRepository { get; set; }
	public IUserTypeRepository UserTypeRepository { get; set; }
	public IRoomRateRepository RoomRateRepository { get; set; }
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
    public IVehicleTypeRepository VehicleTypeRepository { get; set; }
    public IDrivingAvailabilityOptionsRepository DrivingAvailabilityOptionsRepository { get; set; }
    public IServiceCategoryRepository ServiceCategoryRepository { get; set; }
    public ISectionRepository SectionRepository { get; set; }
    public ICountryRepository CountryRepository { get; set; }
    public IStateRepository StateRepository { get; set; }
    public ICityRepository CityRepository { get; set; }
    public IAmenityMappingRepository AmenityMappingRepository { get; set; }
    public IMappingHotelLanguageRepository MappingHotelLanguageRepository { get; set; }
    public IMappingRoomAmenitiesRepository MappingRoomAmenitiesRepository { get; set; }
    public IMappingRoomImageRepository MappingRoomImageRepository { get; set; }
    public ILanguageRepository LanguageRepository { get; set; }
    public IRatePlanTypeRepository RatePlanTypeRepository { get; set; }
    public IRatePlanRepository RatePlanRepository { get; set; }
    public IRoomViewRepository RoomViewRepository { get; set; }
    public IManufacturerRepository ManufacturerRepository { get; set; }
    public IFilterCategoryRepository FilterCategoryRepository { get; set; }
    public IWishListRepository WishListRepository { get; set; }
    public ILastMinuteDealRepository LastMinuteDealRepository { get; set; }
    public IWishListNameRepository WishListNameRepository { get; set; }
    public IOutDoorRepository OutDoorRepository { get; set; }
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
    public IGenericMediaRepository GenericMediaRepository { get; set; }
    public IRoomVisualRepository RoomVisualRepository { get; set; }
    public IAdvertisementRepository AdvertisementRepository { get; set; }
    public IAdvertisementPlaceRepository AdvertisementPlaceRepository { get; set; }
    public IAdvertisementPageRepository AdvertisementPageRepository { get; set; }
    public IActivityTypeRepository ActivityTypeRepository { get; set; }
    public IActivityNatureRepository ActivityNatureRepository { get; set; }
    public IActivitySupervisorRepository ActivitySupervisorRepository { get; set; }
    public IActivityIncludedOptionRepository ActivityIncludedOptionRepository { get; set; }
    public IDisabilityOptionRepository DisabilityOptionRepository { get; set; }
    public ISubServiceRepository SubServiceRepository { get; set; }
    public IActivityPrivateParticipantRepository ActivityPrivateParticipantRepository { get; set; }
    public IActivitySeasonRepository ActivitySeasonRepository { get; set; }
    public IActivityTransportationRepository ActivityTransportationRepository { get; set; }
    public IGenericAddressRepository ActivityAddressRepository { get; set; }
    public IActivityRepository ActivityRepository { get; set; }
    public ISearchActivityDetailRepository SearchActivityDetailRepository { get; set; }
    public IBusinessProfileRepository BusinessProfileRepository { get; set; }
    public IActivityScheduleRepository ActivityScheduleRepository { get; set; }
    public IActivityDisabilityOptionRepository ActivityDisabilityOptionRepository { get; set; }
    public IActivityImageMappingRepository ActivityImageMappingRepository { get; set; }
    public IActivitySeasonMappingRepository ActivitySeasonMappingRepository { get; set; }
    public IActivityDisabilityMappingRepository ActivityDisabilityMappingRepository { get; set; }

    public IActivityGroupRepository ActivityGroupRepository { get; set; }
    public ICurrencyRepository CurrencyRepository { get; set; }
    public IActivityPricePerParticipantRepository ActivityPricePerParticipantRepository { get; set; }
    public IActivityPerGroupPriceRepository ActivityPerGroupPriceRepository { get; set; }
    public IActivityIncludeOptionMappingRepository ActivityIncludeOptionMappingRepository { get; set; }
    public IActivityAddressMappingRepository ActivityAddressMappingRepository { get; set; }
    public IActivityIDImageMappingRepository ActivityIDImageMappingRepository { get; set; }
    public IProcessOrderRepository ProcessOrderRepository { get; set; }
    public IPostPaymentStatusRepository PostPaymentStatusRepository { get; set; }
    public IPackageTypeRepository PackageTypeRepository { get; set; }
    public IPackageDetailRepository PackageDetailRepository { get; set; }
    public IFAQsRepository FAQsRepository { get; set; }
    public ISearchFilterStayRepository SearchFilterStayRepository { get; set; }
    public ISearchFilterThingsToDoRepository SearchFilterThingsToDoRepository { get; set; }
    public ICarRentalSearchFilterRepository CarRentalSearchFilterRepository { get; set; }
    public IKBDetailRepository KBDetailRepository { get; set; }
    public IKBRelatedUrlLinkRepository KBRelatedUrlLinkRepository { get; set; }
    public IKBDescriptionRepository KBDescriptionRepository { get; set; }
    public IKBMediaRepository KBMediaRepository { get; set; }
    public IKBTimingRepository KBTimingRepository { get; set; }
    public ICoreAreaRepository CoreAreaRepository { get; set; }
    public IContactFormRepository ContactFormRepository { get; set; }
    public IKBWhenToVisitRepository KBWhenToVisitRepository { get; set; }
    public IKBInterestedRepository KBInterestedRepository { get; set; }
    public IUserExperienceRepository UserExperienceRepository { get; set; }
    public ICheckProfileStatusRepository CheckProfileStatusRepository { get; set; }
    public IPopularItemsVisitRepository PopularItemsVisitRepository { get; set; }
    public ICustomerReviewRepository CustomerReviewRepository { get; set; }
    public ICustomerAwarenessRepository CustomerAwarenessRepository { get; set; }
    public ICampaignRepository CampaignRepository { get; set; }
    public ICampaignTargetRepository CampaignTargetRepository { get; set; }
    public ICampaignTargetItemsRepository CampaignTargetItemsRepository { get; set; }

    public IGroupActivityParticipantsRepository GroupActivityParticipantsRepository { get; set; }
    public ICampaignScheduleRepository CampaignScheduleRepository { get; set; }
    public IHomeSliderRepository HomeSliderRepository { get; set; }

    public IKBAddressRepository KBAddressRepository { get; set; }

    private readonly IConfiguration configuration;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;


    public UnitOfWork(ApplicationDbContext db, HotelProviderAggregator hotelProviderAggregator, IEmailService emailService, IConfiguration configuration,IMapper mapper, 
        ILogger<URLRepository> logger,
        ILogger<SearchAutoCompleteRepository> _loggerSearchAutoCompleteRepository,
        ILogger<OneBillPaymentRepository> _loggerOneBillPaymentRepository,
        ILogger<StartupDataRepository> _loggerStartupData,
        ILogger<AgeTypeRepository> _logger, 
        ILogger<GenderRepository> _loggerGender, 
        ILogger<FeedbackSubjectTypeRepository> _loggerFeedbackSubjectType, 
        ILogger<FeedbackStatusRepository> _loggerFeedbackStatus, 
        ILogger<HotelRepository> _loggerHotel,
        ILogger<AmenityRepository> _loggerAmenity,
        ILogger<RoomTypeRepository> _loggerRoomType,
		ILogger<UserTypeRepository> _loggerUserType,
		ILogger<RoomRateRepository> _loggerRoomRate,
        ILogger<RoomDetailsRepository> _loggerRoomDetails,
        ILogger<CategoryRepository> _loggerCategory,
        ILogger<SearchHotelDetailRepository> _loggerSearchHotel,
        ILogger<SearchHotelImageRepository> _loggerSearchImage,
        ILogger<SearchHotelAmenitiesRepository> _loggerSearchHotelAmenities,
        ILogger<SearchCountryCitiesRepository> _loggerSearchCountryCities,
        ILogger<SearchHotelRoomDetailRepository> _loggerSearchHotelRoomDetail,
        ILogger<SearchRoomAmenitiesRepository> _loggerSearchRoomAmenities,
        ILogger<RoomImagesRepository> _loggerRoomImages,
        ILogger<RoomSizeUnitReposirory> _loggerRoomSizeUnit,
        ILogger<ServiceRepository> _loggerService,
        ILogger<VehicleTypeRepository> _loggerVehicleType,
        ILogger<DrivingAvailabilityOptionsRepository> _loggerDrivingAvailabilityOptions,
        ILogger<ServiceCategoryRepository> _loggerServiceCategory,
        ILogger<SectionRepository> _loggerSection,
        ILogger<CountryRepository> _loggerCountry,
        ILogger<StateRepository> _loggerState,
        ILogger<CityRepository> _loggerCity,
        ILogger<LanguageRepository> _loggerLanguage,
        ILogger<RatePlanTypeRepository> _loggerRatePlanType,
        ILogger<RatePlanRepository> _loggerRatePlan,
        ILogger<RoomViewRepository> _loggerRoomView,
        ILogger<ManufacturerRepository> _loggerManufacturer,
        ILogger<FilterCategoryRepository> _loggerFilterCategory,
        ILogger<LastMinuteDealRepository> _loggerLastMinuteDeal,
        ILogger<WishListRepository> _loggerWishList,
        ILogger<WishListNameRepository> _loggerWishListName,
        ILogger<OutDoorRepository> _loggerOutDoor,
        ILogger<AmenityMappingRepository> _loggerAmenityMapping,
        ILogger<MappingHotelLanguageRepository> _loggerMappingHotelLanguage,
        ILogger<MappingRoomAmenitiesRepository> _loggerMappingRoomAmenities,
        ILogger<MappingRoomImageRepository> _loggerMappingRoomImage,
        ILogger<BusinessRepository> _loggerBusiness,
        ILogger<BusinessTypeRepository> _loggerBusinessType,
        ILogger<UserSignUpRewardsRepository> _loggerUserSignUpRewards,
        ILogger<UserAssignRewardsRepository> _loggerUserAssignRewards,


        ILogger<BankRepository> _loggerBank,
        ILogger<BusinessBankAccountRepository> _loggerBusinessBankAccount,
        ILogger<CarDetailRepository> _loggerCarDetail,
        ILogger<SearchCarImageRepository> _loggerSearchCarImage,
        ILogger<SearchCarAmenitiesRepository> _loggerSearchCarAmenities,
        ILogger<MappingCarAmenityRepository> _loggerMappingCarAmenity,
        ILogger<SearchBusinessCarDetailRepository> _loggerSearchBusinessCarDetail,
                                ILogger<SearchBusinessDetailRepository> _loggerSearchBusinessDetail,
                                ILogger<CarImageRepository> _loggerCarImage,
                                ILogger<GenericMediaRepository> _loggerHotelImage,
                                ILogger<RoomVisualRepository> _loggerRoomVisual,
                                ILogger<AdvertisementRepository> _loggerAdvertisement,
                                ILogger<AdvertisementPlaceRepository> _loggerAdvertisementPlace,
                                ILogger<AdvertisementPageRepository> _loggerAdvertisementPage,
                                ILogger<ActivityTypeRepository> _loggerActivityType,
                                ILogger<ActivityNatureRepository> _loggerActivityNature,
                                ILogger<ActivitySupervisorRepository> _loggerActivitySupervisor,
                                ILogger<ActivityIncludedOptionRepository> _loggerActivityIncludedOption,
                                ILogger<DisabilityOptionRepository> _loggerDisabilityOption,
                                ILogger<SubServiceRepository> _loggerSubService,
        ILogger<ActivityPrivateParticipantRepository> _loggerActivityPrivateParticipant,
        ILogger<ActivitySeasonRepository> _loggerActivitySeason,
        ILogger<ActivityTransportationRepository> _loggerActivityTransportation,
        ILogger<GenericAddressRepository> _loggerActivityAddress,
        ILogger<ActivityRepository> _loggerActivity,
        ILogger<SearchActivityDetailRepository> _loggerSearchActivityDetail,
        ILogger<BusinessProfileRepository> _loggerBusinessProfile,
        ILogger<ActivityScheduleRepository> _loggerActivitySchedule,
        ILogger<ActivityDisabilityOptionRepository> _loggerActivityDisabilityOption,
        ILogger<ActivityImageMappingRepository> _loggerActivityImageMapping,
        ILogger<ActivitySeasonMappingRepository> _loggerActivitySeasonMapping,
        ILogger<ActivityGroupRepository> _loggerActivityGroup,
        ILogger<CurrencyRepository> _loggerCurrency,
                ILogger<ActivityDisabilityMappingRepository> _loggerActivityDisabilityMapping,
                ILogger<ActivityPricePerParticipantRepository> _loggerActivityPricePerParticipant,
                ILogger<ActivityPerGroupPriceRepository> _loggerActivityPerGroupPrice,
                ILogger<ActivityIncludeOptionMappingRepository> _loggerActivityIncludeOptionMapping,
                ILogger<ActivityAddressMappingRepository> _loggerActivityAddressMapping,
                ILogger<ActivityIDImageMappingRepository> _loggerActivityIDImageMapping,
                ILogger<ProcessOrderRepository> _loggerProcessOrder,
                ILogger<PostPaymentStatusRepository> _loggerPostPaymentStatus,
                ILogger<PackageTypeRepository> _loggerPackageType,
                                ILogger<PackageDetailRepository> _loggerPackageDetail,
            ILogger<FAQsRepository> _loggerFAQs,
           ILogger<SearchFilterStayRepository> _loggerSearchFilterStay,
                      ILogger<SearchFilterThingsToDoRepository> _loggerSearchFilterThingsToDo,
                                            ILogger<CarRentalSearchFilterRepository> _loggerCarRentalSearchFilter,
                                            ILogger<KBDetailRepository> _loggerKBDetail,
                                            ILogger<KBRelatedUrlLinkRepository> _loggerKBRelatedUrlLink,
                                            ILogger<KBDescriptionRepository> _loggerKBDescription,
           ILogger<KBMediaRepository> _loggerKBMedia,
           ILogger<KBTimingRepository> _loggerKBTiming,
           ILogger<CoreAreaRepository> _loggerCoreArea,
           ILogger<ContactFormRepository> _loggerContactForm,
           ILogger<KBAddressRepository> _loggerKBAddress,
           ILogger<KBWhenToVisitRepository> _loggerKBWhenToVisit,
           ILogger<KBInterestedRepository> _loggerKBInterested,
           ILogger<UserExperienceRepository> _loggerUserExperience,
           ILogger<CheckProfileStatusRepository> _loggerCheckProfileStatus,
           ILogger<PopularItemsVisitRepository> _loggerPopularItemsVisit,
           ILogger<CustomerReviewRepository> _loggerCustomerReview,
           ILogger<CustomerAwarenessRepository> _loggerCustomerAwareness,
           ILogger<CampaignRepository> _loggerCampaign,
           ILogger<CampaignTargetRepository> _loggerCampaignTarget,
           ILogger<CampaignTargetItemsRepository> _loggerCampaignTargetItems,

           ILogger<GroupActivityParticipantsRepository> _loggerGroupActivityParticipants,
           ILogger<CampaignScheduleRepository> _loggerCampaignSchedule,
           ILogger<HomeSliderRepository> _loggerHomeSlider,





        IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        UserRefreshTokenRepository = new UserRefreshTokenRepository(_db);
        OrderRepository= new OrderRepository(_db);
        URLRepository = new URLRepository(configuration,mapper,logger,httpContextAccessor);
        SearchAutoCompleteRepository = new SearchAutoCompleteRepository(configuration,mapper, _loggerSearchAutoCompleteRepository, httpContextAccessor);
        OneBillPaymentRepository = new OneBillPaymentRepository(configuration,mapper, _loggerOneBillPaymentRepository, httpContextAccessor);
        StartupDataRepository = new StartupDataRepository(configuration);
        AgeTypeRepository = new AgeTypeRepository(configuration, mapper, _logger, httpContextAccessor);
        GenderRepository = new GenderRepository(configuration, mapper, _loggerGender, httpContextAccessor);
        FeedbackSubjectTypeRepository = new FeedbackSubjectTypeRepository(configuration, mapper, _loggerFeedbackSubjectType, httpContextAccessor);
        FeedbackStatusRepository = new FeedbackStatusRepository(configuration, mapper, _loggerFeedbackStatus, httpContextAccessor);
        HotelRepository=new HotelRepository(configuration, mapper, _loggerHotel, httpContextAccessor);
        AmenityRepository=new AmenityRepository(configuration, mapper, _loggerAmenity, httpContextAccessor);
        RoomTypeRepository=new RoomTypeRepository(configuration, mapper, _loggerRoomType, httpContextAccessor);
		UserTypeRepository = new UserTypeRepository(configuration, mapper, _loggerUserType, httpContextAccessor);
		RoomRateRepository =new RoomRateRepository(configuration, mapper, _loggerRoomRate, httpContextAccessor);
        RoomDetailsRepository=new RoomDetailsRepository(configuration, mapper, _loggerRoomDetails, httpContextAccessor, hotelProviderAggregator);
        CategoryRepository=new CategoryRepository(configuration, mapper, _loggerCategory, httpContextAccessor);
        SearchHotelRepository=new SearchHotelDetailRepository(configuration, mapper, _loggerSearchHotel, httpContextAccessor, hotelProviderAggregator);
        SearchHotelImageRepository = new SearchHotelImageRepository(configuration, mapper, _loggerSearchImage, httpContextAccessor);
        SearchHotelAmenitiesRepository=new SearchHotelAmenitiesRepository(configuration, mapper, _loggerSearchHotelAmenities, httpContextAccessor);
        SearchCountryCitiesRepository=new SearchCountryCitiesRepository(configuration, mapper, _loggerSearchCountryCities, httpContextAccessor);
        SearchHotelRoomDetailRepository=new SearchHotelRoomDetailRepository(configuration, mapper, _loggerSearchHotelRoomDetail, httpContextAccessor);
        SearchRoomAmenitiesRepository=new SearchRoomAmenitiesRepository(configuration, mapper, _loggerSearchRoomAmenities, httpContextAccessor);
        RoomImagesRepository=new RoomImagesRepository(configuration, mapper, _loggerRoomImages, httpContextAccessor);
        RoomSizeUnitReposirory = new RoomSizeUnitReposirory(configuration, mapper, _loggerRoomSizeUnit, httpContextAccessor);
        ServiceRepository= new ServiceRepository(configuration, mapper, _loggerService, httpContextAccessor);
        VehicleTypeRepository= new VehicleTypeRepository(configuration, mapper, _loggerVehicleType, httpContextAccessor);
        DrivingAvailabilityOptionsRepository = new DrivingAvailabilityOptionsRepository(configuration, mapper, _loggerDrivingAvailabilityOptions, httpContextAccessor);
        ServiceCategoryRepository=new ServiceCategoryRepository(configuration, mapper, _loggerServiceCategory, httpContextAccessor);
        SectionRepository=new SectionRepository(configuration, mapper, _loggerSection, httpContextAccessor);
        CountryRepository=new CountryRepository(configuration, mapper, _loggerCountry, httpContextAccessor);
        StateRepository = new StateRepository(configuration, mapper, _loggerState, httpContextAccessor);
        CityRepository = new CityRepository(configuration, mapper, _loggerCity, httpContextAccessor);
        AmenityMappingRepository=new AmenityMappingRepository(configuration, mapper, _loggerAmenityMapping, httpContextAccessor);
        MappingHotelLanguageRepository=new MappingHotelLanguageRepository(configuration, mapper, _loggerMappingHotelLanguage, httpContextAccessor);
        MappingRoomAmenitiesRepository=new MappingRoomAmenitiesRepository(configuration, mapper, _loggerMappingRoomAmenities, httpContextAccessor);
        MappingRoomImageRepository = new MappingRoomImageRepository(configuration, mapper, _loggerMappingRoomImage, httpContextAccessor);
        BusinessRepository = new BusinessRepository(configuration, mapper, _loggerBusiness, httpContextAccessor);
        BusinessTypeRepository = new BusinessTypeRepository(configuration, mapper, _loggerBusinessType, httpContextAccessor);
        
        
        UserSignUpRewardsRepository = new UserSignUpRewardsRepository(configuration, mapper, _loggerUserSignUpRewards, httpContextAccessor);
        UserAssignRewardsRepository = new UserAssignRewardsRepository(configuration, mapper, _loggerUserAssignRewards, httpContextAccessor);

        BankRepository = new BankRepository(configuration, mapper, _loggerBank, httpContextAccessor);
        BusinessBankAccountRepository = new BusinessBankAccountRepository(configuration, mapper, _loggerBusinessBankAccount, httpContextAccessor);
        CarDetailRepository = new CarDetailRepository(configuration, mapper, _loggerCarDetail, httpContextAccessor);
        SearchCarImageRepository = new SearchCarImageRepository(configuration, mapper, _loggerSearchCarImage, httpContextAccessor);
        SearchCarAmenitiesRepository = new SearchCarAmenitiesRepository(configuration, mapper, _loggerSearchCarAmenities, httpContextAccessor);
        MappingCarAmenityRepository = new MappingCarAmenityRepository(configuration, mapper, _loggerMappingCarAmenity, httpContextAccessor);
        SearchBusinessCarDetailRepository = new SearchBusinessCarDetailRepository(configuration, mapper, _loggerSearchBusinessCarDetail, httpContextAccessor);
        SearchBusinessDetailRepository = new SearchBusinessDetailRepository(configuration, mapper, _loggerSearchBusinessDetail, httpContextAccessor);
        CarImageRepository = new CarImageRepository(configuration, mapper, _loggerCarImage, httpContextAccessor);
        GenericMediaRepository = new GenericMediaRepository(configuration, mapper, _loggerHotelImage, httpContextAccessor);
        RoomVisualRepository = new RoomVisualRepository(configuration, mapper, _loggerRoomVisual, httpContextAccessor);
        AdvertisementRepository = new AdvertisementRepository(configuration, mapper, _loggerAdvertisement, httpContextAccessor);
        AdvertisementPlaceRepository = new AdvertisementPlaceRepository(configuration, mapper, _loggerAdvertisementPlace, httpContextAccessor);
        AdvertisementPageRepository = new AdvertisementPageRepository(configuration, mapper, _loggerAdvertisementPage, httpContextAccessor);
        ActivityTypeRepository = new ActivityTypeRepository(configuration, mapper, _loggerActivityType, httpContextAccessor);
        ActivityNatureRepository = new ActivityNatureRepository(configuration, mapper, _loggerActivityNature, httpContextAccessor);
        ActivitySupervisorRepository = new ActivitySupervisorRepository(configuration, mapper, _loggerActivitySupervisor, httpContextAccessor);
        ActivityIncludedOptionRepository = new ActivityIncludedOptionRepository(configuration, mapper, _loggerActivityIncludedOption, httpContextAccessor);
        DisabilityOptionRepository = new DisabilityOptionRepository(configuration, mapper, _loggerDisabilityOption, httpContextAccessor);
        SubServiceRepository = new SubServiceRepository(configuration, mapper, _loggerSubService, httpContextAccessor);
        ActivityPrivateParticipantRepository = new ActivityPrivateParticipantRepository(configuration, mapper, _loggerActivityPrivateParticipant, httpContextAccessor);
        ActivitySeasonRepository = new ActivitySeasonRepository(configuration, mapper, _loggerActivitySeason, httpContextAccessor);
        ActivityTransportationRepository = new ActivityTransportationRepository(configuration, mapper, _loggerActivityTransportation, httpContextAccessor);
        ActivityAddressRepository = new GenericAddressRepository(configuration, mapper, _loggerActivityAddress, httpContextAccessor);
        ActivityRepository = new ActivityRepository(configuration, mapper, _loggerActivity, httpContextAccessor);
        SearchActivityDetailRepository = new SearchActivityDetailRepository(configuration, mapper, _loggerSearchActivityDetail, httpContextAccessor);
        BusinessProfileRepository = new BusinessProfileRepository(configuration, mapper, _loggerBusinessProfile, httpContextAccessor);
        ActivityScheduleRepository = new ActivityScheduleRepository(configuration, mapper, _loggerActivitySchedule, httpContextAccessor);
        ActivityDisabilityOptionRepository = new ActivityDisabilityOptionRepository(configuration, mapper, _loggerActivityDisabilityOption, httpContextAccessor);
        ActivityImageMappingRepository = new ActivityImageMappingRepository(configuration, mapper, _loggerActivityImageMapping, httpContextAccessor);
        ActivitySeasonMappingRepository = new ActivitySeasonMappingRepository(configuration, mapper, _loggerActivitySeasonMapping, httpContextAccessor);
        ActivityGroupRepository = new ActivityGroupRepository(configuration, mapper, _loggerActivityGroup, httpContextAccessor);
        CurrencyRepository = new CurrencyRepository(configuration, mapper, _loggerCurrency, httpContextAccessor);
        ActivityDisabilityMappingRepository = new ActivityDisabilityMappingRepository(configuration, mapper, _loggerActivityDisabilityMapping, httpContextAccessor);
        ActivityPricePerParticipantRepository = new ActivityPricePerParticipantRepository(configuration, mapper, _loggerActivityPricePerParticipant, httpContextAccessor);
        ActivityPerGroupPriceRepository = new ActivityPerGroupPriceRepository(configuration, mapper, _loggerActivityPerGroupPrice, httpContextAccessor);
        ActivityIncludeOptionMappingRepository = new ActivityIncludeOptionMappingRepository(configuration, mapper, _loggerActivityIncludeOptionMapping, httpContextAccessor);
        ActivityAddressMappingRepository = new ActivityAddressMappingRepository(configuration, mapper, _loggerActivityAddressMapping, httpContextAccessor);
        ActivityIDImageMappingRepository = new ActivityIDImageMappingRepository(configuration, mapper, _loggerActivityIDImageMapping, httpContextAccessor);
		ProcessOrderRepository = new ProcessOrderRepository(configuration, mapper, _loggerProcessOrder, httpContextAccessor,emailService);
        PostPaymentStatusRepository = new PostPaymentStatusRepository(configuration, mapper, _loggerPostPaymentStatus, httpContextAccessor,emailService);
        PackageTypeRepository = new PackageTypeRepository(configuration, mapper, _loggerPackageType, httpContextAccessor);
        PackageDetailRepository = new PackageDetailRepository(configuration, mapper, _loggerPackageDetail, httpContextAccessor);
        FAQsRepository = new FAQsRepository(configuration, mapper, _loggerFAQs, httpContextAccessor);

        LanguageRepository = new LanguageRepository(configuration, mapper, _loggerLanguage, httpContextAccessor);
        RatePlanTypeRepository = new RatePlanTypeRepository(configuration, mapper, _loggerRatePlanType, httpContextAccessor);
        RatePlanRepository = new RatePlanRepository(configuration, mapper, _loggerRatePlan, httpContextAccessor);
        RoomViewRepository = new RoomViewRepository(configuration, mapper, _loggerRoomView, httpContextAccessor);
        ManufacturerRepository = new ManufacturerRepository(configuration, mapper, _loggerManufacturer, httpContextAccessor);
        FilterCategoryRepository = new FilterCategoryRepository(configuration, mapper, _loggerFilterCategory, httpContextAccessor);
        LastMinuteDealRepository = new LastMinuteDealRepository(configuration, mapper, _loggerLastMinuteDeal, httpContextAccessor);
        WishListRepository = new WishListRepository(configuration, mapper, _loggerWishList, httpContextAccessor);
        WishListNameRepository = new WishListNameRepository(configuration, mapper, _loggerWishListName, httpContextAccessor);
        OutDoorRepository = new OutDoorRepository(configuration, mapper, _loggerOutDoor, httpContextAccessor);
        SearchFilterStayRepository = new SearchFilterStayRepository(configuration, mapper, _loggerSearchFilterStay, httpContextAccessor);
        SearchFilterThingsToDoRepository = new SearchFilterThingsToDoRepository(configuration, mapper, _loggerSearchFilterThingsToDo, httpContextAccessor);
        CarRentalSearchFilterRepository = new CarRentalSearchFilterRepository(configuration, mapper, _loggerCarRentalSearchFilter, httpContextAccessor);
        KBDetailRepository = new KBDetailRepository(configuration, mapper, _loggerKBDetail, httpContextAccessor);
        KBRelatedUrlLinkRepository = new KBRelatedUrlLinkRepository(configuration, mapper, _loggerKBRelatedUrlLink, httpContextAccessor);
        KBDescriptionRepository = new KBDescriptionRepository(configuration, mapper, _loggerKBDescription, httpContextAccessor);
        KBMediaRepository = new KBMediaRepository(configuration, mapper, _loggerKBMedia, httpContextAccessor);
        KBTimingRepository = new KBTimingRepository(configuration, mapper, _loggerKBTiming, httpContextAccessor);
        CoreAreaRepository = new CoreAreaRepository(configuration, mapper, _loggerCoreArea, httpContextAccessor);
        ContactFormRepository = new ContactFormRepository(configuration, mapper, _loggerContactForm, httpContextAccessor,emailService);
        KBAddressRepository = new KBAddressRepository(configuration, mapper, _loggerKBAddress, httpContextAccessor);
        KBWhenToVisitRepository = new KBWhenToVisitRepository(configuration, mapper, _loggerKBWhenToVisit, httpContextAccessor);
        KBInterestedRepository = new KBInterestedRepository(configuration, mapper, _loggerKBInterested, httpContextAccessor);
        UserExperienceRepository = new UserExperienceRepository(configuration, mapper, _loggerUserExperience, httpContextAccessor);
        CheckProfileStatusRepository = new CheckProfileStatusRepository(configuration, mapper, _loggerCheckProfileStatus, httpContextAccessor);
        PopularItemsVisitRepository = new PopularItemsVisitRepository(configuration, mapper, _loggerPopularItemsVisit, httpContextAccessor);
        CustomerReviewRepository = new CustomerReviewRepository(configuration, mapper, _loggerCustomerReview, httpContextAccessor);
        CustomerAwarenessRepository = new CustomerAwarenessRepository(configuration, mapper, _loggerCustomerAwareness, httpContextAccessor);
        CampaignRepository = new CampaignRepository(configuration, mapper, _loggerCampaign, httpContextAccessor);
        CampaignTargetRepository = new CampaignTargetRepository(configuration, mapper, _loggerCampaignTarget, httpContextAccessor);
        CampaignTargetItemsRepository = new CampaignTargetItemsRepository(configuration, mapper, _loggerCampaignTargetItems, httpContextAccessor);

        GroupActivityParticipantsRepository = new GroupActivityParticipantsRepository(configuration, mapper, _loggerGroupActivityParticipants, httpContextAccessor);
        CampaignScheduleRepository = new CampaignScheduleRepository(configuration, mapper, _loggerCampaignSchedule, httpContextAccessor);
        HomeSliderRepository = new HomeSliderRepository(configuration, mapper, _loggerHomeSlider, httpContextAccessor);

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