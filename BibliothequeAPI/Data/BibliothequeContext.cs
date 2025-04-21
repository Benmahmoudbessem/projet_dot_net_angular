using Microsoft.EntityFrameworkCore;
using BibliothequeAPI.Models;

namespace BibliothequeAPI.Data
{
    public class BibliothequeContext : DbContext
    {
        public BibliothequeContext(DbContextOptions<BibliothequeContext> options)
            : base(options)
        {
        }

        public DbSet<Livre> Livres { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Emprunt> Emprunts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration des relations
            modelBuilder.Entity<Emprunt>()
                .HasOne(e => e.Livre)
                .WithMany(l => l.Emprunts)
                .HasForeignKey(e => e.LivreId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Emprunt>()
                .HasOne(e => e.Utilisateur)
                .WithMany(u => u.Emprunts)
                .HasForeignKey(e => e.UtilisateurId)
                .OnDelete(DeleteBehavior.Restrict);

            // Données de test (seed)
            modelBuilder.Entity<Livre>().HasData(
                new Livre { Id = 1, Titre = "Les Misérables", Auteur = "Victor Hugo", ISBN = "9782253096344", Categorie = "Roman", Annee = 1862, NombreExemplaires = 5, ExemplairesDisponibles = 5, Description = "Un chef-d'œuvre de la littérature française." },
                new Livre { Id = 2, Titre = "Le Petit Prince", Auteur = "Antoine de Saint-Exupéry", ISBN = "9782070612758", Categorie = "Conte", Annee = 1943, NombreExemplaires = 3, ExemplairesDisponibles = 3, Description = "Un conte poétique et philosophique." }
            );

            modelBuilder.Entity<Utilisateur>().HasData(
                new Utilisateur { Id = 1, Nom = "bessem", Prenom = "ben mahmoud", Email = "bessem@gmail.com", Telephone = "0123456789", MotDePasse = "motdepasse123", Adresse = "123 Rue Exemple", DateInscription = new DateTime(2025, 1, 1), EstActif = true }
            );
        }
    }
}
