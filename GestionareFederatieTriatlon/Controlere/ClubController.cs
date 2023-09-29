using GestionareFederatieTriatlon.Manageri;
using GestionareFederatieTriatlon.Modele;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionareFederatieTriatlon.Controlere
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : ControllerBase
    {
        private readonly IClubManager manager;

        public ClubController(IClubManager manager)
        {
            this.manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetClub()
        {
            var listaCluburi = manager.GetClub();
            return Ok(listaCluburi);
        }
        [HttpGet("cluburi")]
        [Authorize(Policy = "AdminUtilizator")]
        public async Task<IActionResult> GetCluburi()
        {
            var listaCluburi = manager.GetCluburi();
            return Ok(listaCluburi);
        }
        [HttpGet("numeCluburi")]
        public async Task<IActionResult> GetNumeCluburi()
        {
            var listaCluburi = manager.GetNumeCluburi();
            return Ok(listaCluburi);
        }

        //[HttpGet] --III.	GetCluburi Ordonate Dupa Nr De Puncte Obtinute De Sportivi La Competitia Cu IdDat

        [HttpGet("byId/{id}")]
        public async Task<IActionResult> GetClubById([FromRoute] int id)
        {
            var club = manager.GetClubInfo(id);
            return Ok(club);
        }

        [HttpPut]
        [Authorize(Policy = "AdminUtilizator")]
        public async Task<IActionResult> Update([FromBody] ClubUpdateModel clubUpdateModel)
        {
            manager.Update(clubUpdateModel);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClub([FromRoute] int id)
        {
            manager.Delete(id);
            return Ok();
        }

        [HttpPost]
        [Authorize(Policy = "AdminUtilizator")]
        public async Task<IActionResult> Create([FromBody] ClubModelById model)
        {
            manager.Create(model);
            return Ok();
        }
    }
}
