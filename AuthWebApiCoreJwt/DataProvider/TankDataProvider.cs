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
    }
}
