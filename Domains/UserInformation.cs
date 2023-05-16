namespace NutriNow.Domains
{
    public class UserInformation
    {
        public int UserInformationId { get; set; }
        public string AgeInterval { get; set; }
        public string Gender { get; set; }
        public string WorkingOutRoutine { get; set; }
        public string Goal { get; set; }
        public string HeightInterval { get; set; }
        public string WeightInterval { get; set; }
        public int UserId { get; set; }

        public UserInformation(int id, string age, string gender, string routine, string goal, string height, string weight)
        {
            this.AgeInterval = age;
            this.Gender = gender;   
            this.UserInformationId = id;
            this.WeightInterval = weight;
            this.Goal = goal;
            this.HeightInterval = height;
            this.WorkingOutRoutine = routine;
        }
        [ObsoleteAttribute("Esse método é usado para testes e como construtor padrão do entityFramework", false)]
        public UserInformation()
        {
                
        }
    }
}
