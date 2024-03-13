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

namespace PROJET_ASI.Pages.Comportes
{
    [Authorize(Roles = "Administrateur,Proprietaire")]
    public class EditModel : PageModel
    {
        private readonly PROJET_ASI.Data.ApplicationDbContext _context;

        public EditModel(PROJET_ASI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Comporte Comporte { get; set; } = default!;
        public Logement Logement { get; set; }
        public Equipement Equipement { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comporte =  await _context.Comporte.FirstOrDefaultAsync(m => m.ID == id);
            if (comporte == null)
            {
                return NotFound();
            }
            else
            {
                Comporte = comporte;
            }

            Logement = await _context.Logement.FirstOrDefaultAsync(l => l.ID == comporte.LogementID);
            List<Logement> logements = [Logement];
            ViewData["LeLogementId"] = new SelectList(logements, "ID", "Adresse");

            Equipement = await _context.Equipement.FirstOrDefaultAsync(e => e.ID == comporte.EquipementID);
            List<Equipement> equipements = [Equipement];
            ViewData["LequipementID"] = new SelectList(equipements, "ID", "Nom_equipement");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Comporte).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComporteExists(Comporte.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("../Comportes/Index", new { id = Comporte.LogementID });
        }

        private bool ComporteExists(int id)
        {
            return _context.Comporte.Any(e => e.ID == id);
        }
    }
}
