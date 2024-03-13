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

namespace PROJET_ASI.Pages.Equipements
{
    [Authorize(Roles = "Administrateur,Proprietaire,Touriste")]
    public class IndexModel : PageModel
    {
        private readonly PROJET_ASI.Data.ApplicationDbContext _context;

        public IndexModel(PROJET_ASI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Equipement> Equipement { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Equipement = await _context.Equipement.ToListAsync();
        }
    }
}
