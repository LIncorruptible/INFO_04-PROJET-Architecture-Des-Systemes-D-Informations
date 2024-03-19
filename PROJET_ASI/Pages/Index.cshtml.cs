using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PROJET_ASI.Models;
using PROJET_ASI.Models.ViewModels;

namespace PROJET_ASI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly PROJET_ASI.Data.ApplicationDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, PROJET_ASI.Data.ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Variable pour stocker les logements
        public IList<Logement> Logements { get; set; } = default!;

        // Variable pour stocker les équipements de chaque logement
        public IList<LogementEquipements> logementEquipements { get; set; } = default!;

        public void OnGet()
        {
            // On récupère les logements
            Logements = _context.Logement.ToList();

            // On récupère les équipements de chaque logement
            logementEquipements = new List<LogementEquipements>();

            foreach (var logement in Logements)
            {
                var equipements = _context.Comporte
                    .Where(c => c.LogementID == logement.ID)
                    .Select(c => c.Equipement)
                    .ToList();

                logementEquipements.Add(new LogementEquipements
                {
                    Logement = logement,
                    Equipements = equipements
                });
            }

        }
    }
}
