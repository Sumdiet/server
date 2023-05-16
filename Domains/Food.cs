using System.ComponentModel.DataAnnotations;

namespace NutriNow.Domains
{
    public class Food
    {
        [Key]
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public Macro Macro { get; set; }
        public ICollection<RegisteredFood> RegisteredFoods { get; set; }

        //Macro será sempre para cada 100ml ou 100g

        public Food(int id, string foodName, Macro macro)
        {
            this.FoodId = id;
            this.FoodName = foodName;
            this.Macro = macro;
        }
        [ObsoleteAttribute("Esse método é usado para testes e como construtor padrão do entityFramework", false)]
        public Food()
        {

        }
    }
}
