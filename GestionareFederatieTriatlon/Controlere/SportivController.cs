using GestionareFederatieTriatlon.Manageri;
using GestionareFederatieTriatlon.Modele;
using GestionareFederatieTriatlon.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace GestionareFederatieTriatlon.Controlere
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportivController : ControllerBase
    {
        private readonly ISportivManager manager;
        public SportivController(ISportivManager manager)
        {
            this.manager = manager;
        }

        /*        [HttpGet("byId/{id}")]
                public async Task<IActionResult> GetSportivById(string id)
                {
                    var sportiv = manager.GetSportivById(id);
                    return Ok(sportiv);
                }*/
        [HttpGet("byEmail/{email}")]
        [Authorize(Policy = "SportivUtilizator")]
        public async Task<IActionResult> GetSportivById(string email)
        {
            var sportiv = manager.GetSportivById(email);
            return Ok(sportiv);
        }

        /*        [HttpGet("byIdView/{id}")] //vazuta de cei care nu sunt sportivi/admin
                public async Task<IActionResult> GetSportivByIdNewView(string id)
                {
                    var sportiv = manager.GetSportivByIdView(id);
                    return Ok(sportiv);
                }*/

        [HttpGet("byEmailView/{email}")] //vazuta de cei care nu sunt sportivi/admin
        public async Task<IActionResult> GetSportivByEmailNewView(string email)
        {
            var sportiv = manager.GetSportivByEmailView(email);
            return Ok(sportiv);
        }

        [HttpGet("varstaSp/{legitimatie}")] //vazuta de cei care nu sunt sportivi/admin
        public async Task<IActionResult> GetSportivVarsta(int legitimatie)
        {
            var sportiv = manager.GetVarstaSportivLeg(legitimatie);
            return Ok(sportiv);
        }

        [HttpGet("gen/{legitimatie}")] //vazuta de cei care nu sunt sportivi/admin
        public async Task<IActionResult> GetSportivGen(int legitimatie)
        {
            var sportiv = manager.GetGenSportivLeg(legitimatie);
            return Ok(sportiv);
        }

        [HttpGet("emailDetSportiv/{legitimatie}")] //vazuta de cei care nu sunt sportivi/admin
        public async Task<IActionResult> GetEmailSportivLegitimat(int legitimatie)
        {
            var sportiv = manager.GetEmailSportivLegitimat(legitimatie);
            return Ok(sportiv);
        }

        /*        [HttpGet("sportiviAntr/{id}")]
                public async Task<IActionResult> GetSportiviAntrenor(string id)
                {
                    var sportivi = manager.GetSportiviForAntrenorById(id);
                    return Ok(sportivi);
                }*/

        [HttpGet("sportiviAntrenor/{email}")]
        [Authorize(Policy = "AntrenorUtilizator")]
        public async Task<IActionResult> GetSportiviAntrenorByEmail(string email)
        {
            var sportivi = manager.GetSportiviForAntrenorByEmail(email);
            return Ok(sportivi);
        }

        [HttpGet("sportiviAntrenorFilter/{email}")]
        [Authorize(Policy = "AntrenorUtilizator")] 
        public async Task<IActionResult> GetSportiviAntrenorByEmail(string email,string gen = "toate genurile", string anNastere = "toti anii")
        {
            var sportivi = manager.GetSportiviFilterForAntrenorByEmail(email,gen,anNastere);
            return Ok(sportivi);
        }

        [HttpGet("sportiviClub/{id}")]
        public async Task<IActionResult> GetSportiviClub(int id)
        {
            var sportivi = manager.GetSportiviClubById(id);
            return Ok(sportivi);
        }

        [HttpGet("sportiviClubSearch/{id}")]
        public async Task<IActionResult> GetSportiviClubSearch(int id,string numeFam="null",string prenume="null")
        {
            var sportivi = manager.GetSportiviClubByIdSearch(id,numeFam,prenume);
            return Ok(sportivi);
        }

        /*        [HttpPut]
                public async Task<IActionResult> Update([FromBody] SportivUpdateModel model)
                {
                    manager.Update(model);
                    return Ok();
                }*/
        [HttpPut]
        [Authorize(Policy = "SportivUtilizator")]
        public async Task<IActionResult> Update([FromBody] SportivEmailUpdateModel model)
        {
            manager.Update(model);
            return Ok();
        }
    }
}
