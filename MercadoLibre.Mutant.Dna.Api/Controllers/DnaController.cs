using MercadoLibre.Mutant.Dna.Api.Requests;
using MercadoLibre.Mutant.Dna.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MercadoLibre.Mutant.Dna.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class DnaController : ControllerBase
    {
        private readonly IDnaServiceCore _dnaServiceCore;

        public DnaController(IDnaServiceCore dnaServiceCore)
        {
            _dnaServiceCore = dnaServiceCore;
        }

        [HttpPost]
        [Route("mutant")]
        public async Task<IActionResult> CheckMutation(IsMutant isMutant)
        {
            bool result = await _dnaServiceCore.IsMutant(isMutant.dna);
            return result ? StatusCode(200) : StatusCode(403);
        }

        [HttpGet]
        [Route("stats")]
        public async Task<IActionResult> GetStats()
        {
            return Ok(await _dnaServiceCore.GetStats());
        }
    }
}
