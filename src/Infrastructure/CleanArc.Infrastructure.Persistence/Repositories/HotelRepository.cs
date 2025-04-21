using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.AgeType;
using CleanArc.Application.Models.Hotel;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Entities.AgeType;
using CleanArc.Domain.Entities.Hotel;
using CleanArc.Infrastructure.Persistence.Helpers;
using CleanArc.Infrastructure.Sql.SqlQueries;
using CleanArc.SharedKernel.Extensions;
using Dapper;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging; 
using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArc.Application.Common;
using CleanArc.Domain.Entities.KBDetail;
using CleanArc.Application.Models.KBDescription;
using CleanArc.Domain.Entities.KBDescription;
using CleanArc.Domain.Entities.Language;
using CleanArc.Domain.Entities.Amenity;

namespace CleanArc.Infrastructure.Persistence.Repositories;

public class HotelRepository : IHotelRepository
{
    /// <summary>
    /// The configuration for accessing application settings.
    /// </summary>
    private readonly IConfiguration configuration;

    /// <summary>
    /// The mapper for mapping between different object types.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// The logger for logging repository-related information.
    /// </summary>
    private readonly ILogger<HotelRepository> _logger;

    /// <summary>
    /// The HTTP context accessor for accessing HTTP context information.
    /// </summary>
    private readonly IHttpContextAccessor _httpContextAccessor;
    public HotelRepository(IConfiguration configuration, IMapper mapper, ILogger<HotelRepository> logger, IHttpContextAccessor httpContextAccessor)
    {
        this.configuration = configuration;
        this._mapper = mapper;
        this._logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ResponseEntity> AddAsync(Hotel hotel)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, hotel))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                CreateHotelDTO createHotelDTO = _mapper.Map<CreateHotelDTO>(hotel);
                var addressTable = new DataTable();
                addressTable.Columns.Add("AddressLine1", typeof(string));
                addressTable.Columns.Add("AddressLine2", typeof(string));
                addressTable.Columns.Add("CountryLookUpId", typeof(int));
                addressTable.Columns.Add("CityLookUpId", typeof(int));
                addressTable.Columns.Add("StateLookUpId", typeof(int));
                addressTable.Columns.Add("PostalCode", typeof(string));
                addressTable.Columns.Add("Latitude", typeof(string));
                addressTable.Columns.Add("Longitude", typeof(string));


                addressTable.Rows.Add(hotel.AddressLine1,
                    hotel.AddressLine2,
                    hotel.CountryLookUpId,
                    hotel.CityLookUpId,
                    hotel.StateLookUpId,
                    hotel.PostalCode.ToString(),
                    hotel.Latitude,
                    hotel.Longitude);
                var parameters = new DynamicParameters(createHotelDTO);
                var languageTable = new DataTable();
                languageTable.Columns.Add("LanguageTypeLookUpId", typeof(int));

                foreach (var language in hotel.LanguageLookUpId)
                    languageTable.Rows.Add(language);
                parameters.Add("@Language", languageTable.AsTableValuedParameter("LanguageTableType"));


                parameters.Add("@Address", addressTable.AsTableValuedParameter("GenericAddressTableType"));
                

                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(HotelQueries.Create_Hotel, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
                
                return result;
            }
        }
    }


    public async Task<ResponseEntity> DeleteAsync(DeleteRequest deleteRequest, int? updatedBy)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, deleteRequest))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@Ids", deleteRequest.SelectedIds);
                parameters.Add("@UpdatedBy", updatedBy);
				parameters.Add("@CultureId", deleteRequest.CultureId);
                parameters.Add("@IsDeleted", deleteRequest.isDeleted);

                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(HotelQueries.Delete_Hotel, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result); 
                return result;
            }
        }
    }


    public async Task<ListResponseWrapper<Hotel>> GetAllAsync(SearchRequest searchRequest)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequest))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
				var parameters = new DynamicParameters();
				parameters.Add("@PageNumber", searchRequest.PageNumber, DbType.Int32);
				parameters.Add("@PageSize", searchRequest.PageSize, DbType.Int32);
				parameters.Add("@cultureId", searchRequest.CultureId, DbType.Int32);
				parameters.Add("@SortingArray", DataTableHelper.ToDataTable(searchRequest.SortingArray), DbType.Object); // Ensure proper type
				parameters.Add("@FilterArray", DataTableHelper.ToDataTable(searchRequest.FilterArray), DbType.Object); // Ensure proper type
				parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
				parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
				var result = await connection.QueryAsync<Hotel>(HotelQueries.GetALL_Hotel, parameters, commandType: CommandType.StoredProcedure);


				(logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
				var response = new ListResponseWrapper<Hotel> { Data = result.ToList(), Code = parameters.Get<int>("@Code"), Message = parameters.Get<string>("@Message") }; return response;
			}
        }
    }


    public async Task<SingleResponseWrapper<Hotel>> GetByIdAsync(SearchRequestById searchRequestById)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, searchRequestById))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
				var parameters = new DynamicParameters();
				parameters.Add("@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
				parameters.Add("@Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
				parameters.Add("@CultureId", searchRequestById.CultureId, DbType.Int32);
				parameters.Add("@ID", searchRequestById.Id, DbType.Int32);
				var result = await connection.QueryMultipleAsync(HotelQueries.GetByID_Hotel, parameters, commandType: CommandType.StoredProcedure);
                var hotel = result.Read<Hotel>().FirstOrDefault();
                if (hotel != null)
                {
                    var language = result.Read<LanguageLookUp>().ToList();
                    hotel.Languages = language;

                    var amenities = result.Read<AmenityLookUp>().ToList();
                    hotel.Amenities = amenities;
                }
                if (!result.IsConsumed)
                {
                    result.Dispose();
                }
                (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                var response = new SingleResponseWrapper<Hotel>
                {
                    Data = hotel,
                    Code = parameters.Get<int>("@Code"),
                    Message = parameters.Get<string>("@Message")
                };
                return response;
            }
        }
    }


    public async Task<ResponseEntity> UpdateAsync(Hotel hotel)
    {
        using (var logger = _logger.LogMethodEntryExit(_httpContextAccessor?.HttpContext, hotel))
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection1")))
            {
                connection.Open();
                UpdateHotelDTO updateHotelDTO = _mapper.Map<UpdateHotelDTO>(hotel);
                //CreateHotelDTO createHotelDTO = _mapper.Map<CreateHotelDTO>(hotel);
                var addressTable = new DataTable();
                addressTable.Columns.Add("AddressLine1", typeof(string));
                addressTable.Columns.Add("AddressLine2", typeof(string));
                addressTable.Columns.Add("CountryLookUpId", typeof(int));
                addressTable.Columns.Add("CityLookUpId", typeof(int));
                addressTable.Columns.Add("StateLookUpId", typeof(int));
                addressTable.Columns.Add("PostalCode", typeof(string));
                addressTable.Columns.Add("Latitude", typeof(string));
                addressTable.Columns.Add("Longitude", typeof(string));

                addressTable.Rows.Add(hotel.AddressLine1,
                    hotel.AddressLine2,
                    hotel.CountryLookUpId,
                    hotel.CityLookUpId,
                    hotel.StateLookUpId,
                    hotel.PostalCode.ToString(),
                    hotel.Latitude,
                    hotel.Longitude);

                var languageTable = new DataTable();
                languageTable.Columns.Add("LanguageTypeLookUpId", typeof(int));

                foreach (var language in hotel.LanguageLookUpId)
                    languageTable.Rows.Add(language);

                var parameters = new DynamicParameters(updateHotelDTO);
                parameters.Add("@Address", addressTable.AsTableValuedParameter("GenericAddressTableType"));
                parameters.Add("@Language", languageTable.AsTableValuedParameter("LanguageTableType"));

                var result = await connection.QueryFirstOrDefaultAsync<ResponseEntity>(HotelQueries.Update_Hotel, parameters, commandType: CommandType.StoredProcedure);
                 (logger as LoggingExtensions.MethodEntryExitLogger)?.SetResponse(result);
                return result;
            }
        }
    }

}
