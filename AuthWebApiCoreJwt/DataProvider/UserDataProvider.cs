using Dapper;
using AuthWebApiCoreJwt.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AuthWebApiCoreJwt.DataProvider
{
    public class UserDataProvider : IUserDataProvider
    {
        IConfiguration _configuration;

        //private readonly string connectionString = "Server=Ntulik;Database=UserDB;Trusted_Connection=True;";

        public UserDataProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ConnectionString()
        {

            return _configuration.GetConnectionString("MySqlDBConnectionString").ToString();
        }

        #region Users

        public async Task<IEnumerable<Users>> GetUsers()
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString()))
            {
                await sqlConnection.OpenAsync();
                return await sqlConnection.QueryAsync<Users>(
                    "GetUsers",
                    null,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Users> GetUser(string UserId)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString()))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@ID", UserId);
                return await sqlConnection.QuerySingleOrDefaultAsync<Users>(
                    "GetUser",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Users> GetUserByUsername(string username)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString()))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Username", username);
                return await sqlConnection.QuerySingleOrDefaultAsync<Users>(
                    "GetUserByUsername",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }


        public async Task AddUser(Users user)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString()))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                //dynamicParameters.Add("@FirstName", user.FirstName);
                //dynamicParameters.Add("@LastName", user.LastName);
                //dynamicParameters.Add("@Mobile", user.Mobile);
                dynamicParameters.Add("@Email", user.Email);
                dynamicParameters.Add("@Password", user.Password);

                await sqlConnection.ExecuteAsync(
                    "NewUser",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateUser(Users user)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString()))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                //dynamicParameters.Add("@UserName", user.Username);
                dynamicParameters.Add("@Email", user.Email);
                await sqlConnection.ExecuteAsync(
                    "UpdateUser",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateUserRefreshToken(Users user)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString()))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Id", user.Id);
                //dynamicParameters.Add("@RefreshToken", user.RefreshToken);
                await sqlConnection.ExecuteAsync(
                    "UpdateUserRefreshToken",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteUser(string UserId)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString()))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@ID", UserId);
                await sqlConnection.ExecuteAsync(
                    "DeleteUser",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        #endregion

        #region Roles

        //public async Task<IEnumerable<Roles>> GetRoles()
        //{
        //    using (var sqlConnection = new SqlConnection(ConnectionString()))
        //    {
        //        await sqlConnection.OpenAsync();
        //        return await sqlConnection.QueryAsync<Roles>(
        //            "spGetRoles",
        //            null,
        //            commandType: CommandType.StoredProcedure);
        //    }
        //}

        //public async Task<Roles> GetRole(int RoleId)
        //{
        //    using (var sqlConnection = new SqlConnection(ConnectionString()))
        //    {
        //        await sqlConnection.OpenAsync();
        //        var dynamicParameters = new DynamicParameters();
        //        dynamicParameters.Add("@RoleId", RoleId);
        //        return await sqlConnection.QuerySingleOrDefaultAsync<Roles>(
        //            "spGetRole",
        //            dynamicParameters,
        //            commandType: CommandType.StoredProcedure);
        //    }
        //}


        //public async Task AddRole(Roles role)
        //{
        //    using (var sqlConnection = new SqlConnection(ConnectionString()))
        //    {
        //        await sqlConnection.OpenAsync();
        //        var dynamicParameters = new DynamicParameters();
        //        dynamicParameters.Add("@RoleId", role.Id);
        //        dynamicParameters.Add("@Name", role.Name);

        //        await sqlConnection.ExecuteAsync(
        //            "spAddRole",
        //            dynamicParameters,
        //            commandType: CommandType.StoredProcedure);
        //    }
        //}

        //public async Task UpdateRole(Roles role)
        //{
        //    using (var sqlConnection = new SqlConnection(ConnectionString()))
        //    {
        //        await sqlConnection.OpenAsync();
        //        var dynamicParameters = new DynamicParameters();
        //        dynamicParameters.Add("@RoleId", role.Id);
        //        dynamicParameters.Add("@Name", role.Name);
        //        await sqlConnection.ExecuteAsync(
        //            "spUpdateRole",
        //            dynamicParameters,
        //            commandType: CommandType.StoredProcedure);
        //    }
        //}

        //public async Task DeleteRole(int RoleId)
        //{
        //    using (var sqlConnection = new SqlConnection(ConnectionString()))
        //    {
        //        await sqlConnection.OpenAsync();
        //        var dynamicParameters = new DynamicParameters();
        //        dynamicParameters.Add("@RoleId", RoleId);
        //        await sqlConnection.ExecuteAsync(
        //            "spDeleteRole",
        //            dynamicParameters,
        //            commandType: CommandType.StoredProcedure);
        //    }
        //}

        #endregion

        #region UserRoles

        //public async Task<IEnumerable<UserRoles>> GetUserRoles()
        //{
        //    using (var sqlConnection = new SqlConnection(ConnectionString()))
        //    {
        //        await sqlConnection.OpenAsync();
        //        return await sqlConnection.QueryAsync<UserRoles>(
        //            "GetUserRoles",
        //            null,
        //            commandType: CommandType.StoredProcedure);
        //    }
        //}

        //public async Task<UserRoles> GetUserRole(string UserId)
        //{
        //    using (var sqlConnection = new SqlConnection(ConnectionString()))
        //    {
        //        await sqlConnection.OpenAsync();
        //        var dynamicParameters = new DynamicParameters();
        //        dynamicParameters.Add("@UserID", UserId);
        //        return await sqlConnection.QuerySingleOrDefaultAsync<UserRoles>(
        //            "GetUserRole",
        //            dynamicParameters,
        //            commandType: CommandType.StoredProcedure);
        //    }
        //}


        //public async Task AddUserRole(UserRoles userRole)
        //{
        //    using (var sqlConnection = new SqlConnection(ConnectionString()))
        //    {
        //        await sqlConnection.OpenAsync();
        //        var dynamicParameters = new DynamicParameters();
        //        dynamicParameters.Add("@UserId", userRole.UserId);
        //        dynamicParameters.Add("@RoleId", userRole.RoleId);

        //        await sqlConnection.ExecuteAsync(
        //            "spAddUserRole",
        //            dynamicParameters,
        //            commandType: CommandType.StoredProcedure);
        //    }
        //}

        //public async Task UpdateUserRole(UserRoles userRole)
        //{
        //    using (var sqlConnection = new SqlConnection(ConnectionString()))
        //    {
        //        await sqlConnection.OpenAsync();
        //        var dynamicParameters = new DynamicParameters();
        //        dynamicParameters.Add("@RoleId", userRole.RoleId);
        //        dynamicParameters.Add("@UserId", userRole.UserId);
        //        await sqlConnection.ExecuteAsync(
        //            "spUpdateUserRole",
        //            dynamicParameters,
        //            commandType: CommandType.StoredProcedure);
        //    }
        //}

        //public async Task DeleteUserRole(int UserRoleId)
        //{
        //    using (var sqlConnection = new SqlConnection(ConnectionString()))
        //    {
        //        await sqlConnection.OpenAsync();
        //        var dynamicParameters = new DynamicParameters();
        //        dynamicParameters.Add("@UserRoleId", UserRoleId);
        //        await sqlConnection.ExecuteAsync(
        //            "spDeleteUserRole",
        //            dynamicParameters,
        //            commandType: CommandType.StoredProcedure);
        //    }
        //}

        #endregion

        //public async Task<bool> Handle(ExchangeRefreshTokenRequest message)
        //{
        //    var cp = _jwtTokenValidator.GetPrincipalFromToken(message.AccessToken, message.SigningKey);

        //    // invalid token/signing key was passed and we can't extract user claims
        //    if (cp != null)
        //    {
        //        var id = cp.Claims.First(c => c.Type == "id");

        //        // var user = await _userRepository.GetSingleBySpec(new UserSpecification(id.Value));

        //        var user = await GetUser(id);

        //        if (user.HasValidRefreshToken(message.RefreshToken))
        //        {
        //            var jwtToken = await _jwtFactory.GenerateEncodedToken(user.IdentityId, user.UserName);
        //            var refreshToken = _tokenFactory.GenerateToken();
        //            user.RemoveRefreshToken(message.RefreshToken); // delete the token we've exchanged
        //            user.AddRereshToken(refreshToken, user.Id, ""); // add the new one
        //            await _userRepository.Update(user);
        //            //outputPort.Handle(new ExchangeRefreshTokenResponse(jwtToken, refreshToken, true));
        //            return true;
        //        }
        //    }
        //    //outputPort.Handle(new ExchangeRefreshTokenResponse(false, "Invalid token."));
        //    return false;
        //}

        //public bool HasValidRefreshToken(string refreshToken)
        //{
        //    return _refreshTokens.Any(rt => rt.Token == refreshToken && rt.Active);
        //}


    }
}
