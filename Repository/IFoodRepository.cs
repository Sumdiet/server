using NutriNow.Domains;

namespace NutriNow.Repository
{
    public interface IFoodRepository
    {
        Task<Food[]> GetFood();
    }
}
