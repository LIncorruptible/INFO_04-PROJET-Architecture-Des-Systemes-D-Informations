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

        [BindProperty]
        public string[] selectedEquipementsIds { get; set; } = default!;

        public IList<Equipement> Equipements { get; set; } = default!;

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

            // On récupère les équipements du logement
            var equipements = from e in _context.Equipement
                              join c in _context.Comporte on e.ID equals c.EquipementID
                              where c.LogementID == id
                              select e;

            Equipements = await equipements.ToListAsync();

            // On créé une liste d'équipements sélectionnés
            selectedEquipementsIds = Equipements.Select(e => e.ID.ToString()).ToArray();

            ViewData["EquipementsIds"] = new SelectList(_context.Equipement, "ID", "Nom_equipement");
            ViewData["ProprietairesIDs"] = new SelectList(_context.Proprietaire, "ID", "NomComplet");

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
                // On supprime les équipements du logement
                var equipements = from c in _context.Comporte
                                  where c.LogementID == Logement.ID
                                  select c;

                _context.Comporte.RemoveRange(equipements);

                // On ajoute les nouveaux équipements
                foreach (var equipementId in selectedEquipementsIds)
                {
                    Comporte comporte = new Comporte
                    {
                        LogementID = Logement.ID,
                        EquipementID = int.Parse(equipementId),
                        Quantite = 1
                    };

                    _context.Comporte.Add(comporte);
                }   

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
