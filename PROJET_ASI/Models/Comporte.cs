using System.ComponentModel.DataAnnotations;

namespace PROJET_ASI.Models
{
    public class Comporte
    {
        public int ID { get; set; }
        [Required]
        public int? Quantite { get; set; }

        // Clé étrangère vers le logement
        // Porte le nom du lien de navigation suivi du nom de la clé primaire de Logements
        public int LogementID { get; set; }
        // Lien de navigation
        public Logement? Logement { get; set; }

        // Clé étrangère vers l'équipement
        // Porte le nom du lien de navigation suivi du nom de la clé primaire de Equipements
        public int EquipementID { get; set; }
        // Lien de navigation
        public Equipement? Equipement { get; set; }
    }
}
