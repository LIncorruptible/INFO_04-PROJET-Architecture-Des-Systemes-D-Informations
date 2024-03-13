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
    [Authorize]
    public class DetailsModel(PROJET_ASI.Data.ApplicationDbContext context) : PageModel
    {
        private readonly PROJET_ASI.Data.ApplicationDbContext _context = context;

        public Proprietaire Proprietaire { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var proprietaires = await _context.Proprietaire.FirstOrDefaultAsync(m => m.ID == id);
            var proprietaires = await _context.Proprietaire
                .Include(f => f.LogementsAppartient)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (proprietaires == null)
            {
                return NotFound();
            }
            else
            {
                Proprietaire = proprietaires;
            }
            return Page();
        }
    }
}
