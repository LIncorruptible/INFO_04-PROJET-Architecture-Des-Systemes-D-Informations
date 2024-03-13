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
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public Logement Logement { get; set; }
        [BindProperty]
        public Comporte Comporte { get; set; }

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Logement = await _context.Logement.FirstOrDefaultAsync(m => m.ID == (int)id);
            ViewData["LequipementID"] = new SelectList(_context.Equipement, "ID", "Nom_equipement");
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Comporte.Add(Comporte);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Comportes/Index", new { id = Comporte.LogementID });
        }
    }
}
