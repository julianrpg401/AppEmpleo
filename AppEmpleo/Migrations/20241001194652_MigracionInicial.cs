using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppEmpleo.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
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
                    table.PrimaryKey("PK__Categori__06370DAD985B2763", x => x.Codigo);
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
                    Teléfono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Empresa__3214EC076CA6B446", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Habilidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Habilida__3214EC07313E3FE0", x => x.Id);
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
                    ContraseñaHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuario__3214EC07DCFF04D7", x => x.Id);
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
                    table.PrimaryKey("PK__Candidat__3214EC0769FC2A81", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Candidato__Id__4222D4EF",
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
                    table.PrimaryKey("PK__Empleado__3214EC073B425ACB", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Empleador__Empre__3F466844",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Empleador__Id__3E52440B",
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
                    table.PrimaryKey("PK__Curricul__3214EC07DFC3549A", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Curriculu__Candi__46E78A0C",
                        column: x => x.CandidatoId,
                        principalTable: "Candidato",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Oferta",
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
                    table.PrimaryKey("PK__Oferta__3214EC0734F9C979", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Oferta__Categori__5070F446",
                        column: x => x.CategoriaCodigo,
                        principalTable: "Categoria",
                        principalColumn: "Codigo");
                    table.ForeignKey(
                        name: "FK__Oferta__Empleado__4F7CD00D",
                        column: x => x.EmpleadorId,
                        principalTable: "Empleador",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OfertaHabilidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfertaEmpleoId = table.Column<int>(type: "int", nullable: false),
                    HabilidadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OfertaHa__3214EC07D82250B9", x => x.Id);
                    table.ForeignKey(
                        name: "FK__OfertaHab__Habil__5535A963",
                        column: x => x.HabilidadId,
                        principalTable: "Habilidades",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__OfertaHab__Ofert__5441852A",
                        column: x => x.OfertaEmpleoId,
                        principalTable: "Oferta",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Postulaciones",
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
                    table.PrimaryKey("PK__Postulac__3214EC074E44ECF6", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Postulaci__Candi__5AEE82B9",
                        column: x => x.CandidatoId,
                        principalTable: "Candidato",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Postulaci__Curri__5BE2A6F2",
                        column: x => x.CurriculumId,
                        principalTable: "Curriculum",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Postulaci__Ofert__59FA5E80",
                        column: x => x.OfertaEmpleoId,
                        principalTable: "Oferta",
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
                name: "UQ__Habilida__75E3EFCF3A815BBF",
                table: "Habilidades",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Oferta_CategoriaCodigo",
                table: "Oferta",
                column: "CategoriaCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_Oferta_EmpleadorId",
                table: "Oferta",
                column: "EmpleadorId");

            migrationBuilder.CreateIndex(
                name: "IX_OfertaHabilidades_HabilidadId",
                table: "OfertaHabilidades",
                column: "HabilidadId");

            migrationBuilder.CreateIndex(
                name: "UC_Oferta_Habilidad",
                table: "OfertaHabilidades",
                columns: new[] { "OfertaEmpleoId", "HabilidadId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Postulaciones_CandidatoId",
                table: "Postulaciones",
                column: "CandidatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Postulaciones_CurriculumId",
                table: "Postulaciones",
                column: "CurriculumId");

            migrationBuilder.CreateIndex(
                name: "UC_Postulacion",
                table: "Postulaciones",
                columns: new[] { "OfertaEmpleoId", "CandidatoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Usuario__A9D1053427F635C8",
                table: "Usuario",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfertaHabilidades");

            migrationBuilder.DropTable(
                name: "Postulaciones");

            migrationBuilder.DropTable(
                name: "Habilidades");

            migrationBuilder.DropTable(
                name: "Curriculum");

            migrationBuilder.DropTable(
                name: "Oferta");

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
