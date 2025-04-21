using Microsoft.EntityFrameworkCore;
using BibliothequeAPI.Data;
using BibliothequeAPI.Models;

namespace BibliothequeAPI.Repositories
{
    public class EmpruntRepository
    {
        private readonly BibliothequeContext _context;

        public EmpruntRepository(BibliothequeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Emprunt>> GetAllEmpruntsAsync()
        {
            return await _context.Emprunts
                .Include(e => e.Livre)
                .Include(e => e.Utilisateur)
                .ToListAsync();
        }

        public async Task<Emprunt> GetEmpruntByIdAsync(int id)
        {
            return await _context.Emprunts
                .Include(e => e.Livre)
                .Include(e => e.Utilisateur)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Emprunt>> GetEmpruntsByUtilisateurAsync(int utilisateurId)
        {
            return await _context.Emprunts
                .Include(e => e.Livre)
                .Where(e => e.UtilisateurId == utilisateurId)
                .ToListAsync();
        }

        public async Task<Emprunt> AddEmpruntAsync(Emprunt emprunt)
        {
            // Vérifier si le livre est disponible
            var livre = await _context.Livres.FindAsync(emprunt.LivreId);
            if (livre.ExemplairesDisponibles <= 0)
                throw new InvalidOperationException("Ce livre n'est pas disponible pour l'emprunt.");

            // Mettre à jour le nombre d'exemplaires disponibles
            livre.ExemplairesDisponibles--;
            
            // Configurer l'emprunt
            emprunt.DateEmprunt = DateTime.Now;
            emprunt.DateRetourPrevue = DateTime.Now.AddDays(14); // 2 semaines par défaut
            emprunt.EstRendu = false;
            
            _context.Emprunts.Add(emprunt);
            await _context.SaveChangesAsync();
            return emprunt;
        }

        public async Task<Emprunt> RetournerEmpruntAsync(int id)
        {
            var emprunt = await _context.Emprunts
                .Include(e => e.Livre)
                .FirstOrDefaultAsync(e => e.Id == id);
                
            if (emprunt == null)
                return null;
                
            if (emprunt.EstRendu)
                return emprunt; // Déjà retourné
                
            // Marquer comme retourné
            emprunt.EstRendu = true;
            emprunt.DateRetourEffective = DateTime.Now;
            
            // Mettre à jour le nombre d'exemplaires disponibles
            var livre = emprunt.Livre;
            livre.ExemplairesDisponibles++;
            
            await _context.SaveChangesAsync();
            return emprunt;
        }

        public async Task<bool> DeleteEmpruntAsync(int id)
        {
            var emprunt = await _context.Emprunts.FindAsync(id);
            if (emprunt == null)
                return false;

            _context.Emprunts.Remove(emprunt);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
