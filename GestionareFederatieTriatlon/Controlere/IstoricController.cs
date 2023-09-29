using GestionareFederatieTriatlon.Entitati;
using GestionareFederatieTriatlon.Manageri;
using GestionareFederatieTriatlon.Modele;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GestionareFederatieTriatlon.Controlere
{
    [Route("api/[controller]")]
    [ApiController]
    public class IstoricController : ControllerBase
    {
        private readonly IIstoricManager manager;
        private readonly UserManager<Utilizator> utilizatorManager;

        public IstoricController(IIstoricManager manager,UserManager<Utilizator> utilizatorManager)
        {
            this.manager = manager;
            this.utilizatorManager= utilizatorManager;
        }

        [HttpPost]
        [Authorize(Policy = "AntrenorUtilizator")]
        public async Task<IActionResult> Create([FromBody] IstoricCreateModel model)
        {
            manager.Create(model);
            return Ok();
        }

        [HttpPut]
        [Authorize(Policy = "AdminUtilizator")]
        public async Task<IActionResult> Update([FromBody] IstoricCreateModel istoric)
        {
            manager.Update(istoric);
            return Ok();
        }

        /*        [HttpGet("bestOf/{id}")]
                public async Task<IActionResult> GetBestofByIdSportiv([FromRoute] string id)
                {
                    var istoric = manager.GetBestOfByIdSportiv(id);
                    return Ok(istoric);
                }*/
        [HttpGet("bestOf/{email}")]
        public async Task<IActionResult> GetBestofByEmailSportiv([FromRoute] string email)
        {
            var istoric = manager.GetBestOfByEmailSportiv(email);
            return Ok(istoric);
        }

        [HttpGet("istoriceComp/{codCompetitie}")]
        [Authorize(Policy = "AdminUtilizator")]
        public async Task<IActionResult> GetAllIstoriceCompetitie([FromRoute] int codCompetitie)
        {
            var istoric = manager.GetIstoriceForCompetitieId(codCompetitie);
            return Ok(istoric);
        }

        [HttpGet("bestOfLegitimatie/{numarLegitimatie}")]
        public async Task<IActionResult> GetBestofByLegitimatiesSportiv([FromRoute] int numarLegitimatie)
        {
            var istoric = manager.GetBestOfByNrLegitimatieSportiv(numarLegitimatie);
            return Ok(istoric);
        }

        /*        [HttpGet("istoric/{id}")]
                public async Task<IActionResult> GetIstoriceByIdSportiv([FromRoute] string id)
                {
                    var istoric = manager.GetIstoricRezultateByIdSportiv(id);
                    return Ok(istoric);
                }*/

        [HttpGet("istoric/{email}")]
        public async Task<IActionResult> GetIstoriceByEmailSportiv([FromRoute] string email)
        {
            var utilizator = await utilizatorManager.FindByEmailAsync(email);
            if (utilizator == null)
                return Ok();
            var id = utilizator.Id;
            var istoric = manager.GetIstoricRezultateByIdSportiv(id);
            return Ok(istoric);
        }

        [HttpGet("istoricLegitimatie/{numarLegitimatie}")]
        public async Task<IActionResult> GetIstoriceByLegitimatieSportiv([FromRoute] int numarLegitimatie)
        {
            var istoric = manager.GetIstoricRezultateByLegitimatieSportiv(numarLegitimatie);
            return Ok(istoric);
        }

        [HttpGet("rezultate/{id}")] //id competitie NEFOLOSIT IN FRONTT
        public async Task<IActionResult> GetRezultateCompetitieOrdonateDupaProba([FromRoute] int id)
        {
            var istoric = manager.GetRezultateCompetitie(id);
            return Ok(istoric);
        }

        [HttpGet("rezultateProba/{id}")]//FOLOSIT ------------------------------------------- REZOLVAT
        public async Task<IActionResult> GetRezultateCompetitieFiltrareProba([FromRoute] int id, string numeProba = "toate probele", string categorie = "toate categoriile", string club = "toate cluburile")
        {
            var istoric = manager.GetRezultateCompetitieNumeProbaCategorieClub(id,numeProba,categorie,club);
            return Ok(istoric);
        }

        [HttpGet("numeProbeParticipante/{id}")] //id competitie
        public async Task<IActionResult> GetNumeProbe([FromRoute] int id)
        {
            var istoric = manager.GetNumeProbe(id);
            return Ok(istoric);
        }

        [HttpGet("categoriiParticipante/{id}")] //id competitie
        public async Task<IActionResult> GetCategoriiPart([FromRoute] int id)
        {
            var istoric = manager.GetCategParticipante(id);
            return Ok(istoric);
        }

        [HttpGet("cluburiParticipante/{id}")] //id competitie  FOLOSIT ------------------------------------- bifaaat
        public async Task<IActionResult> GetCluburiPart([FromRoute] int id)
        {
            var istoric = manager.GetCluburiParticipante(id);
            return Ok(istoric);
        }
        [HttpGet("numeComp/{id}")] //id competitie
        public async Task<IActionResult> GetNumeComp([FromRoute] int id)
        {
            var istoric = manager.GetNumeCompetitie(id);
            return Ok(istoric);
        }


        [HttpGet("ierarhie")]//FOLOSIT -------------------------------------------------- GATA
        public async Task<IActionResult> GetIerarhie(string categ = "toate categoriile", string club = "toate cluburile", string an = "toti anii", string proba = "toate probele")
        {
            var istoric = manager.GetIerarhiePuncteCategClubAnProba(categ, club, an, proba);
            return Ok(istoric);
        }

        [HttpGet("topCluburi/{id}")] //FOLOSIT --------------------------------- gataa
        public async Task<IActionResult> GetTopCluburiCompId([FromRoute] int id)
        {
            var istoric = manager.GetTopCluburiByComp(id);
            return Ok(istoric);
        }

        [HttpGet("cluburiTop")]//FOLOSIT ---------------------------------------gata
        public async Task<IActionResult> GetTopCluburi(string an = "toti anii")
        {
            var istoric = manager.GetTopCluburiPerAn(an);
            return Ok(istoric);
        }

        /*        [HttpGet("antrenorStatistMedalii/{id}")]
                public async Task<IActionResult> GetStatistica([FromRoute] string id, string numeComp)
                {
                    var istoric = manager.GetStatisticaMedaliiSpAntrComp(id, numeComp);
                    return Ok(istoric);
                }*/

        [HttpGet("antrenorStatistMedalii/{emailAntrenor}")]
        [Authorize(Policy = "AntrenorUtilizator")]
        public async Task<IActionResult> GetStatistica([FromRoute] string emailAntrenor, string numeComp)
        {
            var istoric = manager.GetStatisticaMedaliiSpAntrComp(emailAntrenor, numeComp);
            return Ok(istoric);
        }

        [HttpGet("antrenorStatisticPodium/{emailAntrenor}")]
        [Authorize(Policy = "AntrenorUtilizator")]
        public async Task<IActionResult> GetStatisticaPodium([FromRoute] string emailAntrenor, string numeComp)
        {
            var istoric = manager.GetStatisticaPodiumSpAntrComp(emailAntrenor, numeComp);
            return Ok(istoric);
        }

        /*        [HttpGet("antrenorStatisticPodium/{id}")]
                public async Task<IActionResult> GetStatisticaPodium([FromRoute] string id, string numeComp)
                {
                    var istoric = manager.GetStatisticaPodiumSpAntrComp(id, numeComp);
                    return Ok(istoric);
                }*/
    }
}
