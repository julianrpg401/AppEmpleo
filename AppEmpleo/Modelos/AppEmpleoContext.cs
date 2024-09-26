using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AppEmpleo.Modelos;

public partial class AppEmpleoContext : DbContext
{
    public AppEmpleoContext()
    {
    }

    public AppEmpleoContext(DbContextOptions<AppEmpleoContext> options) : base(options)
    {
    }

    public virtual DbSet<Candidato> Candidatos { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Empleador> Empleadores { get; set; }

    public virtual DbSet<Oferta> Ofertas { get; set; }

    public virtual DbSet<Postulacion> Postulaciones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("SqlConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidato>(entity =>
        {
            entity.HasKey(e => e.IdCandidato).HasName("PK__candidat__F84D9A3C11F242F2");

            entity.ToTable("candidato");

            entity.Property(e => e.IdCandidato).HasColumnName("idCandidato");
            entity.Property(e => e.Curriculum).HasColumnName("curriculum");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__categori__8A3D240C2979A577");

            entity.ToTable("categoria");

            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nombreCategoria");
        });

        modelBuilder.Entity<Empleador>(entity =>
        {
            entity.HasKey(e => e.IdEmpleador).HasName("PK__empleado__42034B0DAB174683");

            entity.ToTable("empleador");

            entity.Property(e => e.IdEmpleador).HasColumnName("idEmpleador");
            entity.Property(e => e.EmailEmpresa)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("emailEmpresa");
            entity.Property(e => e.NombreEmpresa)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombreEmpresa");
            entity.Property(e => e.Sector)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("sector");
        });

        modelBuilder.Entity<Oferta>(entity =>
        {
            entity.HasKey(e => e.IdOferta).HasName("PK__oferta__05A1245E756D380F");

            entity.ToTable("oferta");

            entity.Property(e => e.IdOferta).HasColumnName("idOferta");
            entity.Property(e => e.CodEmpleador).HasColumnName("codEmpleador");
            entity.Property(e => e.CodCategoria).HasColumnName("codcategoria");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.FechaCierre).HasColumnName("fechaCierre");
            entity.Property(e => e.FechaInicio).HasColumnName("fechaInicio");
            entity.Property(e => e.Modalidad)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("modalidad");
            entity.Property(e => e.NombreOferta)
                .HasMaxLength(30)
                .HasColumnName("nombreOferta");
            entity.Property(e => e.Requisitos).HasColumnName("requisitos");
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ubicacion");

            entity.HasOne(d => d.CodEmpleadorNavigation).WithMany(p => p.Oferta)
                .HasForeignKey(d => d.CodEmpleador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__oferta__codEmple__412EB0B6");

            entity.HasOne(d => d.CodcategoriaNavigation).WithMany(p => p.Oferta)
                .HasForeignKey(d => d.CodCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__oferta__codcateg__4222D4EF");
        });

        modelBuilder.Entity<Postulacion>(entity =>
        {
            entity.HasKey(e => e.CodPostulacion).HasName("PK__postulac__89F785E8B7836A92");

            entity.ToTable("postulacion");

            entity.Property(e => e.CodPostulacion).HasColumnName("codPostulacion");
            entity.Property(e => e.CodCandidato).HasColumnName("codCandidato");
            entity.Property(e => e.CodEmpleador).HasColumnName("codEmpleador");
            entity.Property(e => e.CodOferta).HasColumnName("codOferta");
            entity.Property(e => e.Curriculum).HasColumnName("curriculum");
            entity.Property(e => e.FechaSubida).HasColumnName("fechaSubida");

            entity.HasOne(d => d.CodCandidatoNavigation).WithMany(p => p.Postulacions)
                .HasForeignKey(d => d.CodCandidato)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__postulaci__codCa__44FF419A");

            entity.HasOne(d => d.CodEmpleadorNavigation).WithMany(p => p.Postulacions)
                .HasForeignKey(d => d.CodEmpleador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__postulaci__codEm__46E78A0C");

            entity.HasOne(d => d.CodOfertaNavigation).WithMany(p => p.Postulacions)
                .HasForeignKey(d => d.CodOferta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__postulaci__codOf__45F365D3");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__usuario__645723A65855DC05");

            entity.ToTable("usuario");

            entity.Property(e => e.IdUsuario)
                .ValueGeneratedOnAdd()
                .HasColumnName("idUsuario");
            entity.Property(e => e.ApellidoUsuario)
                .HasMaxLength(30)
                .HasColumnName("apellidoUsuario");
            entity.Property(e => e.ClaveUsuario)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("claveUsuario");
            entity.Property(e => e.EmailUsuario)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("emailUsuario");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fechaNacimiento");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(30)
                .HasColumnName("nombreUsuario");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdUsuarioNavigation).WithOne(p => p.Usuario)
                .HasForeignKey<Usuario>(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__usuario__idUsuar__3D5E1FD2");

            entity.HasOne(d => d.IdUsuario1).WithOne(p => p.Usuario)
                .HasForeignKey<Usuario>(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__usuario__idUsuar__3E52440B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
