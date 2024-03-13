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

namespace PROJET_ASI.Pages.Logements
{
    [Authorize(Roles = "Administrateur,Proprietaire")]
    public class CreateModel(PROJET_ASI.Data.ApplicationDbContext context) : PageModel
    {
        private readonly PROJET_ASI.Data.ApplicationDbContext _context = context;

        public IActionResult OnGet()
        {
            ViewData["ProprietaireID"] = new SelectList(_context.Proprietaire, "ID", "NomComplet");
            return Page();
        }

        [BindProperty]
        public Logement Logement { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            /*if (!ModelState.IsValid)
            {
                return Page();
            }*/

            _context.Logement.Add(Logement);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
