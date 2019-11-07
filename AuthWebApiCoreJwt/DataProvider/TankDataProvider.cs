using AuthWebApiCoreJwt.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AuthWebApiCoreJwt.DataProvider
{
    public class TankDataProvider : ITankDataProvider
    {
        IConfiguration _configuration;

        //private readonly string connectionString = "Server=Ntulik;Database=UserDB;Trusted_Connection=True;";

        public TankDataProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ConnectionString()
        {

            return _configuration.GetConnectionString("MySqlDBConnectionString").ToString();
        }

        public async Task<IEnumerable<Tanks>> GetSitesByID(int ID)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString()))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@userID", ID);
                return await sqlConnection.QueryAsync<Tanks>(
                    "sp_UserSites",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Users>> GetUserByEmailandPassword(string Email, string Password)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString()))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@email", Email);
                dynamicParameters.Add("@passw", Password);
                return await sqlConnection.QueryAsync<Users>(
                    "sp_UserAuth",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Tanks>> GetTanks()
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString()))
            {
                await sqlConnection.OpenAsync();
                return await sqlConnection.QueryAsync<Tanks>(
                    "sp_Tanks",
                    null,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Tanks>> GetTankByAny(string anyVariable)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString()))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@AnyVariable", anyVariable);
                return await sqlConnection.QueryAsync<Tanks>(
                    "sp_TankByAnyVariable",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Tanks>> GetTankByID(int ID)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString()))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@TankID", ID);
                return await sqlConnection.QueryAsync<Tanks>(
                    "sp_TankByID",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Tanks>> GetTankByID2(int ID, int intervalnum, string intervaltype)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString()))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@TankID", ID);
                dynamicParameters.Add("@IntervalNum", intervalnum);
                dynamicParameters.Add("@IntervalPeriod", intervaltype);
                return await sqlConnection.QueryAsync<Tanks>(
                    "sp_TankByID2",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Tanks>> GetTankByIDandDate(int ID, DateTime dateform, DateTime dateto)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString()))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@TankID", ID);
                dynamicParameters.Add("@DateFrom", dateform);
                dynamicParameters.Add("@DateTo", dateto);
                return await sqlConnection.QueryAsync<Tanks>(
                    "sp_TankByIDandPeriod",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Tanks>> GetTankByIDandDateTime(int ID, DateTime dateform, DateTime dateto, DateTime timeform, DateTime timeto)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString()))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@TankID", ID);
                dynamicParameters.Add("@DateFrom", dateform);
                dynamicParameters.Add("@DateTo", dateto);
                dynamicParameters.Add("@TimeFrom", timeform);
                dynamicParameters.Add("@TimeTo", timeto);
                return await sqlConnection.QueryAsync<Tanks>(
                    "sp_TankByIDandDateTime",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
