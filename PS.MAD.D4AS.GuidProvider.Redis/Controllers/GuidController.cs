using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace PS.MAD.D4AS.GuidProvider.Redis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuidController : ControllerBase
    {
        private readonly string Guids = "guids";

        [HttpGet]
        public ActionResult Get()
        {
            var result = Guid.NewGuid();

            var redisHostName = "hostname";
            var redisKey = "redisKey";
            var redisConnection = $"{redisHostName},abortConnection=false,ssl=true,password={redisKey}";

            var lazyConnection = new Lazy<ConnectionMultiplexer>(()=> {
                return ConnectionMultiplexer.Connect(redisConnection);
            });

            IDatabase cache = lazyConnection.Value.GetDatabase();

            IDictionary<Guid, Guid> generatedGuid = Newtonsoft.Json.JsonConvert.DeserializeObject<IDictionary<Guid, Guid>>(
                cache.StringGet(this.Guids));

            // update dictionary

            cache.StringSet(this.Guids, Newtonsoft.Json.JsonConvert.SerializeObject(generatedGuid));

            return Ok(result);
        }
    }
}