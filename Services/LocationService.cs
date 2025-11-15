using System;
using System.Data;
using System.Data.SqlClient;

namespace BBQandGrill.Services
{
    /// <summary>
    /// Service for location-related business logic
    /// </summary>
    public class LocationService
    {
        private readonly DatabaseService _databaseService;

        public LocationService()
        {
            _databaseService = new DatabaseService();
        }

        /// <summary>
        /// Searches for locations by zip code, city, or state
        /// </summary>
        public LocationSearchResult SearchLocations(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return LocationSearchResult.Error("Please enter either a valid zip code, a city, or a state");
            }

            try
            {
                DataSet results = PerformSearch(searchText.Trim());

                if (results.Tables.Count == 0 || results.Tables[0].Rows.Count == 0)
                {
                    return LocationSearchResult.NotFound("Sorry, we did not find any location close to your area.");
                }

                return LocationSearchResult.Success(results);
            }
            catch (ArgumentOutOfRangeException)
            {
                return LocationSearchResult.Error("Please enter either a valid zip, state or city");
            }
            catch (Exception ex)
            {
                // Log exception here (implement in Phase 4)
                return LocationSearchResult.Error("We were unable to check our repository. Please try again later.");
            }
        }

        private DataSet PerformSearch(string searchText)
        {
            int zip;
            bool isZipCode = int.TryParse(searchText, out zip);

            if (isZipCode && searchText.Length >= 3)
            {
                // Search by zip code (first 3 digits for area matching)
                return SearchByZipCode(searchText.Substring(0, 3));
            }
            else
            {
                // Search by city or state name
                return SearchByCityOrState(searchText);
            }
        }

        private DataSet SearchByZipCode(string zipPrefix)
        {
            SqlParameter param = new SqlParameter("@zipText", zipPrefix);
            return _databaseService.ExecuteStoredProcedure("Get_Location", param);
        }

        private DataSet SearchByCityOrState(string cityOrState)
        {
            SqlParameter param = new SqlParameter("@zipText", cityOrState);
            return _databaseService.ExecuteStoredProcedure("Get_Location_By_City_State", param);
        }

        public void Dispose()
        {
            _databaseService?.Dispose();
        }
    }

    /// <summary>
    /// Result object for location search operations
    /// </summary>
    public class LocationSearchResult
    {
        public bool IsSuccess { get; private set; }
        public bool IsNotFound { get; private set; }
        public string Message { get; private set; }
        public DataSet Data { get; private set; }

        private LocationSearchResult(bool isSuccess, bool isNotFound, string message, DataSet data)
        {
            IsSuccess = isSuccess;
            IsNotFound = isNotFound;
            Message = message;
            Data = data;
        }

        public static LocationSearchResult Success(DataSet data) 
            => new LocationSearchResult(true, false, string.Empty, data);

        public static LocationSearchResult NotFound(string message) 
            => new LocationSearchResult(false, true, message, null);

        public static LocationSearchResult Error(string message) 
            => new LocationSearchResult(false, false, message, null);
    }
}
