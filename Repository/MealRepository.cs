using NutriNow.Domains;
using NutriNow.Persistence;using Microsoft.EntityFrameworkCore;

namespace NutriNow.Repository
{
    public class MealRepository : IMealRepository
    {
        private readonly Context _context;

        public MealRepository(Context context)
        {
            _context = context;

        }
        public async Task<Meal[]> GetMeals()
        {
            var query = _context.Meal;
            query.AsNoTracking()
                .OrderBy(meal => meal.MealId);
            return await query.ToArrayAsync();
        }
    }
}
