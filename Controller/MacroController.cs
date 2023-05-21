using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NutriNow.Domains;
using NutriNow.Repository;
using NutriNow.ViewModel;

namespace NutriNow.Controller
{
    [EnableCors]
    [ApiController]
    [Route("api/v1/macro")]
    public class MacroController : ControllerBase
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly IMacroRepository _macroRepository;
        public MacroController(IMacroRepository macroRepository, IGeneralRepository generalRepository)
        {
            this._macroRepository = macroRepository;
            this._generalRepository = generalRepository;
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateMacroGoal(UpdateMacroVM macro)
        {
            var macroFinded = await _macroRepository.GetMacroById(macro.MacroId);
            macroFinded.Carbs = macro.Carbs;
            macroFinded.Kcal = macro.Kcal;
            macroFinded.Fat = macro.Fat;
            macroFinded.Water  = macro.Water;
            macroFinded.Protein = macro.Protein;
           
            _generalRepository.Update(macroFinded);
            if (await _generalRepository.SaveChangesAsync())
            {
                return Ok(macroFinded);
            } else
            {
                return BadRequest();
            }

        }
    }
}
