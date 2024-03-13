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

namespace PROJET_ASI.Pages.Logements
{
    [Authorize]
    public class IndexModel(PROJET_ASI.Data.ApplicationDbContext context) : PageModel
    {
        private readonly PROJET_ASI.Data.ApplicationDbContext _context = context;

        public IList<Logement> Logement { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Logement = await _context.Logement
                .Include(l => l.Proprietaire).ToListAsync();
        }
    }
}
