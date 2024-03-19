using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PROJET_ASI.Data;
using PROJET_ASI.Models;

namespace PROJET_ASI.Pages.Reservations
{
    [Authorize]
    public class CreateModel : Base
    {

        public CreateModel(
            ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager
            ) : base(context, authorizationService, userManager)
        {
            _context = context;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        private readonly PROJET_ASI.Data.ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<IdentityUser> _userManager;

        public IActionResult OnGet()
        {
        ViewData["LogementID"] = new SelectList(_context.Logement, "ID", "Adresse");
            return Page();
        }

        [BindProperty]
        public Reservation Reservation { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Reservation.DateFin < Reservation.DateDebut)
            {
                ModelState.AddModelError(nameof(Reservation.DateDebut), "La date de début doit être antérieure à la date de fin.");
                return Page();
            }

            // On associe l'utilisateur connecté à la réservation
            Reservation.UserID = _userManager.GetUserId(User);

            _context.Reservation.Add(Reservation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
