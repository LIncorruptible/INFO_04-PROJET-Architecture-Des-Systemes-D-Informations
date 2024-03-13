using System.ComponentModel.DataAnnotations;

namespace PROJET_ASI.Models
{
    public class Reservation
    {
        public int ID { get; set; }

        [Required]
        public DateTime DateDebut { get; set; }
        [Required]
        public DateTime DateFin { get; set; }

        // Clé étrangère vers le logement
        // Porte le nom du lien de navigation suivi du nom de la clé primaire de Logements
        public int LogementID { get; set; }
        // Lien de navigation
        public Logement? Logement { get; set; }
    }
}
