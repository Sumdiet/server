using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NutriNow.Repository;

namespace NutriNow.Controller
{
    [EnableCors]
    [ApiController]
    [Route("api/v1/food")]
    public class FoodController : ControllerBase
    {
        private readonly IFoodRepository _foodRepository;
        public FoodController(IFoodRepository foodRepository)
        {
            this._foodRepository = foodRepository; 
        }

        [HttpGet]
        public async Task<IActionResult> getFood()
        {
            return Ok(await _foodRepository.GetFood());
        }
    }
}
