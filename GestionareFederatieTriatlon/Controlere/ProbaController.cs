using GestionareFederatieTriatlon.Manageri;
using GestionareFederatieTriatlon.Modele;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionareFederatieTriatlon.Controlere
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProbaController : ControllerBase
    {
        private readonly IProbaManager manager;
        public ProbaController(IProbaManager manager)
        {
            this.manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetProbe()
        {
            var listaProbe = manager.GetProbe();
            return Ok(listaProbe);
        }

        [HttpGet("probe")]
        [Authorize(Policy = "AdminUtilizator")]
        public async Task<IActionResult> GetProbeTotal()
        {
            var listaProbe = manager.GetProbeTotal();
            return Ok(listaProbe);
        }

        [HttpGet("byId/{id}")]
        public async Task<IActionResult> GetProbeById([FromRoute] int id)
        {
            var proba = manager.GetProbaInfo(id);
            return Ok(proba);
        }

        [HttpGet("probeComp/{id}")]
        public async Task<IActionResult> GetProbeComp([FromRoute] int id)
        {
            var proba = manager.GetProbaCompetitie(id);
            return Ok(proba);
        }

        [HttpGet("nrParticipantiProbeComp/{id}")]
        public async Task<IActionResult> GetNrPartProbeComp([FromRoute] int id)
        {
            var proba = manager.GetNrParticipProbeComp(id);
            return Ok(proba);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProba([FromRoute]int id)
        {
            manager.Delete(id);
            return Ok();
        }

        [HttpPost]
        [Authorize(Policy = "AdminUtilizator")]
        public async Task<IActionResult> Create([FromBody] ProbaModelById model)
        {
            manager.Create(model);
            return Ok();
        }

        [HttpPut]
        [Authorize(Policy = "AdminUtilizator")]
        public async Task<IActionResult> Update([FromBody] ProbaUpdateModel updateModel)
        {
            manager.Update(updateModel);
            return Ok();
        }


    }
}
