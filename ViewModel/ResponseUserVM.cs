using NutriNow.DataTransferObj;
using NutriNow.Domains;

namespace NutriNow.ViewModel
{
    public class ResponseUserVM
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public UserInformation? UserInformation { get; set; }
        public ICollection<Meal>? Meals { get; set; }
        public Macro? MacroGoal { get; set; }
        public MacroDto? CurrentMacro { get; set; }
        public Food[] Food { get; set; }
        public string Token { get; set; }

        public ResponseUserVM(int userId, string userName, UserInformation? userInformation, ICollection<Meal>? meals, Macro? macro, MacroDto? currentMacro, string token)
        {
            this.UserId = userId;
            this.UserName = userName;
            this.UserInformation = userInformation;
            this.Meals = meals;
            this.MacroGoal = macro;
            this.CurrentMacro = currentMacro;
            this.Token = token;

        }
    }
}
