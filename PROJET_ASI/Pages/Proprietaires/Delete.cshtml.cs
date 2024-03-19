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

namespace PROJET_ASI.Pages.Proprietaires
{
    [Authorize(Roles = "Administrateur")]
    public class DeleteModel(PROJET_ASI.Data.ApplicationDbContext context) : PageModel
    {
        [BindProperty]
        public Proprietaire Proprietaire { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proprietaire = await context.Proprietaire.FirstOrDefaultAsync(m => m.ID == id);

            if (proprietaire == null)
            {
                return NotFound();
            }
            else
            {
                Proprietaire = proprietaire;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proprietaires = await context.Proprietaire.FindAsync(id);
            if (proprietaires != null)
            {
                Proprietaire = proprietaires;
                context.Proprietaire.Remove(Proprietaire);
                await context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
