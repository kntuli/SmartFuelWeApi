using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AuthWebApiCoreJwt.DataProvider;
using AuthWebApiCoreJwt.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace AuthWebApiCoreJwt.Controllers
{
    [Route("api")]
    [ApiController]
    [Produces("application/json")]
    public class ValuesController : ControllerBase
    {
        private readonly IUserDataProvider userDataProvider;
        private readonly ITankDataProvider tankDataProvider;
        public ValuesController(IUserDataProvider userDataProvider, ITankDataProvider tankDataProvider)
        {
            this.userDataProvider = userDataProvider;
            this.tankDataProvider = tankDataProvider;
        }

        // GET api/values
        [HttpGet]
        [Route("values")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //public ActionResult<IEnumerable<string>> GetAll()
        //{
        //    Users x = userDataProvider.GetUsers().Result;
        //    return x.ToList();
        //}

        //[HttpGet]
        //public async Task<IEnumerable<Users>> Get()
        //{
        //    using (var sqlConnection = new MySqlConnection(ConnectionString()))
        //    {
        //        await sqlConnection.OpenAsync();
        //        return await sqlConnection.QueryAsync<Users>(
        //            "GetUsers",
        //            null,
        //            commandType: CommandType.StoredProcedure);
        //    }
        //}

        [HttpGet]
        [Route("users")]
        public async Task<IEnumerable<Users>> Get2()
        {
            return await userDataProvider.GetUsers();
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

        //[HttpGet]
        //[Route("users")]
        //public async Task<IEnumerable<Users>> Get2()
        //{
        //    using (var sqlConnection = new MySqlConnection(ConnectionString()))
        //    {
        //        await sqlConnection.OpenAsync();
        //        return await sqlConnection.QueryAsync<Users>(
        //            "GetUsers",
        //            null,
        //            commandType: CommandType.StoredProcedure);
        //    }
        //}


        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
