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
    Task CommitAsync();
    ValueTask RollBackAsync();
}