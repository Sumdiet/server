using Konscious.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using NutriNow.DataTransferObj;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace NutriNow.Domains
{
    public class User
    {
        [Key()]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public UserInformation? UserInformation { get; set; }
        public ICollection<Meal>? Meals { get; set; }
        public Macro? MacroGoal { get; set; }
        [NotMapped]
        public MacroDto? CurrentMacro { get; set; } = new MacroDto();

        public User(int userId, string userName, string email, string password, UserInformation? userInformation, Macro? macroGoal, ICollection<Meal>? meal)
        {
            this.UserId = userId;
            this.UserName = userName;   
            this.Email = email;
            this.Password = this.HashPassword(password);
            this.UserInformation = userInformation;
            this.MacroGoal = macroGoal;
            this.Meals = meal;
            CountingMacro();
        }
        [ObsoleteAttribute("Esse método é usado para testes e como construtor padrão do entityFramework", false)]
        public User()
        {

        }
        public User(string password, string email, string username)
        {
            this.UserName = username;
            this.Email = email;
            this.Password = this.HashPassword(password);
        }
        private byte[] CreateSalt()
        {
            var buffer = Encoding.ASCII.GetBytes(this.UserName);
            return buffer;
        }

        public byte[] HashPassword(string password)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));

            argon2.Salt = this.CreateSalt();
            argon2.DegreeOfParallelism = 1; 
            argon2.Iterations = 1;
            argon2.MemorySize = 4;

            return argon2.GetBytes(16);
        }


        public string CreateTokenJwt(string chaveSecreta, string audiencia, string emissor, int tempoExpiracaoEmMinutos)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var header = new JwtHeader(credentials);

            var payload = new JwtPayload
        {
            {"aud", audiencia},
            {"iss", emissor},
            {"exp", DateTimeOffset.UtcNow.AddMinutes(tempoExpiracaoEmMinutos).ToUnixTimeSeconds()},
            {"iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds()}
        };

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public bool VerifyHash(string password)
        {
            var newHash = HashPassword(password);
            return this.Password.SequenceEqual(newHash);
        }

        public void CountingMacro()
        {
            if (this.Meals == null) return;
            foreach(var meal in this.Meals!)
            {
                if (meal.RegisteredFood!.Count != 0)
                {
                    foreach(var foodRegistred in meal.RegisteredFood!)
                    {
                        foreach (var key in typeof(MacroDto).GetProperties())
                        {
                            var result = key.GetValue(foodRegistred.CurrentMacro);
                            if (result != null)
                            {
                                var value = key.GetValue(this.CurrentMacro);
                                if (value != null)
                                {
                                    key.SetValue(this.CurrentMacro, (Convert.ToDouble(result) + Convert.ToDouble(value)).ToString());
                                } else
                                {
                                    key.SetValue(this.CurrentMacro, result);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
