using Microsoft.EntityFrameworkCore;
using BibliothequeAPI.Data;
using BibliothequeAPI.Models;

namespace BibliothequeAPI.Repositories
{
    public class UtilisateurRepository
    {
        private readonly BibliothequeContext _context;

        public UtilisateurRepository(BibliothequeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Utilisateur>> GetAllUtilisateursAsync()
        {
            return await _context.Utilisateurs.ToListAsync();
        }

        public async Task<Utilisateur> GetUtilisateurByIdAsync(int id)
        {
            return await _context.Utilisateurs.FindAsync(id);
        }

        public async Task<Utilisateur> AddUtilisateurAsync(Utilisateur utilisateur)
        {
            utilisateur.DateInscription = DateTime.Now;
            _context.Utilisateurs.Add(utilisateur);
            await _context.SaveChangesAsync();
            return utilisateur;
        }

        public async Task<Utilisateur> UpdateUtilisateurAsync(Utilisateur utilisateur)
        {
            _context.Entry(utilisateur).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return utilisateur;
        }

        public async Task<bool> DeleteUtilisateurAsync(int id)
        {
            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur == null)
                return false;

            _context.Utilisateurs.Remove(utilisateur);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
