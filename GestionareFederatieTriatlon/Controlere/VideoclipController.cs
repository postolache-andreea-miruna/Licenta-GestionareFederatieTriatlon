using GestionareFederatieTriatlon.Manageri;
using GestionareFederatieTriatlon.Modele;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionareFederatieTriatlon.Controlere
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoclipController : ControllerBase
    {
        private readonly IVideoclipManager manager;

        public VideoclipController(IVideoclipManager manager)
        {
            this.manager = manager;
        }

        [HttpGet("videouri")]
        [Authorize(Policy = "AdminUtilizator")]
        public async Task<IActionResult> GetVideouri()
        {
            var listaVideouri = manager.GetVideouri();
            return Ok(listaVideouri);
        }
        [HttpGet("codVideo/{codComepetitie}")]
        public async Task<IActionResult> GeCodVideoIdCompetitie(int codComepetitie)
       {
            var cod = manager.GetCodVideoCompetitie(codComepetitie);
            return Ok(cod);
        }

        [HttpPost]
        [Authorize(Policy = "AdminUtilizator")]
        public async Task<IActionResult> Create([FromBody] VideoModelCreate model)
        {
            manager.Create(model);
            return Ok();
        }

        [HttpPut]
        [Authorize(Policy = "AdminUtilizator")]
        public async Task<IActionResult> Update([FromBody] VideoModelUpdate model)
        {
            manager.Update(model);
            return Ok();
        }
    }
}
