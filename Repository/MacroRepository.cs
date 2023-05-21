using Microsoft.EntityFrameworkCore;
using NutriNow.Domains;
using NutriNow.Persistence;

namespace NutriNow.Repository
{
    public class MacroRepository : IMacroRepository
    {
        private readonly Context _context;

        public MacroRepository(Context context)
        {
            _context = context;

        }
        public async Task<Macro> GetMacroById(int macroId)
        {
            var result = _context.Macro;

            result.AsNoTracking()
                .OrderBy(macro => macro.MacroId)
                .Where(macro => macro.MacroId == macroId);
            return await result!.FirstOrDefaultAsync();
        }
    }
}
