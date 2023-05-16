using Microsoft.EntityFrameworkCore;
using NutriNow.Domains;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace NutriNow.Persistence
{
    public class Context: DbContext
    {
        public IConfiguration _config { get; }
        public Context(DbContextOptions<Context> options, IConfiguration config) : base(options)
        {
            this._config = config;
        }

        public DbSet<Food> Food { get; set; }
        public DbSet<Macro> Macro { get; set; }
        public DbSet<Meal> Meal { get; set; }
        public DbSet<RegisteredFood> RegisteredFood { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserInformation> UserInformation { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public string ObterCaminhoConexao()
        {
            return _config.GetConnectionString("ConnectionDatabase");
        }
    }
}
