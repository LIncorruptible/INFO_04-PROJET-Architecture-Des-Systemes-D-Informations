using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PROJET_ASI.Data;
using PROJET_ASI.Models;

namespace PROJET_ASI.Pages.Reservations
{
    [Authorize(Roles = "Administrateur,Touriste")]
    public class CreateModel(PROJET_ASI.Data.ApplicationDbContext context) : PageModel
    {
        private readonly PROJET_ASI.Data.ApplicationDbContext _context = context;

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

            _context.Reservation.Add(Reservation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
