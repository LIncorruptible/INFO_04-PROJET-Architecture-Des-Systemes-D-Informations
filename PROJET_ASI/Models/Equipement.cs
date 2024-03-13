using System.ComponentModel.DataAnnotations;

namespace PROJET_ASI.Models
{
    public class Equipement
    {
        public int ID { get; set; }
        [Required]
        public string? Nom_equipement { get; set; }

        // Lien de navigation ManyToMany 
        [Display(Name = "Logements qui en ont un")]
        public ICollection<Comporte>? LesComporte { get; set; }
    }
}
