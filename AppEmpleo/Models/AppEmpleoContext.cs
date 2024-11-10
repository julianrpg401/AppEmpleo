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
            entity.HasKey(e => e.Id).HasName("PK__Candidat__3214EC07B4540503");

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
                .HasConstraintName("FK__Candidato__Id__0C85DE4D");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__Categori__06370DAD0B2F114B");

            entity.Property(e => e.Codigo).HasMaxLength(10);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Curriculum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Curricul__3214EC075D002124");

            entity.ToTable("Curriculum");

            entity.Property(e => e.FechaCarga)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreArchivo).HasMaxLength(255);
            entity.Property(e => e.RutaArchivo).HasMaxLength(500);

            entity.HasOne(d => d.Candidato).WithMany(p => p.Curriculums)
                .HasForeignKey(d => d.CandidatoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Curriculu__Candi__114A936A");
        });

        modelBuilder.Entity<Empleador>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Empleado__3214EC07384E7F9B");

            entity.ToTable("Empleador");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Empresa).WithMany(p => p.Empleadores)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Empleador__Empre__09A971A2");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Empleador)
                .HasForeignKey<Empleador>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Empleador__Id__08B54D69");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Empresa__3214EC075E695FD9");

            entity.ToTable("Empresa");

            entity.Property(e => e.Dirección).HasMaxLength(300);
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.SitioWeb).HasMaxLength(200);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<Habilidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Habilida__3214EC077D852756");

            entity.ToTable("Habilidad");

            entity.HasIndex(e => e.Nombre, "UQ__Habilida__75E3EFCFABB70B06").IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<OfertaHabilidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OfertaHa__3214EC0751B9F54B");

            entity.ToTable("OfertaHabilidad");

            entity.HasIndex(e => new { e.OfertaEmpleoId, e.HabilidadId }, "UC_Oferta_Habilidad").IsUnique();

            entity.HasOne(d => d.Habilidad).WithMany(p => p.OfertaHabilidades)
                .HasForeignKey(d => d.HabilidadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OfertaHab__Habil__1F98B2C1");

            entity.HasOne(d => d.OfertaEmpleo).WithMany(p => p.OfertaHabilidades)
                .HasForeignKey(d => d.OfertaEmpleoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OfertaHab__Ofert__1EA48E88");
        });

        modelBuilder.Entity<Oferta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Oferta__3214EC07BD202C1F");

            entity.Property(e => e.CategoriaCodigo).HasMaxLength(10);
            entity.Property(e => e.ModalidadTrabajo).HasMaxLength(20);
            entity.Property(e => e.NombreOferta).HasMaxLength(200);
            entity.Property(e => e.Salario).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UbicacionTrabajo).HasMaxLength(200);

            entity.HasOne(d => d.CategoriaCodigoNavigation).WithMany(p => p.Ofertas)
                .HasForeignKey(d => d.CategoriaCodigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Oferta__Categori__1AD3FDA4");

            entity.HasOne(d => d.Empleador).WithMany(p => p.Ofertas)
                .HasForeignKey(d => d.EmpleadorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Oferta__Empleado__19DFD96B");
        });

        modelBuilder.Entity<Postulacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Postulac__3214EC070AB5BA7E");

            entity.ToTable("Postulacion");

            entity.HasIndex(e => new { e.OfertaEmpleoId, e.CandidatoId }, "UC_Postulacion").IsUnique();

            entity.Property(e => e.FechaPostulacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Candidato).WithMany(p => p.Postulaciones)
                .HasForeignKey(d => d.CandidatoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Postulaci__Candi__25518C17");

            entity.HasOne(d => d.Curriculum).WithMany(p => p.Postulaciones)
                .HasForeignKey(d => d.CurriculumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Postulaci__Curri__2645B050");

            entity.HasOne(d => d.OfertaEmpleo).WithMany(p => p.Postulaciones)
                .HasForeignKey(d => d.OfertaEmpleoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Postulaci__Ofert__245D67DE");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC07EBEB3F99");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D105346AE90531").IsUnique();

            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.ClaveHash).HasMaxLength(255);
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
