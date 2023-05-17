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
        //User User1 = new User(1, "Dante Schiavi", "dantemds@email.com", "123", null, null, null);


        //List<User> Users = new List<User>();
        //List<Meal> Meal = new List<Meal>();
        //List<RegisteredFood> RegisteredFood = new List<RegisteredFood>();
        //List<Food> Food = new List<Food>();
        //public UserController()
        //{
        //    UserInformation UserInfo2 = new UserInformation(1, "+30", "Homem", "4-6 vezes na semana", "Ganhar peso", "175", "-90");
        //    Macro Macro2 = new Macro(1, "300", "250", "15", "2000", 1, 2, "2800");
        //    Macro macroGoalMeal = new Macro(2, "100", "100", "3", null, 2, 1, "600");
        //    Macro macroFood = new Macro(3, "1", "12", "1", null, 3, 1, "30");
        //    Food food1 = new Food(1, "Pão integral", macroFood);
        //    Food.Add(food1);
        //    RegisteredFood registeredFood = new RegisteredFood(1, 1, 2, Food, 100);
        //    RegisteredFood.Add(registeredFood);
        //    Meal meal1 = new Meal(1, "Café da Manhã", "08:00", macroGoalMeal, RegisteredFood);
        //    Meal.Add(meal1);
        //    User User2 = new User(2, "Ruan Lucas", "ruan@email.com", "1234", UserInfo2, Macro2, Meal);

        //    Users.Add(User1);
        //    Users.Add(User2);
        //}
        private readonly AppSettings _appSettings;
        private readonly IUserRepository _userRepository;
        private readonly IGeneralRepository _generalRepository;
        public UserController(IUserRepository userRepository, IGeneralRepository generalRepository, IOptions<AppSettings> appSettings)
        {
            this._generalRepository = generalRepository;
            this._appSettings = appSettings.Value;
            this._userRepository = userRepository;
        }

        //[HttpGet("getall")]
        //public async Task<IActionResult> GetAllUsersAsync()
        //{
        //    if (Users.Count == 0) return NoContent();
        //    return Ok(Users);
        //}

        [HttpPost("auth")]
        public async Task<IActionResult> AuthUserAsync(AuthUserVM authUser)
        {
            //User userFinded = Users.Find(u => u.Email == authUser.Email);
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
                ResponseUserVM responseUserVM = new ResponseUserVM(userFinded.UserId, userFinded.UserName, userFinded.UserInformation, userFinded.Meals, userFinded.MacroGoal, userFinded.CurrentMacro, token);
                return Ok(responseUserVM);
            }
            return Unauthorized();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUserAsync(CreateUserVM user)
        {
            //User newUser = new User(3, user.UserName, user.Email, user.Password, null, null, null);
            User newUser = new User(user.Password, user.Email, user.UserName);
            
            _generalRepository.Add(newUser);
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
            ResponseUserVM responseUserVM = new ResponseUserVM(userFinded.UserId, userFinded.UserName, userFinded.UserInformation, userFinded.Meals, userFinded.MacroGoal, userFinded.CurrentMacro, token);
            return Ok(responseUserVM);
        }

    }
}
