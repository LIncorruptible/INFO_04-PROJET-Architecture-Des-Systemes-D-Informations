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

namespace PROJET_ASI.Pages.Proprietaires
{
    [Authorize(Roles = "Administrateur")]
    public class CreateModel(PROJET_ASI.Data.ApplicationDbContext context) : PageModel
    {
        private readonly PROJET_ASI.Data.ApplicationDbContext _context = context;

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Proprietaire Proprietaire { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            /*if (!ModelState.IsValid)
            {
                return Page();
            }*/

            _context.Proprietaire.Add(Proprietaire);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
