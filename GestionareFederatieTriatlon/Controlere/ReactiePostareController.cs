using GestionareFederatieTriatlon.Manageri;
using GestionareFederatieTriatlon.Modele;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionareFederatieTriatlon.Controlere
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactiePostareController : ControllerBase
    {
        private readonly IReactiePostareManager manager;

        public ReactiePostareController(IReactiePostareManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReactiePostareCreateModel model)
        {
            manager.Create(model);
            return Ok();
        }

        [HttpPut("fericire")]
        public async Task<IActionResult> UpdateReactieFericire([FromBody] ReactiePostareUpdateFericireModel model)
        {
            manager.UpdateFericire(model);
            return Ok();
        }

        [HttpPut("tristete")]
        public async Task<IActionResult> UpdateReactieTristete([FromBody] ReactiePostareUpdateTristeteModel model)
        {
            manager.UpdateTristete(model);
            return Ok();
        }



        [HttpGet("reactii/{codPostare}/{emailUtilizator}")]
        public async Task<IActionResult> GetReactiiForUserPostare([FromRoute]string emailUtilizator, int codPostare)
        {
            var reactii = manager.GetReactiiForUserPost(emailUtilizator,codPostare);
            return Ok(reactii);
        }

        [HttpGet("nrFericire/{codPostare}")]
        public async Task<IActionResult>GetNrReactiiFericire([FromRoute] int codPostare)
        {
            var nrFericire = manager.GetNrTotalFericirePostare(codPostare);
            return Ok(nrFericire);
        }

        [HttpGet("nrTristete/{codPostare}")]
        public async Task<IActionResult> GetNrReactiiTristete([FromRoute] int codPostare)
        {
            var nrTristete = manager.GetNrTotalTristetePostare(codPostare);
            return Ok(nrTristete);
        }


    }
}
