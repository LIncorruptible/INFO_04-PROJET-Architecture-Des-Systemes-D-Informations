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
using PROJET_ASI.Models.ViewModels;

namespace PROJET_ASI.Pages.Logements
{
    [Authorize(Roles = "Administrateur,Proprietaire")]
    public class CreateModel(PROJET_ASI.Data.ApplicationDbContext context) : PageModel
    {
        private readonly PROJET_ASI.Data.ApplicationDbContext _context = context;


        [BindProperty]
        public Logement Logement { get; set; } = default!;

        [BindProperty]
        public string[] selectedEquipementsIds { get; set; } = default!;

        public IActionResult OnGet()
        {
            ViewData["ProprietairesIDs"] = new SelectList(_context.Proprietaire, "ID", "NomComplet");
            ViewData["EquipementsIDs"] = new SelectList(_context.Equipement, "ID", "Nom_equipement");

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            /*if (!ModelState.IsValid)
            {
                return Page();
            }*/

            _context.Logement.Add(Logement);
            await _context.SaveChangesAsync();

            // Ajout des équipements sélectionnés
            foreach (var equipementId in selectedEquipementsIds)
            {
                Comporte comporte = new Comporte
                {
                    LogementID = Logement.ID,
                    EquipementID = int.Parse(equipementId),
                    Quantite = 1
                };

                _context.Comporte.Add(comporte);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
 