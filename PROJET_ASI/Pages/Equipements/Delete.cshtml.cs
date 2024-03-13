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

namespace PROJET_ASI.Pages.Equipements
{
    [Authorize(Roles = "Administrateur")]
    public class DeleteModel : PageModel
    {
        private readonly PROJET_ASI.Data.ApplicationDbContext _context;

        public DeleteModel(PROJET_ASI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Equipement Equipement { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipement = await _context.Equipement.FirstOrDefaultAsync(m => m.ID == id);

            if (equipement == null)
            {
                return NotFound();
            }
            else
            {
                Equipement = equipement;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipement = await _context.Equipement.FindAsync(id);
            if (equipement != null)
            {
                Equipement = equipement;
                _context.Equipement.Remove(Equipement);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
