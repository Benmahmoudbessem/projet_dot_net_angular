using Microsoft.EntityFrameworkCore;
using BibliothequeAPI.Data;
using BibliothequeAPI.Models;

namespace BibliothequeAPI.Repositories
{
    public class LivreRepository
    {
        private readonly BibliothequeContext _context;

        public LivreRepository(BibliothequeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Livre>> GetAllLivresAsync()
        {
            return await _context.Livres.ToListAsync();
        }

        public async Task<Livre> GetLivreByIdAsync(int id)
        {
            return await _context.Livres.FindAsync(id);
        }

        public async Task<Livre> AddLivreAsync(Livre livre)
        {
            _context.Livres.Add(livre);
            await _context.SaveChangesAsync();
            return livre;
        }

        public async Task<Livre> UpdateLivreAsync(Livre livre)
        {
            _context.Entry(livre).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return livre;
        }

        public async Task<bool> DeleteLivreAsync(int id)
        {
            var livre = await _context.Livres.FindAsync(id);
            if (livre == null)
                return false;

            _context.Livres.Remove(livre);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
