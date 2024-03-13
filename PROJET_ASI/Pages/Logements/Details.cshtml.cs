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
    [Authorize]
    public class DetailsModel(PROJET_ASI.Data.ApplicationDbContext context) : PageModel
    {
        private readonly PROJET_ASI.Data.ApplicationDbContext _context = context;

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
    }
}
