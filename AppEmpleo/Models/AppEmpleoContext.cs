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

    public virtual DbSet<Candidato> Candidatos { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Curriculum> Curriculums { get; set; }

    public virtual DbSet<Empleador> Empleadores { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Habilidad> Habilidades { get; set; }

    public virtual DbSet<OfertaHabilidad> OfertaHabilidades { get; set; }

    public virtual DbSet<Oferta> Ofertas { get; set; }

    public virtual DbSet<Postulacion> Postulaciones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidato>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Candidat__3214EC0769FC2A81");

            entity.ToTable("Candidato");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Curp)
                .HasMaxLength(18)
                .HasColumnName("CURP");
            entity.Property(e => e.Dirección).HasMaxLength(300);
            entity.Property(e => e.Telefono).HasMaxLength(20);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Candidato)
                .HasForeignKey<Candidato>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Candidato__Id__4222D4EF");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__Categori__06370DAD985B2763");

            entity.Property(e => e.Codigo).HasMaxLength(10);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Curriculum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Curricul__3214EC07DFC3549A");

            entity.ToTable("Curriculum");

            entity.Property(e => e.FechaCarga)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreArchivo).HasMaxLength(255);
            entity.Property(e => e.RutaArchivo).HasMaxLength(500);

            entity.HasOne(d => d.Candidato).WithMany(p => p.Curricula)
                .HasForeignKey(d => d.CandidatoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Curriculu__Candi__46E78A0C");
        });

        modelBuilder.Entity<Empleador>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Empleado__3214EC073B425ACB");

            entity.ToTable("Empleador");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Empresa).WithMany(p => p.Empleadores)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Empleador__Empre__3F466844");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Empleador)
                .HasForeignKey<Empleador>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Empleador__Id__3E52440B");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Empresa__3214EC076CA6B446");

            entity.ToTable("Empresa");

            entity.Property(e => e.Dirección).HasMaxLength(300);
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.SitioWeb).HasMaxLength(200);
            entity.Property(e => e.Teléfono).HasMaxLength(20);
        });

        modelBuilder.Entity<Habilidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Habilida__3214EC07313E3FE0");

            entity.HasIndex(e => e.Nombre, "UQ__Habilida__75E3EFCF3A815BBF").IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<OfertaHabilidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OfertaHa__3214EC07D82250B9");

            entity.HasIndex(e => new { e.OfertaEmpleoId, e.HabilidadId }, "UC_Oferta_Habilidad").IsUnique();

            entity.HasOne(d => d.Habilidades).WithMany(p => p.OfertaHabilidades)
                .HasForeignKey(d => d.HabilidadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OfertaHab__Habil__5535A963");

            entity.HasOne(d => d.OfertaEmpleo).WithMany(p => p.OfertaHabilidades)
                .HasForeignKey(d => d.OfertaEmpleoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OfertaHab__Ofert__5441852A");
        });

        modelBuilder.Entity<Oferta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Oferta__3214EC0734F9C979");

            entity.Property(e => e.CategoriaCodigo).HasMaxLength(10);
            entity.Property(e => e.ModalidadTrabajo).HasMaxLength(20);
            entity.Property(e => e.NombreOferta).HasMaxLength(200);
            entity.Property(e => e.Salario).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UbicacionTrabajo).HasMaxLength(200);

            entity.HasOne(d => d.CategoriaCodigoNavigation).WithMany(p => p.Ofertas)
                .HasForeignKey(d => d.CategoriaCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Oferta__Categori__5070F446");

            entity.HasOne(d => d.Empleador).WithMany(p => p.Ofertas)
                .HasForeignKey(d => d.EmpleadorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Oferta__Empleado__4F7CD00D");
        });

        modelBuilder.Entity<Postulacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Postulac__3214EC074E44ECF6");

            entity.HasIndex(e => new { e.OfertaEmpleoId, e.CandidatoId }, "UC_Postulacion").IsUnique();

            entity.Property(e => e.FechaPostulacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Candidato).WithMany(p => p.Postulaciones)
                .HasForeignKey(d => d.CandidatoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Postulaci__Candi__5AEE82B9");

            entity.HasOne(d => d.Curriculum).WithMany(p => p.Postulaciones)
                .HasForeignKey(d => d.CurriculumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Postulaci__Curri__5BE2A6F2");

            entity.HasOne(d => d.OfertaEmpleo).WithMany(p => p.Postulaciones)
                .HasForeignKey(d => d.OfertaEmpleoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Postulaci__Ofert__59FA5E80");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC07DCFF04D7");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D1053427F635C8").IsUnique();

            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.ContraseñaHash).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Rol).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
