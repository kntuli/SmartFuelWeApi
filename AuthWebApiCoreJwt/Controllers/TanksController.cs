using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthWebApiCoreJwt.DataProvider;
using AuthWebApiCoreJwt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthWebApiCoreJwt.Controllers
{
    [Route("api")]
    [ApiController]
    [Produces("application/json")]
    public class TanksController : ControllerBase
    {
        private readonly ITankDataProvider tankDataProvider;
        public TanksController(ITankDataProvider tankDataProvider)
        {
            this.tankDataProvider = tankDataProvider;
        }

        [HttpGet]
        [Route("tanks")]
        public async Task<IEnumerable<Tanks>> GetTanks()
        {
            return await tankDataProvider.GetTanks();
        }

        // GET api/values/5
        [HttpGet]
        [Route("tanks/{anyVar}")]
        public async Task<IEnumerable<Tanks>> GetTanksByAny(string anyVar)
        {
            return await tankDataProvider.GetTankByAny(anyVar);
        }

        [HttpGet]
        [Route("tanksbyid/{id}")]
        public async Task<IEnumerable<Tanks>> GetTanksByID(int id)
        {
            return await tankDataProvider.GetTankByID(id);
        }

        [HttpGet]
        [Route("tanksbyid/{id}/{intervalnum}/{intervaltype}")]
        public async Task<IEnumerable<Tanks>> GetTanksByID(int id, int intervalnum, string intervaltype)
        {
            return await tankDataProvider.GetTankByID2(id, intervalnum, intervaltype);
        }

        [HttpGet]
        [Route("tanksbyidanddate/{id}/{datefrom}/{dateto}")]
        public async Task<IEnumerable<Tanks>> GetTanksByIDandDate(int id, DateTime datefrom, DateTime dateto)
        {
            return await tankDataProvider.GetTankByIDandDate(id, datefrom, dateto);
        }

        [HttpGet]
        [Route("tanksbyidanddatetime/{id}/{datefrom}/{dateto}/{timefrom}/{timeto}")]
        public async Task<IEnumerable<Tanks>> GetTanksByIDandDate(int id, DateTime datefrom, DateTime dateto, DateTime timefrom, DateTime timeto)
        {
            return await tankDataProvider.GetTankByIDandDateTime(id, datefrom, dateto, timefrom, timeto);
        }
    }
}