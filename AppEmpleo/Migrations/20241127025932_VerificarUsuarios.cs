using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppEmpleo.Migrations
{
    /// <inheritdoc />
    public partial class VerificarUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Codigo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__06370DAD0B2F114B", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Dirección = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    SitioWeb = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Empresa__3214EC075E695FD9", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Habilidad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Habilida__3214EC077D852756", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ClaveHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuario__3214EC07EBEB3F99", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Candidato",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CURP = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: true),
                    Dirección = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Candidat__3214EC07B4540503", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Candidato__Id__0C85DE4D",
                        column: x => x.Id,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Empleador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Empleado__3214EC07384E7F9B", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Empleador__Empre__09A971A2",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Empleador__Id__08B54D69",
                        column: x => x.Id,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Curriculum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidatoId = table.Column<int>(type: "int", nullable: false),
                    NombreArchivo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RutaArchivo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FechaCarga = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    EsPreferido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Curricul__3214EC075D002124", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Curriculu__Candi__114A936A",
                        column: x => x.CandidatoId,
                        principalTable: "Candidato",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ofertas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpleadorId = table.Column<int>(type: "int", nullable: false),
                    NombreOferta = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaCierre = table.Column<DateOnly>(type: "date", nullable: false),
                    Salario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ModalidadTrabajo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UbicacionTrabajo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CategoriaCodigo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Oferta__3214EC07BD202C1F", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Oferta__Categori__1AD3FDA4",
                        column: x => x.CategoriaCodigo,
                        principalTable: "Categoria",
                        principalColumn: "Codigo");
                    table.ForeignKey(
                        name: "FK__Oferta__Empleado__19DFD96B",
                        column: x => x.EmpleadorId,
                        principalTable: "Empleador",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OfertaHabilidad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfertaEmpleoId = table.Column<int>(type: "int", nullable: false),
                    HabilidadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OfertaHa__3214EC0751B9F54B", x => x.Id);
                    table.ForeignKey(
                        name: "FK__OfertaHab__Habil__1F98B2C1",
                        column: x => x.HabilidadId,
                        principalTable: "Habilidad",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__OfertaHab__Ofert__1EA48E88",
                        column: x => x.OfertaEmpleoId,
                        principalTable: "Ofertas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Postulacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfertaEmpleoId = table.Column<int>(type: "int", nullable: false),
                    CandidatoId = table.Column<int>(type: "int", nullable: false),
                    CurriculumId = table.Column<int>(type: "int", nullable: false),
                    FechaPostulacion = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Postulac__3214EC070AB5BA7E", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Postulaci__Candi__25518C17",
                        column: x => x.CandidatoId,
                        principalTable: "Candidato",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Postulaci__Curri__2645B050",
                        column: x => x.CurriculumId,
                        principalTable: "Curriculum",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Postulaci__Ofert__245D67DE",
                        column: x => x.OfertaEmpleoId,
                        principalTable: "Ofertas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Curriculum_CandidatoId",
                table: "Curriculum",
                column: "CandidatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleador_EmpresaId",
                table: "Empleador",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "UQ__Habilida__75E3EFCFABB70B06",
                table: "Habilidad",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OfertaHabilidad_HabilidadId",
                table: "OfertaHabilidad",
                column: "HabilidadId");

            migrationBuilder.CreateIndex(
                name: "UC_Oferta_Habilidad",
                table: "OfertaHabilidad",
                columns: new[] { "OfertaEmpleoId", "HabilidadId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ofertas_CategoriaCodigo",
                table: "Ofertas",
                column: "CategoriaCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_Ofertas_EmpleadorId",
                table: "Ofertas",
                column: "EmpleadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Postulacion_CandidatoId",
                table: "Postulacion",
                column: "CandidatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Postulacion_CurriculumId",
                table: "Postulacion",
                column: "CurriculumId");

            migrationBuilder.CreateIndex(
                name: "UC_Postulacion",
                table: "Postulacion",
                columns: new[] { "OfertaEmpleoId", "CandidatoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Usuario__A9D105346AE90531",
                table: "Usuario",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfertaHabilidad");

            migrationBuilder.DropTable(
                name: "Postulacion");

            migrationBuilder.DropTable(
                name: "Habilidad");

            migrationBuilder.DropTable(
                name: "Curriculum");

            migrationBuilder.DropTable(
                name: "Ofertas");

            migrationBuilder.DropTable(
                name: "Candidato");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Empleador");

            migrationBuilder.DropTable(
                name: "Empresa");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
