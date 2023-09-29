using GestionareFederatieTriatlon.Manageri;
using GestionareFederatieTriatlon.Modele;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionareFederatieTriatlon.Controlere
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecenzieController : ControllerBase
    {
        private readonly IRecenzieManager manager;
        public RecenzieController(IRecenzieManager manager)
        {
            this.manager = manager;
        }
        [HttpGet("SteleCompetitie/{id}")]
        public double RecenzieMedieCompetitie([FromRoute] int id)
        {
            var rezultat = manager.GetCompetitieSteleMedie(id);
            return rezultat;
        }

        [HttpGet("recenziiTotal")]
        [Authorize(Policy = "AdminUtilizator")]
        public async Task<IActionResult> GetRecenziiTotal()
        {
            var rezultat = manager.GetCodTextRecenzii();
            return Ok(rezultat);
        }

        [HttpGet("RecenziiCompetitie/{id}")]
        public async Task<IActionResult> GetRecenziiCompId([FromRoute] int id)
        {
            var recenzii = manager.RecenziiSportiviCompId(id);
            return Ok(recenzii);
        }

        [HttpGet("RecenzieSpComp/{email}/{idComp}")]
        public async Task<IActionResult> RecenzieDataSportivCompId([FromRoute] string email, int idComp)//GetRecenziiCompId
        {
            var recenzii = manager.RecenzieDataSportivCompId(email,idComp);
            return Ok(recenzii);
        }

        [HttpGet("RecenziiSp/{email}")]
        [Authorize(Policy = "SportivUtilizator")]
        public async Task<IActionResult> GetRecenziiSportiv([FromRoute] string email)//GetRecenziiCompId
        {
            var recenzii = manager.RecenziiSportiv(email);
            return Ok(recenzii);
        }

        [HttpPost]
        [Authorize(Policy = "SportivUtilizator")]
        public async Task<IActionResult> Create([FromBody] RecenzieCreateModel model)
        {
            manager.Create(model);
            return Ok();
        }

        [HttpPut]
        [Authorize(Policy = "SportivUtilizator")]
        public async Task<IActionResult> Update([FromBody] RecenzieUpdateModel model)
        {
            manager.Update(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminUtilizator")]
        public async Task<IActionResult> DeleteRecenzie([FromRoute] int id)
        {
            manager.Delete(id);
            return Ok();
        }
    }
}
