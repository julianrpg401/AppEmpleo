﻿// <auto-generated />
using System;
using AppEmpleo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AppEmpleo.Migrations
{
    [DbContext(typeof(AppEmpleoContext))]
    partial class AppEmpleoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AppEmpleo.Models.Candidato", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Curp")
                        .HasMaxLength(18)
                        .HasColumnType("nvarchar(18)")
                        .HasColumnName("CURP");

                    b.Property<string>("Dirección")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Telefono")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id")
                        .HasName("PK__Candidat__3214EC0769FC2A81");

                    b.ToTable("Candidato", (string)null);
                });

            modelBuilder.Entity("AppEmpleo.Models.Categorium", b =>
                {
                    b.Property<string>("Codigo")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Codigo")
                        .HasName("PK__Categori__06370DAD985B2763");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("AppEmpleo.Models.Curriculum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CandidatoId")
                        .HasColumnType("int");

                    b.Property<bool>("EsPreferido")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCarga")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("NombreArchivo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("RutaArchivo")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id")
                        .HasName("PK__Curricul__3214EC07DFC3549A");

                    b.HasIndex("CandidatoId");

                    b.ToTable("Curriculum", (string)null);
                });

            modelBuilder.Entity("AppEmpleo.Models.Empleador", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("EmpresaId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Empleado__3214EC073B425ACB");

                    b.HasIndex("EmpresaId");

                    b.ToTable("Empleador", (string)null);
                });

            modelBuilder.Entity("AppEmpleo.Models.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Dirección")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("SitioWeb")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Teléfono")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id")
                        .HasName("PK__Empresa__3214EC076CA6B446");

                    b.ToTable("Empresa", (string)null);
                });

            modelBuilder.Entity("AppEmpleo.Models.Habilidade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK__Habilida__3214EC07313E3FE0");

                    b.HasIndex(new[] { "Nombre" }, "UQ__Habilida__75E3EFCF3A815BBF")
                        .IsUnique();

                    b.ToTable("Habilidades");
                });

            modelBuilder.Entity("AppEmpleo.Models.OfertaHabilidade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("HabilidadId")
                        .HasColumnType("int");

                    b.Property<int>("OfertaEmpleoId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__OfertaHa__3214EC07D82250B9");

                    b.HasIndex("HabilidadId");

                    b.HasIndex(new[] { "OfertaEmpleoId", "HabilidadId" }, "UC_Oferta_Habilidad")
                        .IsUnique();

                    b.ToTable("OfertaHabilidades");
                });

            modelBuilder.Entity("AppEmpleo.Models.Ofertum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoriaCodigo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmpleadorId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("FechaCierre")
                        .HasColumnType("date");

                    b.Property<DateOnly>("FechaInicio")
                        .HasColumnType("date");

                    b.Property<string>("ModalidadTrabajo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("NombreOferta")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("Salario")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("UbicacionTrabajo")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id")
                        .HasName("PK__Oferta__3214EC0734F9C979");

                    b.HasIndex("CategoriaCodigo");

                    b.HasIndex("EmpleadorId");

                    b.ToTable("Oferta");
                });

            modelBuilder.Entity("AppEmpleo.Models.Postulacione", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CandidatoId")
                        .HasColumnType("int");

                    b.Property<int>("CurriculumId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaPostulacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("OfertaEmpleoId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Postulac__3214EC074E44ECF6");

                    b.HasIndex("CandidatoId");

                    b.HasIndex("CurriculumId");

                    b.HasIndex(new[] { "OfertaEmpleoId", "CandidatoId" }, "UC_Postulacion")
                        .IsUnique();

                    b.ToTable("Postulaciones");
                });

            modelBuilder.Entity("AppEmpleo.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ContraseñaHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("FechaRegistro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id")
                        .HasName("PK__Usuario__3214EC07DCFF04D7");

                    b.HasIndex(new[] { "Email" }, "UQ__Usuario__A9D1053427F635C8")
                        .IsUnique();

                    b.ToTable("Usuario", (string)null);
                });

            modelBuilder.Entity("AppEmpleo.Models.Candidato", b =>
                {
                    b.HasOne("AppEmpleo.Models.Usuario", "IdNavigation")
                        .WithOne("Candidato")
                        .HasForeignKey("AppEmpleo.Models.Candidato", "Id")
                        .IsRequired()
                        .HasConstraintName("FK__Candidato__Id__4222D4EF");

                    b.Navigation("IdNavigation");
                });

            modelBuilder.Entity("AppEmpleo.Models.Curriculum", b =>
                {
                    b.HasOne("AppEmpleo.Models.Candidato", "Candidato")
                        .WithMany("Curricula")
                        .HasForeignKey("CandidatoId")
                        .IsRequired()
                        .HasConstraintName("FK__Curriculu__Candi__46E78A0C");

                    b.Navigation("Candidato");
                });

            modelBuilder.Entity("AppEmpleo.Models.Empleador", b =>
                {
                    b.HasOne("AppEmpleo.Models.Empresa", "Empresa")
                        .WithMany("Empleadors")
                        .HasForeignKey("EmpresaId")
                        .IsRequired()
                        .HasConstraintName("FK__Empleador__Empre__3F466844");

                    b.HasOne("AppEmpleo.Models.Usuario", "IdNavigation")
                        .WithOne("Empleador")
                        .HasForeignKey("AppEmpleo.Models.Empleador", "Id")
                        .IsRequired()
                        .HasConstraintName("FK__Empleador__Id__3E52440B");

                    b.Navigation("Empresa");

                    b.Navigation("IdNavigation");
                });

            modelBuilder.Entity("AppEmpleo.Models.OfertaHabilidade", b =>
                {
                    b.HasOne("AppEmpleo.Models.Habilidade", "Habilidad")
                        .WithMany("OfertaHabilidades")
                        .HasForeignKey("HabilidadId")
                        .IsRequired()
                        .HasConstraintName("FK__OfertaHab__Habil__5535A963");

                    b.HasOne("AppEmpleo.Models.Ofertum", "OfertaEmpleo")
                        .WithMany("OfertaHabilidades")
                        .HasForeignKey("OfertaEmpleoId")
                        .IsRequired()
                        .HasConstraintName("FK__OfertaHab__Ofert__5441852A");

                    b.Navigation("Habilidad");

                    b.Navigation("OfertaEmpleo");
                });

            modelBuilder.Entity("AppEmpleo.Models.Ofertum", b =>
                {
                    b.HasOne("AppEmpleo.Models.Categorium", "CategoriaCodigoNavigation")
                        .WithMany("Oferta")
                        .HasForeignKey("CategoriaCodigo")
                        .IsRequired()
                        .HasConstraintName("FK__Oferta__Categori__5070F446");

                    b.HasOne("AppEmpleo.Models.Empleador", "Empleador")
                        .WithMany("Oferta")
                        .HasForeignKey("EmpleadorId")
                        .IsRequired()
                        .HasConstraintName("FK__Oferta__Empleado__4F7CD00D");

                    b.Navigation("CategoriaCodigoNavigation");

                    b.Navigation("Empleador");
                });

            modelBuilder.Entity("AppEmpleo.Models.Postulacione", b =>
                {
                    b.HasOne("AppEmpleo.Models.Candidato", "Candidato")
                        .WithMany("Postulaciones")
                        .HasForeignKey("CandidatoId")
                        .IsRequired()
                        .HasConstraintName("FK__Postulaci__Candi__5AEE82B9");

                    b.HasOne("AppEmpleo.Models.Curriculum", "Curriculum")
                        .WithMany("Postulaciones")
                        .HasForeignKey("CurriculumId")
                        .IsRequired()
                        .HasConstraintName("FK__Postulaci__Curri__5BE2A6F2");

                    b.HasOne("AppEmpleo.Models.Ofertum", "OfertaEmpleo")
                        .WithMany("Postulaciones")
                        .HasForeignKey("OfertaEmpleoId")
                        .IsRequired()
                        .HasConstraintName("FK__Postulaci__Ofert__59FA5E80");

                    b.Navigation("Candidato");

                    b.Navigation("Curriculum");

                    b.Navigation("OfertaEmpleo");
                });

            modelBuilder.Entity("AppEmpleo.Models.Candidato", b =>
                {
                    b.Navigation("Curricula");

                    b.Navigation("Postulaciones");
                });

            modelBuilder.Entity("AppEmpleo.Models.Categorium", b =>
                {
                    b.Navigation("Oferta");
                });

            modelBuilder.Entity("AppEmpleo.Models.Curriculum", b =>
                {
                    b.Navigation("Postulaciones");
                });

            modelBuilder.Entity("AppEmpleo.Models.Empleador", b =>
                {
                    b.Navigation("Oferta");
                });

            modelBuilder.Entity("AppEmpleo.Models.Empresa", b =>
                {
                    b.Navigation("Empleadors");
                });

            modelBuilder.Entity("AppEmpleo.Models.Habilidade", b =>
                {
                    b.Navigation("OfertaHabilidades");
                });

            modelBuilder.Entity("AppEmpleo.Models.Ofertum", b =>
                {
                    b.Navigation("OfertaHabilidades");

                    b.Navigation("Postulaciones");
                });

            modelBuilder.Entity("AppEmpleo.Models.Usuario", b =>
                {
                    b.Navigation("Candidato");

                    b.Navigation("Empleador");
                });
#pragma warning restore 612, 618
        }
    }
}
