namespace NutriNow.DataTransferObj
{
    public class MacroDto
    {
        public string Protein { get; set; }
        public string Carbs { get; set; }
        public string Fat { get; set; }
        public string Water { get; set; }
        public string Kcal { get; set; }

        public MacroDto() {}
        public MacroDto(string protein, string carbs, string fat, string water, string kcal)
        {
            this.Protein = protein;
            this.Carbs = carbs;
            this.Fat = fat;
            this.Water = water;
            this.Kcal = kcal;
        }
    }
}
