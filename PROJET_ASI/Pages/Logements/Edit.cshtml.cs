using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROJET_ASI.Data;
using PROJET_ASI.Models;

namespace PROJET_ASI.Pages.Logements
{
    [Authorize(Roles = "Administrateur,Proprietaire")]
    public class EditModel(PROJET_ASI.Data.ApplicationDbContext context) : PageModel
    {
        private readonly PROJET_ASI.Data.ApplicationDbContext _context = context;

        [BindProperty]
        public Logement Logement { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logement =  await _context.Logement.FirstOrDefaultAsync(m => m.ID == id);
            if (logement == null)
            {
                return NotFound();
            }
            Logement = logement;
           ViewData["ProprietaireID"] = new SelectList(_context.Proprietaire, "ID", "NomComplet");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            /*if (!ModelState.IsValid)
            {
                return Page();
            }*/

            _context.Attach(Logement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogementExists(Logement.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LogementExists(int id)
        {
            return _context.Logement.Any(e => e.ID == id);
        }
    }
}
