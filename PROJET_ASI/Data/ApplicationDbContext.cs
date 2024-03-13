using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PROJET_ASI.Models;

namespace PROJET_ASI.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Proprietaire> Proprietaire { get; set; }
        public DbSet<Logement> Logement { get; set; } = default!;
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Equipement> Equipement { get; set; }
        public DbSet<Comporte> Comporte { get; set; }
    }
}
