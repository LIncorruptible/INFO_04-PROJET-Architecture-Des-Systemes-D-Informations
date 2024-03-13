using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using PROJET_ASI.Data;
using PROJET_ASI.Models;

namespace PROJET_ASI.Pages.Proprietaires
{
    [Authorize]
    public class IndexModel(PROJET_ASI.Data.ApplicationDbContext context) : PageModel
    {
        private readonly PROJET_ASI.Data.ApplicationDbContext _context = context;

        public IList<Proprietaire> Proprietaires { get;set; } = default!;

        //Permet le filtre
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public async Task OnGetAsync()
        {
            // Query pour les Proprietaires
            var proprioQuery = from proprietaire in _context.Proprietaire
                               select proprietaire;

            // Appliquer le filtre de recherche si SearchString n'est pas vide
            if (!string.IsNullOrEmpty(SearchString))
            {
                proprioQuery = proprioQuery.Where(s => s.Nom.Contains(SearchString));
            }

            // Charger les LogementsAppartient associés
            Proprietaires = await proprioQuery
                .Include(e => e.LogementsAppartient)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
