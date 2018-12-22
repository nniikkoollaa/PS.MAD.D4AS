using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using System;
using System.Threading.Tasks;

namespace PS.MAD.D4AS.GuidProvider.State.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuidController : ControllerBase
    {
        private IReliableStateManager _stateManager;
        private readonly string Guids = "guids";

        public GuidController(IReliableStateManager stateManager)
        {
            _stateManager = stateManager;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var newGuid = Guid.NewGuid();

            IReliableDictionary<Guid, Guid> generatedGuids = await _stateManager.GetOrAddAsync<IReliableDictionary<Guid, Guid>>(Guids);
            using (ITransaction tx = _stateManager.CreateTransaction())
            {
                while(await generatedGuids.ContainsKeyAsync(tx, newGuid))
                {
                    newGuid = Guid.NewGuid();
                }

                await generatedGuids.AddAsync(tx, newGuid, newGuid);
                await tx.CommitAsync();
            }


            return Ok(newGuid);
        }
    }
}