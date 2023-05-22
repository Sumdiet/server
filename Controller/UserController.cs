using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NutriNow.Configuration;
using NutriNow.Domains;
using NutriNow.Repository;
using NutriNow.ViewModel;

namespace NutriNow.Controller
{
    [EnableCors]
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : ControllerBase
    {
       
        private readonly AppSettings _appSettings;
        private readonly IUserRepository _userRepository;
        private readonly IGeneralRepository _generalRepository;
        private readonly IMealRepository _mealRepository;
        public UserController(IUserRepository userRepository, IGeneralRepository generalRepository, IOptions<AppSettings> appSettings, IMealRepository mealRepository)
        {
            this._generalRepository = generalRepository;
            this._mealRepository = mealRepository;
            this._appSettings = appSettings.Value;
            this._userRepository = userRepository;
        }

        [HttpPost("auth")]
        public async Task<IActionResult> AuthUserAsync(AuthUserVM authUser)
        {
            User userFinded = await _userRepository.GetUserAsyncByEmail(authUser.Email,authUser.Date);
            if (userFinded == null) return NotFound();
            bool verified = userFinded.VerifyHash(authUser.Password);
            if (verified)
            {
                foreach(Meal meal in userFinded.Meals)
                {
                    foreach(RegisteredFood registeredFood in meal.RegisteredFood)
                    {
                        registeredFood.CoutingMacro();
                    }
                }
                userFinded.CountingMacro();
                string token = userFinded.CreateTokenJwt(_appSettings.Secret, _appSettings.ValidoEm, _appSettings.Emissor, _appSettings.ExpiracaoHoras);
                ResponseUserVM responseUserVM = new ResponseUserVM(userFinded.UserId, userFinded.UserName, userFinded.Meals, userFinded.MacroGoal, userFinded.CurrentMacro, token, userFinded.Water);
                return Ok(responseUserVM);
            }
            return Unauthorized();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUserAsync(CreateUserVM user)
        {
            Macro defaultMacro = new Macro()
            {
                Protein = "60",
                Carbs = "300",
                Fat = "50",
                Kcal = "2000",
                Type = 1,
                Water = "2000"
            };

            _generalRepository.Add(defaultMacro);




            User newUser = new User(user.Password, user.Email, user.UserName, defaultMacro.MacroId);
            
            _generalRepository.Add(newUser);
            if (await _generalRepository.SaveChangesAsync())
            {
                var userFinded = await _userRepository.GetUserAsyncByEmail(user.Email, DateTime.Now);
                Meal meal1 = new Meal("Café da manhã", "08:00", Guid.Parse("c1e888b1-b1a3-475f-bc7b-4e6f419610cc"), userFinded.UserId);
                Meal meal2 = new Meal("Almoço", "12:00", Guid.Parse("c1e888b1-b1a3-475f-bc7b-4e6f419610cc"), userFinded.UserId);
                Meal meal3 = new Meal("Café da tarde", "16:00", Guid.Parse("c1e888b1-b1a3-475f-bc7b-4e6f419610cc"), userFinded.UserId);
                Meal meal4 = new Meal("Jantar", "20:00", Guid.Parse("c1e888b1-b1a3-475f-bc7b-4e6f419610cc"), userFinded.UserId);

                _generalRepository.Add(meal1);
                _generalRepository.Add(meal2);
                _generalRepository.Add(meal3);
                _generalRepository.Add(meal4);
            }
            if (await _generalRepository.SaveChangesAsync())
            {
                user.Password = "";
                return Ok(user);
            }
            else
            {
                throw new Exception();
            }
        }

        [HttpGet("{idUser}")]
        [Authorize]
        public async Task<IActionResult> GetLogedUserAsync(int idUser, DateTime date)
        {
            User userFinded = await _userRepository.GetUserAsyncById(idUser, date);
            if (userFinded == null) return NotFound();
            foreach (Meal meal in userFinded.Meals)
            {
                foreach (RegisteredFood registeredFood in meal.RegisteredFood)
                {
                    registeredFood.CoutingMacro();
                }
            }
            userFinded.CountingMacro();
            string token = userFinded.CreateTokenJwt(_appSettings.Secret, _appSettings.ValidoEm, _appSettings.Emissor, _appSettings.ExpiracaoHoras);
            ResponseUserVM responseUserVM = new ResponseUserVM(userFinded.UserId, userFinded.UserName, userFinded.Meals, userFinded.MacroGoal, userFinded.CurrentMacro, token, userFinded.Water);
            return Ok(responseUserVM);
        }

    }
}
