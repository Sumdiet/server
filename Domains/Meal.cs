using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;

namespace NutriNow.Domains
{
    public class Meal
    {
        [Key()]
        public int MealId { get; set; }
        public string MealName { get; set; }
        public string MealTime { get; set; }
        public ICollection<RegisteredFood>? RegisteredFood { get; set; }
        public Macro MacroGoal { get; set; }

        public Meal(int id, string mealName, string mealTime, Macro macroGoal, ICollection<RegisteredFood>? registeredFoods)
        {
            this.MealId = id;
            this.MealName = mealName;
            this.MealTime = mealTime;
            this.MacroGoal = macroGoal;
            this.RegisteredFood = registeredFoods;
        }
        [ObsoleteAttribute("Esse método é usado para testes e como construtor padrão do entityFramework", false)]
        public Meal()
        {

        }
    }
}
