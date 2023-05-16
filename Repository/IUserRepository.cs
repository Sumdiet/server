using NutriNow.Domains;

namespace NutriNow.Repository
{
    public interface IUserRepository
    {
        public Task<User> GetUserAsyncByEmail(string email,  DateTime date);
        public Task<User> GetUserAsyncById(int id, DateTime date);
    }
}
