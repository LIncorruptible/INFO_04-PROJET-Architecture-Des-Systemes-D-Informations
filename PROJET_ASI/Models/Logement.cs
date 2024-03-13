using System.ComponentModel.DataAnnotations;

namespace PROJET_ASI.Models
{
    public class Logement
    {
        public int ID { get; set; }
        [Required]
        public string? Adresse { get; set; }
        [Required]
        public float? Prix { get; set; }
        [Required]
        public float? Superficie { get; set; }
        [Required]
        public int? Nb_chambres { get; set; }

        // Clé étrangère vers le propriétaire du logement
        // Porte le nom du lien de navigation suivi du nom de la clé primaire de Proprietaires
        public int ProprietaireID { get; set; }
        // Lien de navigation
        public Proprietaire? Proprietaire { get; set; }

        // Lien de navigation ManyToMany 
        [Display(Name = "Equipements du logement")]
        public ICollection<Comporte> LesComporte { get; set; }
    }
}
