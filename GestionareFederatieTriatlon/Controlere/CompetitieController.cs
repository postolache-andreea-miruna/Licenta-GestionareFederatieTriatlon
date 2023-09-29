using GestionareFederatieTriatlon.Manageri;
using GestionareFederatieTriatlon.Modele;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionareFederatieTriatlon.Controlere
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitieController : ControllerBase
    {
        private readonly ICompetitieManager manager;
        public CompetitieController(ICompetitieManager manager)
        {
            this.manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompetitie()
        {
            var listaCompetitii = manager.GetCompetitii();
            return Ok(listaCompetitii);
        }

        [HttpGet("numeComp/{codComp}")]
        public async Task<IActionResult> GetNumeCompId([FromRoute]int codComp)
        {
            var nume = manager.GetNumeCompId(codComp);
            return Ok(nume);
        }

        [HttpGet("competitii")]
        public async Task<IActionResult> GetCompetitiiTotal()
        {
            var listaCompetitii = manager.GetCompetitiiTotal();
            return Ok(listaCompetitii);
        }

        [HttpGet("byEmailSportiv/{email}")]
        [Authorize(Policy = "SportivUtilizator")]
        public async Task<IActionResult> GetCompetitiiByEmailSportiv([FromRoute] string email)
        {
            var competitii = manager.GetCompetitiiSportiv(email);
            return Ok(competitii);
        }

        [HttpGet("numeCompetitii")]
        public async Task<IActionResult> GetNumeCompetitiiNeanulate()
        {
            var competitii = manager.GetNumeCompNeanulate();
            return Ok(competitii);
        }

        [HttpGet("numeIdCompetitii")]
        public async Task<IActionResult> GetNumeIdCompetitiiNeanulate()
        {
            var competitii = manager.GetNumeIdCompetitii();
            return Ok(competitii);
        }

        [HttpGet("byId/{id}")]
        public async Task<IActionResult> GetCompetitieById([FromRoute] int id)
        {
            var competitie = manager.GetCompetitieInfo(id);
            return Ok(competitie);
        }

        /*        [HttpPut("delete")]
                public async Task<IActionResult> Delete([FromBody] CompetitieModelDelete model) // is just the status changed
                {
                    manager.Delete(model);
                    return Ok();
                }*/

 /*       [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            manager.Delete(id);
            return Ok();
        }*/

        [HttpPut]
        [Authorize(Policy = "AdminUtilizator")]
        public async Task<IActionResult> Update([FromBody] CompetitieModelUpdate model)
        {
            manager.Update(model);
            return Ok();
        }

        [HttpPost]
        [Authorize(Policy = "AdminUtilizator")]
        public async Task<IActionResult> Create([FromBody] CompetitieModelCreate model)
        {
            manager.Create(model);
            return Ok();
        }

    }
}
