using NutriNow.DataTransferObj;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutriNow.Domains
{
    public class RegisteredFood
    {
        [Key()]
        public int RegisteredFoodId { get; set; }
        public int MealId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("Food")]
        public int FoodId { get; set; }
        public Food Food { get; set; }

        public double Quantity { get; set; }
        public DateTime Date { get; set; }
        [NotMapped]
        public MacroDto? CurrentMacro { get; set; } = new MacroDto();

        public RegisteredFood(int id, int mealId, int userId, Food food, double quantity) 
        {
            this.MealId = mealId;
            this.UserId = userId;
            this.Food = food;
            this.RegisteredFoodId = id;
            this.Quantity = quantity;
            CoutingMacro();

        }

        [ObsoleteAttribute("Esse método é usado para testes e como construtor padrão do entityFramework", false)]
        public RegisteredFood()
        {
        }

       
        public void CoutingMacro()
        {
           
            foreach (var key in typeof(MacroDto).GetProperties())
            {
                var keyMacro = typeof(Macro).GetProperties().Where(x => x.Name == key.Name).FirstOrDefault()!;
                var value = keyMacro.GetValue(this.Food.Macro);
                if (value.ToString() == "null") continue;
                var percent = (Convert.ToDouble(value) * this.Quantity) / 100;
                key.SetValue(this.CurrentMacro, percent.ToString());
            }
        }

    }
}
