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

namespace PROJET_ASI.Pages.Equipements
{
    [Authorize(Roles = "Administrateur")]
    public class EditModel(PROJET_ASI.Data.ApplicationDbContext context) : PageModel
    {
        private readonly PROJET_ASI.Data.ApplicationDbContext _context = context;

        [BindProperty]
        public Equipement Equipement { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipement =  await _context.Equipement.FirstOrDefaultAsync(m => m.ID == id);
            if (equipement == null)
            {
                return NotFound();
            }
            Equipement = equipement;
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

            _context.Attach(Equipement).State = EntityState.Modified;

            try
            {
                bool unique = true;
                IList<Equipement> Equipements = await _context.Equipement.ToListAsync();

                if (Equipement.Nom_equipement != null)
                {
                    foreach (Equipement equipement in Equipements)
                    {
                        if (Equipement.Nom_equipement.Equals(equipement.Nom_equipement))
                            unique = false;
                    }
                }

                if (unique)
                {
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
                else
                {
                    ModelState.AddModelError(nameof(Equipement.Nom_equipement), "Le nom de l'équipement existe déjà.");
                    return Page();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipementExists(Equipement.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool EquipementExists(int id)
        {
            return _context.Equipement.Any(e => e.ID == id);
        }
    }
}
