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
        public async Task<Macro> GetMacroById(Guid macroId)
        {
            var result = _context.Macro;

            result.AsNoTracking();
          
            var query = await result!.ToArrayAsync();
            return query.Where(q => q.MacroId == macroId).FirstOrDefault();
        }
    }
}
