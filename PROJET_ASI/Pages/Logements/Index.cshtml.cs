using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PROJET_ASI.Data;
using PROJET_ASI.Models;

namespace PROJET_ASI.Pages.Logements
{
    [Authorize]
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

        public IList<Logement> Logement { get;set; } = default!;
        public int? userID { get; set; } = null;
        public bool IsAdmin { get; set; } = false;

        public async Task OnGetAsync()
        {
            // On récupère l'ID de l'utilisateur connecté
            var user = await _userManager.GetUserAsync(User);
            // Convertir l'ID en int
            userID = int.Parse(user.Id);
            
            // Si l'utilisateur est un administrateur
            IsAdmin = User.IsInRole("Administrateur");

            if(IsAdmin)
            {
                // On récupère tous les logements
                Logement = await _context.Logement
                    .Include(l => l.Proprietaire)
                    .ToListAsync();
            } else
            {
                // On récupère les logements de l'utilisateur connecté
                Logement = await _context.Logement
                    .Include(l => l.Proprietaire)
                    .Where(l => l.ProprietaireID == userID)
                    .ToListAsync();
            }

        }
    }
}
