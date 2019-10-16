using AuthWebApiCoreJwt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthWebApiCoreJwt.DataProvider
{
    public interface ITankDataProvider
    {
        Task<IEnumerable<Tanks>> GetTanks();
        Task<IEnumerable<Tanks>> GetTankByAny(string anyVar);

        Task<IEnumerable<Tanks>> GetTankByID(int ID);

        Task<IEnumerable<Tanks>> GetTankByID2(int ID, int intervalnum, string intervaltype);
    }
}
