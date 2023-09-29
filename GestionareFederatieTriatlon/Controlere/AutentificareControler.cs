using GestionareFederatieTriatlon.Manageri;
using GestionareFederatieTriatlon.Modele;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionareFederatieTriatlon.Controlere
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutentificareController : ControllerBase
    {
        private IAutentificareManager autentificareManager;
        private ITokenManager tokenManager;

        public AutentificareController(IAutentificareManager autentificareManager,ITokenManager tokenManager)
        {
            this.autentificareManager = autentificareManager;
            this.tokenManager = tokenManager;
        }

        [HttpPost("inregistrare")]
        public async Task<IActionResult> Inregistrare([FromBody] InregistrareUtilizatorModel model)
        {
            try
            {
                await autentificareManager.Inregistrare(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Eroare la inregistrare");
            }
        }

        [HttpPost("logare")]
        public async Task<IActionResult> Logare([FromBody] LogareUtilizatorModel model)
        {
            try
            {
                var tokenuri = await autentificareManager.Logare(model);
                if (tokenuri != null)
                    return Ok(tokenuri);
                else
                {
                    return BadRequest("Eroare la logare");
                }

            }
            catch (Exception ex)
            {
                return BadRequest("Eroare");
            }
        }

        [HttpPost("rol")]
        public async Task<IActionResult> Rol([FromBody] LogareUtilizatorModel model)
        {
            var rol = await autentificareManager.Rol(model);
            return Ok(rol);
        }

        [HttpGet("tokenValid/{token}")]
        public async Task<IActionResult> GetTokenIsValid([FromRoute]string token)
        {
            var isValid = tokenManager.IsTokenExpired(token);
            return Ok(isValid);
        }
    }
}
