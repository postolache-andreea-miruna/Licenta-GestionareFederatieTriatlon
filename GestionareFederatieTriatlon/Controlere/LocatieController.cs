using GestionareFederatieTriatlon.Manageri;
using GestionareFederatieTriatlon.Modele;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionareFederatieTriatlon.Controlere
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocatieController : ControllerBase
    {
        private readonly ILocatieManager manager;

        public LocatieController(ILocatieManager manager)
        {
            this.manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetLocatie()
        {
            var listaLocatii = manager.GetLocatie();
            return Ok(listaLocatii);
        }

        [HttpGet("byIdComp/{id}")]
        public async Task<IActionResult> GetLocatieByIdComp([FromRoute] int id)
        {
            var locatie = manager.GetLocatieGivenComp(id);
            return Ok(locatie);
        }

        [HttpGet("byId/{id}")]
        public async Task<IActionResult> GetLocatieById([FromRoute] int id)
        {
            var locatie = manager.GetLocatieInfo(id);
            return Ok(locatie);
        }

        [HttpPut]
        [Authorize(Policy = "AdminUtilizator")]
        public async Task<IActionResult> Update([FromBody] LocatieModel locatieModel)
        {
            manager.Update(locatieModel);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminUtilizator")]
        public async Task<IActionResult> DeleteLocatie([FromRoute] int id)
        {
            manager.Delete(id);
            return Ok();
        }

        [HttpPost]
        [Authorize(Policy = "AdminUtilizator")]
        public async Task<IActionResult> Create([FromBody] LocatieModelById model)
        {
            manager.Create(model);
            return Ok();
        }
    }
}
