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

namespace PROJET_ASI.Pages.Reservations
{
    [Authorize]
    public class IndexModel : Base
    {
        private readonly PROJET_ASI.Data.ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(
            PROJET_ASI.Data.ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager
            ) : base(context, authorizationService, userManager)
        {
            _context = context;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        public IList<Reservation> Reservation { get;set; } = default!;

        public async Task OnGetAsync()
        {
            // Si l'utilisateur est un administrateur, on récupère toutes les réservations
            if (User.IsInRole("Administrateur"))
            {
                // On récupère toutes les réservations
                Reservation = await _context.Reservation
                    .Include(reservation => reservation.Logement)
                    .ToListAsync();
            }
            else
            {
                // Si l'utilisateur est un utilisateur standard, on ne récupère que ses réservations
                if (!User.IsInRole("Proprietaire"))
                {
                    // On récupère toutes les réservations liées à l'utilisateur
                    Reservation = await _context.Reservation
                        .Include(reservation => reservation.Logement)
                        .Where(reservation => reservation.UserID == _userManager.GetUserId(User))
                        .ToListAsync();
                } else
                {
                    // On récupère le propriétaire lié à l'utilisateur
                    var proprietaire = await _context.Proprietaire
                        .FirstOrDefaultAsync(prop => prop.UserID == int.Parse(_userManager.GetUserId(User)));

                    // On récupère les logements liés au propriétaire
                    var logements = await _context.Logement
                        .Where(logement => logement.ProprietaireID == proprietaire.ID)
                        .ToListAsync();

                    if (logements.Count == 0)
                    {
                        Reservation = new List<Reservation>();
                        return;
                    }

                    // On récupère les réservations liées aux logements
                    Reservation = await _context.Reservation
                        .Include(reservation => reservation.Logement)
                        .Where(reservation => logements.Contains(reservation.Logement))
                        .ToListAsync();
                }
            }
        }
    }
}
