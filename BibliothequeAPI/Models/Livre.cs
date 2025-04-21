using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BibliothequeAPI.Models
{
    public class Livre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Titre { get; set; }

        [Required]
        [StringLength(100)]
        public string Auteur { get; set; }

        [StringLength(20)]
        public string ISBN { get; set; }

        [StringLength(50)]
        public string Categorie { get; set; }

        public int Annee { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int NombreExemplaires { get; set; }

        public int ExemplairesDisponibles { get; set; }
        public Livre()
        {
            Emprunts = new List<Emprunt>();
        }

       
        [JsonIgnore]
        public virtual ICollection<Emprunt> Emprunts { get; set; }
    }
}
