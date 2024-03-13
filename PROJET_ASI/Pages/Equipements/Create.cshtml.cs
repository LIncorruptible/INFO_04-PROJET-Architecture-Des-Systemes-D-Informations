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
    [Authorize(Roles = "Administrateur,Proprietaire")]//Proprio peut demander mais admin doit valider
    public class CreateModel(PROJET_ASI.Data.ApplicationDbContext context) : PageModel
    {
        private readonly PROJET_ASI.Data.ApplicationDbContext _context = context;

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Equipement Equipement { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            /*if (!ModelState.IsValid)
            {
                return Page();
            }*/

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
                _context.Equipement.Add(Equipement);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(nameof(Equipement.Nom_equipement), "Le nom de l'équipement existe déjà.");
                return Page();
            }
        }
    }
}
