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

namespace PROJET_ASI.Pages.Logements
{
    [Authorize(Roles = "Administrateur,Proprietaire")]
    public class DeleteModel : PageModel
    {
        private readonly PROJET_ASI.Data.ApplicationDbContext _context;

        public DeleteModel(PROJET_ASI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Logement Logement { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logement = await _context.Logement
                .Include(logement => logement.Proprietaire)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (logement == null)
            {
                return NotFound();
            }
            else
            {
                Logement = logement;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logement = await _context.Logement.FindAsync(id);
            if (logement != null)
            {
                Logement = logement;
                _context.Logement.Remove(Logement);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
