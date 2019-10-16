using AuthWebApiCoreJwt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthWebApiCoreJwt.DataProvider
{
    public interface IUserDataProvider
    {
        #region Users
        Task<IEnumerable<Users>> GetUsers();

        Task<Users> GetUser(string UserId);

        Task<Users> GetUserByUsername(string Username);

        Task AddUser(Users item);

        Task UpdateUser(Users item);

        Task UpdateUserRefreshToken(Users item);

        Task DeleteUser(string UserId);

        #endregion

        #region Roles
        //Task<IEnumerable<Roles>> GetRoles();

        //Task<Roles> GetRole(int RoleId);

        //Task AddRole(Roles item);

        //Task UpdateRole(Roles item);

        //Task DeleteRole(int RoleId);

        #endregion

        #region UserRoles
        //Task<IEnumerable<UserRoles>> GetUserRoles();

        //Task<UserRoles> GetUserRole(string UserId);

        //Task AddUserRole(UserRoles item);

        //Task UpdateUserRole(UserRoles item);

        //Task DeleteUserRole(int RoleId);

        #endregion
    }
}
