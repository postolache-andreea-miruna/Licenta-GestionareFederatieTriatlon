using GestionareFederatieTriatlon.Manageri;
using GestionareFederatieTriatlon.Modele;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionareFederatieTriatlon.Controlere
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentariuController : ControllerBase
    {
        private readonly IComentariuManager manager;

        public ComentariuController(IComentariuManager manager)
        {
            this.manager = manager;
        }

        [HttpGet("byId/{id}")]
        public async Task<IActionResult> GetComentariiByIdPostare([FromRoute] int id)
        {
            var comentarii = manager.GetComentariiByIdPostare(id);
            return Ok(comentarii);
        }

        [HttpGet("comentarii")]
        [Authorize(Policy = "AdminUtilizator")]
        public async Task<IActionResult> GetComentariiTotal()
        {
            var comentarii = manager.GetComentariiCodMesaj();
            return Ok(comentarii);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComentariu([FromRoute] int id)
        {
            manager.Delete(id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ComentariuUpdateModel model)
        {
            manager.Update(model);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ComentariuModelCreate model)
        {
            manager.Create(model);
            return Ok();
        }

    }
}
