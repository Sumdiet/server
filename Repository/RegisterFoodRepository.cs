using Microsoft.EntityFrameworkCore;
using NutriNow.Domains;
using NutriNow.Persistence;

namespace NutriNow.Repository
{
    public class RegisterFoodRepository: IRegisterFoodRepository
    {
        private readonly Context _context;

        public RegisterFoodRepository(Context context)
        {
            _context = context;

        }
        public async Task<RegisteredFood> GetRegisteredFoodAsyncById(int id)
        {
            IQueryable<RegisteredFood> query = _context.RegisteredFood;
            query = query.AsNoTracking()
                .OrderBy(registerFood => registerFood.RegisteredFoodId)
                .Where(registerFood => registerFood.RegisteredFoodId == id);
            var result = await query.FirstOrDefaultAsync();
            

            return result;
        }
    }
}
