using Microsoft.EntityFrameworkCore;
using NutriNow.Domains;
using NutriNow.Persistence;

namespace NutriNow.Repository
{
    public class FoodRepository: IFoodRepository
    {
        private readonly Context _context;

        public FoodRepository(Context context)
        {
            _context = context;

        }
        public async Task<Food[]> GetFood()
        {
            var query = _context.Food;
            query.AsNoTracking()
                .OrderBy(food => food.FoodId);
            return await query.ToArrayAsync();
        }
    }
}
