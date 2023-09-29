using GestionareFederatieTriatlon.Entitati;
using GestionareFederatieTriatlon.Manageri;
using GestionareFederatieTriatlon.Modele;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GestionareFederatieTriatlon.Controlere
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatManager manager;
        private readonly UserManager<Utilizator> userManager;
        public ChatController(IChatManager manager, UserManager<Utilizator> userManager)
        {
            this.manager = manager;
            this.userManager = userManager;
        }
        [HttpPatch("conexiune/{idUtilizator}")]
        public async Task<IActionResult> UpdateCodConexiune(string idUtilizator, string conexiune)
        {
            manager.UpdateCodConexiune(idUtilizator, conexiune);
            return Ok();
        }


/*        [HttpPost("create-chat")]
        public async Task<IActionResult> CreateChat()
        {
            manager.CreateChat();
            return Ok();
        }*/

        [HttpPost("create-meesaj")]
        public async Task<IActionResult> CreateMesaj([FromBody] ConversatieCreateModel model)
        {
            manager.CreateMesaj(model);
            return Ok();
        }

        [HttpGet("mesaje/{emailUtiliz}/{emailUtiliz2}")]
        public async Task<IActionResult> GetConversatii(string emailUtiliz, string emailUtiliz2)
        {
            var codUtiliz = userManager.Users.Where(u => u.Email == emailUtiliz).Select(u => u.Id).FirstOrDefault();
            var codUtiliz2 = userManager.Users.Where(u => u.Email == emailUtiliz2).Select(u => u.Id).FirstOrDefault();

            var mesaje = manager.GetMesaje(codUtiliz, codUtiliz2);
            return Ok(mesaje);
        }
        [HttpPut("editDispo/{emailUtiliz},{disponibilitate}")]
        public async Task<IActionResult> UpdateDispo([FromRoute] string emailUtiliz, bool disponibilitate)
        {
            var idUtiliz = userManager.Users.Where(u => u.Email == emailUtiliz).Select(u => u.Id).FirstOrDefault();
            manager.UpdateDisponibilitate(idUtiliz,disponibilitate);
            return Ok();
        }


        [HttpGet("nume/{emailUtiliz}")]
        public async Task<IActionResult> GetNumeUtiliz(string emailUtiliz)
        {
            var codUtiliz = userManager.Users.Where(u => u.Email == emailUtiliz).Select(u => u.Id).FirstOrDefault();
            var nume = manager.GetNume(codUtiliz);
            return Ok(nume);
        }

        [HttpGet("mesajeNecitite/{emailUtiliz}")]
        public async Task<IActionResult> GetNrMesajeNecitite(string emailUtiliz)
        {
            var numar = manager.NumarMesajeNecitite(emailUtiliz);
            return Ok(numar);
        }

        [HttpGet("prenume/{emailUtiliz}")]
        public async Task<IActionResult> GetPrenumeUtiliz(string emailUtiliz)
        {
            var codUtiliz = userManager.Users.Where(u => u.Email == emailUtiliz).Select(u => u.Id).FirstOrDefault();
            var prenume = manager.GetPrenume(codUtiliz);
            return Ok(prenume);
        }
        /*
         [HttpGet]
        public async Task<IActionResult> GetClub()
        {
            var listaCluburi = manager.GetClub();
            return Ok(listaCluburi);
        }

         [HttpPut]
        public async Task<IActionResult> Update([FromBody] ClubUpdateModel clubUpdateModel)
        {
            manager.Update(clubUpdateModel);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClubModelById model)
        {
            manager.Create(model);
            return Ok();
        }
         */
    }
}
