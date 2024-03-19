namespace PROJET_ASI.Models.ViewModels
{
    public class LogementEquipements
    {
        // Logement
        public Logement Logement { get; set; } = default!;
        // Et ses équipements
        public List<Equipement> Equipements { get; set; } = default!;
    }
}
