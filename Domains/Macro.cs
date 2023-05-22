using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutriNow.Domains
{
    public class Macro
    {
        [Key()]
        public Guid MacroId { get; set; } = Guid.NewGuid();
        public string Protein { get; set; }
        public string Carbs { get; set; }
        public string Fat { get; set; }
        public string Water { get; set; }
        public string Kcal { get; set; }
        public int Type { get; set; }
        public int EntityId { get; set; }

        #region
        public Macro( string prot, string carb, string fat, string water, int type, int entityId, string kcal)
        {
            this.Protein = prot;
            this.Carbs = carb;
            this.Fat = fat;
            this.Water = water;
            this.Type = type;
            this.EntityId = entityId;
            this.Kcal = kcal;
        }
        [ObsoleteAttribute("Esse método é usado para testes e como construtor padrão do entityFramework", false)]
        public Macro() { }
        #endregion
        #region
        public void setType(int type)
        {
            this.Type = type;
        }
        public void setEntityId(int entityId)
        {
            this.EntityId = entityId; 
        }
        #endregion
        #region
        public override bool Equals(object? obj)
        {
            if(obj == null) return false;
            if(obj.GetType() != this.GetType()) return false;
            Macro objectMacro = (Macro) obj as Macro;
            if(this.Protein == objectMacro.Protein && this.Kcal == objectMacro.Kcal && this.Carbs == objectMacro.Carbs && this.Water == objectMacro.Water && this.Fat == objectMacro.Fat)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region
        //Type:
        // 1 - User goal
        // 2 - Meal goal
        // 3 - food have
        //Entity Id could be:
        // UserID
        // FoodID
        // MealID
        #endregion
    }
}
