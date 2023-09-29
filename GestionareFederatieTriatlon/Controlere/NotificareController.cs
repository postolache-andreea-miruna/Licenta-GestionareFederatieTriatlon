using GestionareFederatieTriatlon.Manageri;
using GestionareFederatieTriatlon.Modele;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionareFederatieTriatlon.Controlere
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificareController : ControllerBase
    {
        private readonly INotificareManager manager;
        public NotificareController(INotificareManager manager)
        {
            this.manager = manager;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotificare([FromRoute] int id)
        {
            manager.Delete(id);
            return Ok();
        }
        /*        [HttpGet("byId/{id}")]
                public async Task<IActionResult> GetNotificariLogatUser([FromRoute] string id)
                {
                    var notif = manager.GetNotificariIdUtilizLogat(id);
                    return Ok(notif);
                }*/
        [HttpGet("notificarile")]
        public async Task<IActionResult> GetNotificariCodMesaj()
        {
            var notif = manager.GetCodMesajNotificari();
            return Ok(notif);
        }

        [HttpGet("byEmail/{email}")]
        public async Task<IActionResult> GetNotificariLogatUser([FromRoute] string email)
        {
            var notif = manager.GetNotificariIdUtilizLogat(email);
            return Ok(notif);
        }

        [HttpGet("numarNotif/{email}")]
        public async Task<IActionResult> GetNrNotificariLogatUser([FromRoute] string email)
        {
            var notif = manager.GetNrNotificariNecititeIdUtilizLogat(email);
            return Ok(notif);
        }

        [HttpGet("byIdul/{idNotif}")]
        public async Task<IActionResult> GetInfoNotifById([FromRoute] int idNotif)
        {
            var notif = manager.GetNotificareById(idNotif);
            return Ok(notif);
        }

        [HttpGet("lista/{emailUtilizator}/{numarLegitimatieUtiliz2}")]
        public async Task<IActionResult> lista([FromRoute] string emailUtilizator, int numarLegitimatieUtiliz2)
        {
            var notif = manager.GetLista(emailUtilizator, numarLegitimatieUtiliz2);
            return Ok(notif);
        }

        [HttpPost]
        [Authorize(Policy = "AntrenorUtilizator")]
        public async Task<IActionResult> Create([FromBody] NotificareCreateModel model)
        {
            manager.Create(model);
            return Ok();
        }

        [HttpPatch("notificareCitita/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            manager.NotificareCititaUpdate(id);
            return Ok();
        }
    }
}
