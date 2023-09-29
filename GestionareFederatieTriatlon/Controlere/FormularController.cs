using GestionareFederatieTriatlon.Manageri;
using GestionareFederatieTriatlon.Modele;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionareFederatieTriatlon.Controlere
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormularController : ControllerBase
    {
        private readonly IFormularManager manager;

        public FormularController(IFormularManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Authorize(Policy = "SportivUtilizator")]
        public async Task<IActionResult> Create([FromBody] FormularCreateModel model)
        {
            manager.Create(model);
            return Ok();
        }

        [HttpGet("form/{id}")]
        [Authorize(Policy = "SportivUtilizator")]

        public async Task<IActionResult> GetFormInfoById([FromRoute] int id)
        {
            var form = manager.GetFormInfo(id);
            return Ok(form);
        }

        [HttpGet("formulare")]
        [Authorize(Policy = "AdminUtilizator")]
        public async Task<IActionResult> GetAllFormulare(string an="toti anii")
        {
            var formulare = manager.GetAllFormualre(an);
            return Ok(formulare);
        }

        [HttpGet("formuri/{legitimatie}")]
        public async Task<IActionResult> GetFormulareBySpLegitimatie([FromRoute] int legitimatie)
        {
            var formulare = manager.GetFormulareForSportiv(legitimatie);
            return Ok(formulare);
        }

        [HttpGet("formuriByEmail/{email}")]
        [Authorize(Policy = "SportivUtilizator")]
        public async Task<IActionResult> GetFormulareBySpEmail([FromRoute] string email)
        {
            var formulare = manager.GetFormulareForSportivByEmail(email);
            return Ok(formulare);
        }
    }
}
