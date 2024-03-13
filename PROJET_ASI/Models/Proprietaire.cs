using System.ComponentModel.DataAnnotations;

namespace PROJET_ASI.Models
{
    public class Proprietaire
    {
        public int ID { get; set; }
        [Required]
        public string? Nom { get; set; }
        [Required]
        public string? Prenom { get; set; }
        public override string ToString()
        {
            return ID + " : ( " + Nom + " , " + Prenom + " )";
        }
        public string NomComplet
        {
            get

            {
                return Nom + " " + Prenom;
            }

        }


        // Liste des logements dont il est le propriétaire
        public ICollection<Logement> LogementsAppartient { get; set; }

        // Données calculées non persistante
        // Pas de set
        [Display(Name = "Nombre de logements")]
        public int NbLogements
        {
            get
            {
                if (LogementsAppartient != null)
                    return LogementsAppartient.Count;
                else return -1;
            }
        }
    }
}
