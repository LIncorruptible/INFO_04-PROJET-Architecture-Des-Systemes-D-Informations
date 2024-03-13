using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PROJET_ASI.Data;
using PROJET_ASI.Models;

namespace PROJET_ASI.Pages.Reservations
{
    [Authorize(Roles = "Administrateur,Proprietaire,Touriste")]
    public class DeleteModel : PageModel
    {
        private readonly PROJET_ASI.Data.ApplicationDbContext _context;

        public DeleteModel(PROJET_ASI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Reservation Reservation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(reservation => reservation.Logement)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (reservation == null)
            {
                return NotFound();
            }
            else
            {
                Reservation = reservation;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation != null)
            {
                Reservation = reservation;
                _context.Reservation.Remove(Reservation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
