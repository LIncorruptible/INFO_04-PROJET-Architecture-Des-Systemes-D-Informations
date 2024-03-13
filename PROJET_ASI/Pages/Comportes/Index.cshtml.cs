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

namespace PROJET_ASI.Pages.Comportes
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly PROJET_ASI.Data.ApplicationDbContext _context;

        public IndexModel(PROJET_ASI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Comporte> Comporte { get;set; } = default!;

        public int LogementID { get; set; }
        public Logement Logement { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Récupération de l'ID de l'enseignant dont on veut gérer les UEs
            LogementID = (int)id;
            // Récupération des UEs de cet enseignant
            Logement = _context.Logement.Find(id);
            // Récupération des UEs de cet enseignant
            Comporte = await _context.Comporte
            .Include(e => e.Logement).Where(e => e.Logement.ID == id)
            .Include(e => e.Equipement).ToListAsync();
            if (Comporte == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
