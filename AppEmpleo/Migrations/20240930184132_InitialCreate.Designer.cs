﻿// <auto-generated />
using System;
using AppEmpleo.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AppEmpleo.Migrations
{
    [DbContext(typeof(AppEmpleoContext))]
    [Migration("20240930184132_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AppEmpleo.Modelos.Candidato", b =>
                {
                    b.Property<int>("IdCandidato")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idCandidato");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCandidato"));

                    b.Property<byte[]>("Curriculum")
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("curriculum");

                    b.HasKey("IdCandidato")
                        .HasName("PK__candidat__F84D9A3C11F242F2");

                    b.ToTable("candidato", (string)null);
                });

            modelBuilder.Entity("AppEmpleo.Modelos.Categoria", b =>
                {
                    b.Property<int>("IdCategoria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idCategoria");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCategoria"));

                    b.Property<string>("NombreCategoria")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("nombreCategoria");

                    b.HasKey("IdCategoria")
                        .HasName("PK__categori__8A3D240C2979A577");

                    b.ToTable("categoria", (string)null);
                });

            modelBuilder.Entity("AppEmpleo.Modelos.Empleador", b =>
                {
                    b.Property<int>("IdEmpleador")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idEmpleador");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEmpleador"));

                    b.Property<string>("EmailEmpresa")
                        .HasMaxLength(60)
                        .IsUnicode(false)
                        .HasColumnType("varchar(60)")
                        .HasColumnName("emailEmpresa");

                    b.Property<string>("NombreEmpresa")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("nombreEmpresa");

                    b.Property<string>("Sector")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("sector");

                    b.HasKey("IdEmpleador")
                        .HasName("PK__empleado__42034B0DAB174683");

                    b.ToTable("empleador", (string)null);
                });

            modelBuilder.Entity("AppEmpleo.Modelos.Oferta", b =>
                {
                    b.Property<int>("IdOferta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idOferta");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdOferta"));

                    b.Property<int>("CodCategoria")
                        .HasColumnType("int")
                        .HasColumnName("codcategoria");

                    b.Property<int>("CodEmpleador")
                        .HasColumnType("int")
                        .HasColumnName("codEmpleador");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("descripcion");

                    b.Property<DateOnly>("FechaCierre")
                        .HasColumnType("date")
                        .HasColumnName("fechaCierre");

                    b.Property<DateOnly>("FechaInicio")
                        .HasColumnType("date")
                        .HasColumnName("fechaInicio");

                    b.Property<string>("Modalidad")
                        .IsRequired()
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("varchar(12)")
                        .HasColumnName("modalidad");

                    b.Property<string>("NombreOferta")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("nombreOferta");

                    b.Property<string>("Requisitos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("requisitos");

                    b.Property<string>("Ubicacion")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("ubicacion");

                    b.HasKey("IdOferta")
                        .HasName("PK__oferta__05A1245E756D380F");

                    b.HasIndex("CodCategoria");

                    b.HasIndex("CodEmpleador");

                    b.ToTable("oferta", (string)null);
                });

            modelBuilder.Entity("AppEmpleo.Modelos.Postulacion", b =>
                {
                    b.Property<int>("CodPostulacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("codPostulacion");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodPostulacion"));

                    b.Property<int>("CodCandidato")
                        .HasColumnType("int")
                        .HasColumnName("codCandidato");

                    b.Property<int>("CodEmpleador")
                        .HasColumnType("int")
                        .HasColumnName("codEmpleador");

                    b.Property<int>("CodOferta")
                        .HasColumnType("int")
                        .HasColumnName("codOferta");

                    b.Property<byte[]>("Curriculum")
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("curriculum");

                    b.Property<DateOnly>("FechaSubida")
                        .HasColumnType("date")
                        .HasColumnName("fechaSubida");

                    b.HasKey("CodPostulacion")
                        .HasName("PK__postulac__89F785E8B7836A92");

                    b.HasIndex("CodCandidato");

                    b.HasIndex("CodEmpleador");

                    b.HasIndex("CodOferta");

                    b.ToTable("postulacion", (string)null);
                });

            modelBuilder.Entity("AppEmpleo.Modelos.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idUsuario");

                    b.Property<string>("ApellidoUsuario")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("apellidoUsuario");

                    b.Property<string>("ClaveUsuario")
                        .IsRequired()
                        .HasMaxLength(64)
                        .IsUnicode(false)
                        .HasColumnType("varchar(64)")
                        .HasColumnName("claveUsuario");

                    b.Property<string>("EmailUsuario")
                        .IsRequired()
                        .HasMaxLength(60)
                        .IsUnicode(false)
                        .HasColumnType("varchar(60)")
                        .HasColumnName("emailUsuario");

                    b.Property<DateOnly>("FechaNacimiento")
                        .HasColumnType("date")
                        .HasColumnName("fechaNacimiento");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("nombreUsuario");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("telefono");

                    b.HasKey("IdUsuario")
                        .HasName("PK__usuario__645723A65855DC05");

                    b.ToTable("usuario", (string)null);
                });

            modelBuilder.Entity("AppEmpleo.Modelos.Oferta", b =>
                {
                    b.HasOne("AppEmpleo.Modelos.Categoria", "CodcategoriaNavigation")
                        .WithMany("Oferta")
                        .HasForeignKey("CodCategoria")
                        .IsRequired()
                        .HasConstraintName("FK__oferta__codcateg__4222D4EF");

                    b.HasOne("AppEmpleo.Modelos.Empleador", "CodEmpleadorNavigation")
                        .WithMany("Oferta")
                        .HasForeignKey("CodEmpleador")
                        .IsRequired()
                        .HasConstraintName("FK__oferta__codEmple__412EB0B6");

                    b.Navigation("CodEmpleadorNavigation");

                    b.Navigation("CodcategoriaNavigation");
                });

            modelBuilder.Entity("AppEmpleo.Modelos.Postulacion", b =>
                {
                    b.HasOne("AppEmpleo.Modelos.Candidato", "CodCandidatoNavigation")
                        .WithMany("Postulacions")
                        .HasForeignKey("CodCandidato")
                        .IsRequired()
                        .HasConstraintName("FK__postulaci__codCa__44FF419A");

                    b.HasOne("AppEmpleo.Modelos.Empleador", "CodEmpleadorNavigation")
                        .WithMany("Postulacions")
                        .HasForeignKey("CodEmpleador")
                        .IsRequired()
                        .HasConstraintName("FK__postulaci__codEm__46E78A0C");

                    b.HasOne("AppEmpleo.Modelos.Oferta", "CodOfertaNavigation")
                        .WithMany("Postulacions")
                        .HasForeignKey("CodOferta")
                        .IsRequired()
                        .HasConstraintName("FK__postulaci__codOf__45F365D3");

                    b.Navigation("CodCandidatoNavigation");

                    b.Navigation("CodEmpleadorNavigation");

                    b.Navigation("CodOfertaNavigation");
                });

            modelBuilder.Entity("AppEmpleo.Modelos.Usuario", b =>
                {
                    b.HasOne("AppEmpleo.Modelos.Candidato", "IdUsuarioNavigation")
                        .WithOne("Usuario")
                        .HasForeignKey("AppEmpleo.Modelos.Usuario", "IdUsuario")
                        .IsRequired()
                        .HasConstraintName("FK__usuario__idUsuar__3D5E1FD2");

                    b.HasOne("AppEmpleo.Modelos.Empleador", "IdUsuario1")
                        .WithOne("Usuario")
                        .HasForeignKey("AppEmpleo.Modelos.Usuario", "IdUsuario")
                        .IsRequired()
                        .HasConstraintName("FK__usuario__idUsuar__3E52440B");

                    b.Navigation("IdUsuario1");

                    b.Navigation("IdUsuarioNavigation");
                });

            modelBuilder.Entity("AppEmpleo.Modelos.Candidato", b =>
                {
                    b.Navigation("Postulacions");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("AppEmpleo.Modelos.Categoria", b =>
                {
                    b.Navigation("Oferta");
                });

            modelBuilder.Entity("AppEmpleo.Modelos.Empleador", b =>
                {
                    b.Navigation("Oferta");

                    b.Navigation("Postulacions");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("AppEmpleo.Modelos.Oferta", b =>
                {
                    b.Navigation("Postulacions");
                });
#pragma warning restore 612, 618
        }
    }
}
