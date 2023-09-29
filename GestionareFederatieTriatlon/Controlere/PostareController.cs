using GestionareFederatieTriatlon.Entitati;
using GestionareFederatieTriatlon.Manageri;
using GestionareFederatieTriatlon.Modele;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionareFederatieTriatlon.Controlere
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostareController : ControllerBase
    {
        private readonly IPostareManager manager;
        public PostareController(IPostareManager manager)
        {
            this.manager = manager;
        }

/*        [HttpGet("byId/{id}")]
        public async Task<IActionResult> GetPostariById([FromRoute] string id)
        {
            var postari = manager.GetPostariIdUtiliz(id);
            return Ok(postari);
        }
*/
        [HttpGet("postariUtilizator/{email}")]
        public async Task<IActionResult> GetPostariByEmail([FromRoute] string email)
        {
            var postari = manager.GetPostariEmailUtiliz(email);
            return Ok(postari);
        }

/*        [HttpGet("byIdul/{idUtiliz}")]
        public async Task<IActionResult> GetPostariVazuteDeUtilizId([FromRoute] string idUtiliz)
        {
            var postari = manager.GetPostariVazuteDeIdUtiliz(idUtiliz);
            return Ok(postari);
        }*/

        [HttpGet("byEmail/{email}")]
        public async Task<IActionResult> GetPostariVazuteDeUtilizEmail([FromRoute] string email)
        {
            var postari = manager.GetPostariVazuteDeIdUtilizComentarii(email);
            return Ok(postari);
        }

        [HttpGet("postari")]
        [Authorize(Policy = "AdminUtilizator")]
        public async Task<IActionResult> GetAllPostari()
        {
            var postari = manager.GetAllPostari();
            return Ok(postari);
        }

        [HttpPut("fericireReactiiCresc")]
        public async Task<IActionResult> UpdateFericC([FromBody] int id)
        {
            manager.UpdateFericireCresc(id);
            return Ok();
        }

        [HttpPut("fericireReactiiDesc")]
        public async Task<IActionResult> UpdateFericD([FromBody] int id)
        {
            manager.UpdateFericireDesc(id);
            return Ok();
        }

        [HttpPut("tristeteReactiiCresc")]
        public async Task<IActionResult> UpdateTristC([FromBody] int id)
        {
            manager.UpdateTristeteCresc(id);
            return Ok();
        }


        [HttpPut("tristeteReactiiDesc")]
        public async Task<IActionResult> UpdateTristD([FromBody] int id)
        {
            manager.UpdateTristeteDesc(id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostare([FromRoute] int id)
        {
            manager.Delete(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PostareCreateModel model)
        {
            manager.Create(model);
            return Ok();
        }
    }
}
