using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using PROJET_ASI.Data;
using PROJET_ASI.Models;

namespace PROJET_ASI.Pages.Proprietaires
{
    [Authorize (Roles = "Administrateur, Proprietaire")]
    public class IndexModel : Base
    {
        private readonly PROJET_ASI.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel
            (
                PROJET_ASI.Data.ApplicationDbContext context,
                IAuthorizationService authorizationService,
                UserManager<IdentityUser> userManager
            ) : base(context, authorizationService, userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Proprietaire> Proprietaires { get;set; } = default!;

        public int? proprietaireID { get; set; } = null;

        public bool IsAdmin { get; set; } = false;

        //Permet le filtre
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public async Task OnGetAsync()
        {
            // Si l'utilisateur est un administrateur
            IsAdmin = User.IsInRole("Administrateur");

            // On récupère l'ID de l'utilisateur connecté
            var user = await _userManager.GetUserAsync(User);

            if (IsAdmin)
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
            } else
            {
                // On récupère l'ID de l'utilisateur connecté
                var userID = int.Parse(user.Id);
                // On récupère l'ID du propriétaire associé à l'utilisateur connecté
                proprietaireID = _context.Proprietaire
                    .Where(p => p.UserID == userID)
                    .Select(p => p.ID)
                    .FirstOrDefault();
            }
        }
    }
}
