using GestionareFederatieTriatlon.Manageri;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionareFederatieTriatlon.Controlere
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilizatorController : ControllerBase
    {
        private readonly IUtilizatorManager managerUtiliz;
        public UtilizatorController(IUtilizatorManager managerUtiliz)
        {
            this.managerUtiliz = managerUtiliz;
        }

        [HttpGet("antrenori")]
        public async Task<IActionResult> GetDetaliiAntrenoriEmail()
        {
            var antrenori = managerUtiliz.GetDetaliiTrimitereEmail();
            return Ok(antrenori);
        }
        [HttpGet("pozaByEmail/{email}")]
        public async Task<IActionResult> GetUtilizatorPozaById([FromRoute] string email)
        {
            var urlPoza = managerUtiliz.GetPozaUtilizator(email);
            return Ok(urlPoza);
        }

        [HttpGet("abonareStiri/{email}")]
        public async Task<IActionResult> GetUtilizatorAbonareStiri([FromRoute] string email)
        {
            var abonare = managerUtiliz.GetAbonare(email);
            return Ok(abonare);
        }
    }
}
