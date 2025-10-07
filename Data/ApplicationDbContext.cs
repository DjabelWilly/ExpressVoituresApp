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

        // Tables
        public DbSet<Vehicule> Vehicules { get; set; }
        public DbSet<Achat> Achats { get; set; }
        public DbSet<Vente> Ventes { get; set; }
        public DbSet<Reparation> Reparations { get; set; }
        public DbSet<Annonce> Annonces { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- Relations entre entités ---

            // Véhicule 1..1 -> Achat
            modelBuilder.Entity<Vehicule>()
                .HasOne(v => v.Achat)
                .WithOne(a => a.Vehicule)
                .HasForeignKey<Achat>(a => a.VehiculeId);

            // Véhicule 0..1 -> Vente
            modelBuilder.Entity<Vehicule>()
                .HasOne(v => v.Vente)
                .WithOne(ve => ve.Vehicule)
                .HasForeignKey<Vente>(ve => ve.VehiculeId);

            // Véhicule 0..1 -> Annonce
            modelBuilder.Entity<Vehicule>()
                .HasOne(v => v.Annonce)
                .WithOne(an => an.Vehicule)
                .HasForeignKey<Annonce>(an => an.VehiculeId);

            // Véhicule 1..n -> Réparations
            modelBuilder.Entity<Vehicule>()
                .HasMany(v => v.Reparations)
                .WithOne(r => r.Vehicule)
                .HasForeignKey(r => r.VehiculeId);

            // --- Configuration des types de données ---

            modelBuilder.Entity<Achat>()
                .Property(a => a.Prix)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Vente>()
                .Property(v => v.Prix)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Reparation>()
                .Property(r => r.Cout)
                .HasColumnType("decimal(10,2)");
        }
    }
}
