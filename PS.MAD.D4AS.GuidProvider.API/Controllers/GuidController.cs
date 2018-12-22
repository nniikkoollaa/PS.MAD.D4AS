using System;
using System.Collections.Generic;
using System.Fabric;
using System.Fabric.Query;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PS.MAD.D4AS.GuidProvider.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuidController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly FabricClient _fabricClient;
        private readonly StatelessServiceContext _serviceContext;

        public GuidController(
            HttpClient httpClient,
            FabricClient fabricClient,
            StatelessServiceContext serviceContext)
        {
            this._httpClient = httpClient;
            this._fabricClient = fabricClient;
            this._serviceContext = serviceContext;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            Uri stateServiceName = API.GetStateServiceName(this._serviceContext);
            Uri proxyAddress = this.GetProxyAddress(stateServiceName);

            ServicePartitionList partitions = await this._fabricClient.QueryManager.GetPartitionListAsync(stateServiceName);
            Guid result = Guid.Empty;

            foreach(var partition in partitions)
            {
                var partitionKey = ((Int64RangePartitionInformation)partition.PartitionInformation).LowKey;
                string proxyUrl = $"{proxyAddress}/api/Guid?PartitionKey={partitionKey}&PartitionKind=Int64Range";
                using(HttpResponseMessage response = await this._httpClient.GetAsync(proxyUrl))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        continue;
                    }

                    result = Guid.Parse(await response.Content.ReadAsStringAsync());
                }
            }

            return Ok(result);
        }

        private Uri GetProxyAddress(Uri serviceName)
        {
            return new Uri($"http://localhost:19081{serviceName.AbsolutePath}");
        }
    }
}