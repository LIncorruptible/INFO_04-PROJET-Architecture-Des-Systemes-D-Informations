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

namespace PROJET_ASI.Pages.Comportes
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
        public Comporte Comporte { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comporte = await _context.Comporte
                .Include(c => c.Logement)
                .Include(c => c.Equipement)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (comporte == null)
            {
                return NotFound();
            }
            else
            {
                Comporte = comporte;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comporte = await _context.Comporte.FindAsync(id);
            if (comporte != null)
            {
                Comporte = comporte;
                _context.Comporte.Remove(Comporte);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
