using GestionareFederatieTriatlon.Manageri;
using GestionareFederatieTriatlon.Modele;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionareFederatieTriatlon.Controlere
{
    [Route("api/[controller]")]
    [ApiController]
    public class AntrenorController : ControllerBase
    {
        private readonly IAntrenorManager manager;
        private readonly IUtilizatorManager managerUtiliz;
        public AntrenorController(IAntrenorManager manager, IUtilizatorManager managerUtiliz)
        {
            this.manager = manager;
            this.managerUtiliz = managerUtiliz;
        }
        [HttpGet]
        public async Task<IActionResult> GetAntrenori()
        {
            var antrenori = manager.GetAntrenori();
            return Ok(antrenori);
        }
        /*        [HttpGet("byId/{id}")]
                public async Task<IActionResult> GetAntrenorById([FromRoute] string id)
                {
                    var antrenor = manager.GetAntrenorInfo(id);
                    return Ok(antrenor);
         }*/

        [HttpGet("byEmail/{email}")]
        [Authorize(Policy = "AntrenorUtilizator")]
        public async Task<IActionResult> GetAntrenorById([FromRoute] string email)
        {
            var antrenor = manager.GetAntrenorInfo(email);
            return Ok(antrenor);
        }

        ////////////////
        /*        [HttpGet("pozaByEmail/{email}")]
                public async Task<IActionResult> GetAntrenorPozaById([FromRoute] string email)
                {
                    var urlPoza = managerUtiliz.GetPozaUtilizator(email);
                    return Ok(urlPoza);
                }*/

        /*        [HttpGet("byIdViewSportiv/{id}")]
                public async Task<IActionResult> GetAntrenorByIdViewSp([FromRoute] string id) //cum este vazut profilul antrenorului de catre sportiv
                {
                    var antrenor = manager.GetAntrenorInfoBySp(id);
                    return Ok(antrenor);
                }
        */
        [HttpGet("byEmailViewSportiv/{email}")] 
        public async Task<IActionResult> GetAntrenorByIdViewSp([FromRoute] string email) //cum este vazut profilul antrenorului de catre sportiv
        {
            var antrenor = manager.GetAntrenorInfoBySp(email);
            return Ok(antrenor);
        }
        [HttpGet("nrSportivi/{email}")]
        [Authorize(Policy = "AntrenorUtilizator")]
        public async Task<IActionResult> GetNrSportivi([FromRoute] string email)
        {
            var numar = manager.GetNumarSportiviActualiAntr(email);
            return Ok(numar);
        }

        [HttpGet("byIdClub/{id}")]
        public async Task<IActionResult> GetAntrenoriByIdClub([FromRoute] int id)
        {
            var antrenori = manager.GetAntrenoriByClubId(id);
            return Ok(antrenori);
        }

        [HttpGet("byIdClubSearch/{id}")]
        public async Task<IActionResult> GetAntrenoriByIdClubSearch([FromRoute] int id,string numeFam="null",string prenume="null")
       {
            var antrenori = manager.GetAntrenoriSearchByClubId(id,numeFam,prenume);
            return Ok(antrenori);
        }

        [HttpPut]
        [Authorize(Policy = "AntrenorUtilizator")]
        public async Task<IActionResult> Update([FromBody] AntrenorUpdateModel model)
        {
            manager.Update(model);
            return Ok();
        }

    }
}
