using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ExpressVoituresApp.Models;

namespace ExpressVoituresApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Tables métiers
        public DbSet<Vehicule> Vehicules { get; set; }
        public DbSet<Achat> Achats { get; set; }
        public DbSet<Vente> Ventes { get; set; }
        public DbSet<Reparation> Reparations { get; set; }
        public DbSet<Annonce> Annonces { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // indispensable avec Identity

            // Vehicule 1..1 -> Achat
            modelBuilder.Entity<Vehicule>()
                .HasOne(v => v.Achat)
                .WithOne(a => a.Vehicule)
                .HasForeignKey<Achat>(a => a.VehiculeId);

            // Vehicule 0..1 -> Vente
            modelBuilder.Entity<Vehicule>()
                .HasOne(v => v.Vente)
                .WithOne(ve => ve.Vehicule)
                .HasForeignKey<Vente>(ve => ve.VehiculeId);

            // Vehicule 0..1 -> Annonce
            modelBuilder.Entity<Vehicule>()
                .HasOne(v => v.Annonce)
                .WithOne(an => an.Vehicule)
                .HasForeignKey<Annonce>(an => an.VehiculeId);

            // Vehicule 1..n -> Réparation
            modelBuilder.Entity<Vehicule>()
                .HasMany(v => v.Reparations)
                .WithOne(r => r.Vehicule)
                .HasForeignKey(r => r.VehiculeId);
        }
    }
}
