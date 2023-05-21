using NutriNow.Domains;

namespace NutriNow.Repository
{
    public interface IMealRepository
    {
        Task<Meal[]> GetMeals();
    }
}
