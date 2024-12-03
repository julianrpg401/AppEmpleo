using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AppEmpleo.Models;

public partial class AppEmpleoContext : DbContext
{
    public AppEmpleoContext()
    {
    }

    public AppEmpleoContext(DbContextOptions<AppEmpleoContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Reclutador> Reclutadores { get; set; }
    public DbSet<Candidato> Candidatos { get; set; }
    public DbSet<Curriculum> Curriculums { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Habilidad> Habilidades { get; set; }
    public DbSet<Oferta> Ofertas { get; set; }
    public DbSet<OfertaHabilidad> OfertaHabilidades { get; set; }
    public DbSet<Postulacion> Postulaciones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurar las relaciones de las claves foráneas
        modelBuilder.Entity<Postulacion>()
            .HasOne(p => p.Candidato)
            .WithMany(c => c.Postulaciones)
            .HasForeignKey(p => p.CandidatoId)
            .OnDelete(DeleteBehavior.NoAction); // No eliminar en cascada

        modelBuilder.Entity<Postulacion>()
            .HasOne(p => p.Curriculum)
            .WithMany(c => c.Postulaciones)
            .HasForeignKey(p => p.CurriculumId)
            .OnDelete(DeleteBehavior.NoAction); // No eliminar en cascada

        modelBuilder.Entity<Postulacion>()
            .HasOne(p => p.OfertaEmpleo)
            .WithMany(o => o.Postulaciones)
            .HasForeignKey(p => p.OfertaEmpleoId)
            .OnDelete(DeleteBehavior.NoAction); // No eliminar en cascada

        // Configuración adicional de otras entidades si es necesario...
    }
}
