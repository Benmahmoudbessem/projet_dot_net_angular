using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace BibliothequeAPI.Models
{
    public class Utilisateur
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nom { get; set; }

        [Required]
        [StringLength(50)]
        public string Prenom { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(15)]
        public string Telephone { get; set; }

        [Required]
        [StringLength(100)]
        public string MotDePasse { get; set; }

        [StringLength(200)]
        public string Adresse { get; set; }

        public DateTime DateInscription { get; set; }

        public bool EstActif { get; set; } = true;

        // Navigation properties
        [JsonIgnore]
        public virtual ICollection<Emprunt> Emprunts { get; set; }
        public Utilisateur()
        {
            Emprunts = new List<Emprunt>();
        }
    }
}
