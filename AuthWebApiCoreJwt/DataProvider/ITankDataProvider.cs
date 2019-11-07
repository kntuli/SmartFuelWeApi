using AuthWebApiCoreJwt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthWebApiCoreJwt.DataProvider
{
    public interface ITankDataProvider
    {
        Task<IEnumerable<Users>> GetUserByEmailandPassword(string email, string password);
        Task<IEnumerable<Tanks>> GetSitesByID(int ID);
        Task<IEnumerable<Tanks>> GetTanks();
        Task<IEnumerable<Tanks>> GetTankByAny(string anyVar);

        Task<IEnumerable<Tanks>> GetTankByID(int ID);

        Task<IEnumerable<Tanks>> GetTankByID2(int ID, int intervalnum, string intervaltype);
        Task<IEnumerable<Tanks>> GetTankByIDandDate(int ID, DateTime datefrom, DateTime dateto);
        Task<IEnumerable<Tanks>> GetTankByIDandDateTime(int ID, DateTime datefrom, DateTime dateto, DateTime timefrom, DateTime timeto);
    }
}
