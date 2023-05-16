using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NutriNow.Domains;
using NutriNow.Repository;
using NutriNow.ViewModel;
using System;

namespace NutriNow.Controller
{
    [EnableCors]
    [ApiController]
    [Route("api/v1/registeredfood")]
    public class RegistredFoodController : ControllerBase
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly IRegisterFoodRepository _registerFoodRepository;
        public RegistredFoodController(IGeneralRepository generalRepository, IRegisterFoodRepository registerFoodRepository)
        {
                this._generalRepository = generalRepository;
            this._registerFoodRepository = registerFoodRepository;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RegistredFoodAsync(RegisteredFoodVM registredFood)
        {
            var registeredDate = registredFood.Date;
            var date = new DateTime(registeredDate.Year, registeredDate.Month, registeredDate.Day + 1,registeredDate.Hour ,registeredDate.Minute, registeredDate.Second, DateTimeKind.Utc) ;
            RegisteredFood registeredFood = new RegisteredFood()
            {
                FoodId = registredFood.FoodId,
                Quantity = registredFood.Quantity,
                MealId = registredFood.MealId,
                UserId = registredFood.UserId,
                Date = date,
                
                
            };
            _generalRepository.Add(registeredFood);
            if (await _generalRepository.SaveChangesAsync())
            {
                return Ok(registeredFood);
            }
            else
            {
                throw new Exception();
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteRegisterFood(int id)
        {
            var registerFood = await _registerFoodRepository.GetRegisteredFoodAsyncById(id);
            _generalRepository.Delete(registerFood);
            if (await _generalRepository.SaveChangesAsync())
            {
                return Ok(true);

            }
            else
            {
                return BadRequest(false);
            }
        }

    }
}
