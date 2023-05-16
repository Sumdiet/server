using NutriNow.Domains;

namespace NutriNow.Repository
{
    public interface IRegisterFoodRepository
    {
        Task<RegisteredFood> GetRegisteredFoodAsyncById(int id);
    }
}
