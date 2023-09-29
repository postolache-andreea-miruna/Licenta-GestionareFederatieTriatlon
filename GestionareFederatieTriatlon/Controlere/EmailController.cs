using GestionareFederatieTriatlon.Manageri;
using GestionareFederatieTriatlon.Modele;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionareFederatieTriatlon.Controlere
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        IEmailServ manager = null;
        public EmailController(IEmailServ manager) 
        { 
            this.manager = manager;
        }

        [HttpPost]
        public bool TrimeMail(DateEmail detalii)
        {
            return manager.TrimiteEmail(detalii);
        }
    }
}
