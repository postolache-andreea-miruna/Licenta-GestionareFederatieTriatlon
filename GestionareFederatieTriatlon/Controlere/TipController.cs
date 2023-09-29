using GestionareFederatieTriatlon.Manageri;
using GestionareFederatieTriatlon.Modele;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionareFederatieTriatlon.Controlere
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipController : ControllerBase
    {
        private readonly ITipManager manager;
        public TipController(ITipManager manager)
        {
            this.manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetTipuri()
        {
            var tipuri = manager.GetTip();
            return Ok(tipuri);
        }
        [HttpGet("tipuriTotal")]
        [Authorize(Policy = "AdminUtilizator")]
        public async Task<IActionResult> GetTipuriTotal()
        {
            var tipuri = manager.GetTipuri();
            return Ok(tipuri);
        }

        [HttpGet("tipuri")]
        public async Task<IActionResult> GetTipuriCodNume()
        {
            var tipuri = manager.GetTipCodNume();
            return Ok(tipuri);
        }

        [HttpGet("byId/{id}")]
        public async Task<IActionResult> GetTipById([FromRoute] int id)
        {
            var tip = manager.GetTipInfo(id);
            return Ok(tip);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTip([FromRoute] int id)
        {
            manager.Delete(id);
            return Ok();
        }

        [HttpPut]
        [Authorize(Policy = "AdminUtilizator")]
        public async Task<IActionResult> Update([FromBody] TipUpdateModel model)
        {
            manager.Update(model);
            return Ok();
        }

        [HttpPost]
        [Authorize(Policy = "AdminUtilizator")]
        public async Task<IActionResult> Create([FromBody] TipModelById model)
        {
            manager.Create(model);
            return Ok();
        }
    }
}
