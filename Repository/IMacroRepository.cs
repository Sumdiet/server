using NutriNow.Domains;

namespace NutriNow.Repository
{
    public interface IMacroRepository
    {
        public Task<Macro> GetMacroById(int macroId);
    }
}
