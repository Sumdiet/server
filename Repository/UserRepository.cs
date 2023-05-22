using Microsoft.EntityFrameworkCore;
using NutriNow.Domains;
using NutriNow.Persistence;

namespace NutriNow.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            _context = context;

        }
        public async Task<User> GetUserAsyncByEmail(string email, DateTime date)
        {
            IQueryable<User> query = _context.User.Include(u => u.MacroGoal).Include(u=>u.Meals).ThenInclude(m=> m.MacroGoal).Include(u=> u.Meals).ThenInclude(m=>m.RegisteredFood).ThenInclude(r=>r.Food)
                .ThenInclude(f=>f.Macro);

            query = query.AsNoTracking()
                .OrderBy(user => user.UserId)
                .Where(user => user.Email == email);
            var result = await query.FirstOrDefaultAsync();
            foreach (Meal meal in result.Meals)
            {
                meal.RegisteredFood = meal.RegisteredFood.Where(r => r.Date.Day == date.Day).ToArray();
            }
            return result;
        }
        public async Task<User> GetUserAsyncById(int id, DateTime date)
        {
            IQueryable<User> query = _context.User.Include(u => u.MacroGoal).Include(u => u.Meals).ThenInclude(m => m.MacroGoal).Include(u => u.Meals).ThenInclude(m => m.RegisteredFood).ThenInclude(r => r.Food)
                .ThenInclude(f => f.Macro);
            query = query.AsNoTracking()
                .OrderBy(user => user.UserId)
                .Where(user => user.UserId == id);
            var result = await query.FirstOrDefaultAsync();
            foreach(Meal meal in result.Meals)
            {
                meal.RegisteredFood = meal.RegisteredFood.Where(r => r.Date == date).ToArray();
            }

            return result;
        }
    }
}
