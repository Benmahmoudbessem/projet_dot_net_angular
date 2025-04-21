using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace BibliothequeAPI.Models
{
    public class Emprunt
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int LivreId { get; set; }

        [Required]
        public int UtilisateurId { get; set; }

        [Required]
        public DateTime DateEmprunt { get; set; }

        public DateTime DateRetourPrevue { get; set; }

        public DateTime? DateRetourEffective { get; set; }

        public bool EstRendu { get; set; } = false;

        [StringLength(500)]
        public string Commentaire { get; set; }

        // Navigation properties
        [JsonIgnore]
        [ForeignKey("LivreId")]
        [ValidateNever]
        public virtual Livre Livre { get; set; }

        [JsonIgnore]
        [ForeignKey("UtilisateurId")]
        [ValidateNever]
        public virtual Utilisateur Utilisateur { get; set; }

        public Emprunt()
        {
        }
    }
}
