using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppEmpleo.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "candidato",
                columns: table => new
                {
                    idCandidato = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    curriculum = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__candidat__F84D9A3C11F242F2", x => x.idCandidato);
                });

            migrationBuilder.CreateTable(
                name: "categoria",
                columns: table => new
                {
                    idCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreCategoria = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__categori__8A3D240C2979A577", x => x.idCategoria);
                });

            migrationBuilder.CreateTable(
                name: "empleador",
                columns: table => new
                {
                    idEmpleador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreEmpresa = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    sector = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    emailEmpresa = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__empleado__42034B0DAB174683", x => x.idEmpleador);
                });

            migrationBuilder.CreateTable(
                name: "oferta",
                columns: table => new
                {
                    idOferta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codEmpleador = table.Column<int>(type: "int", nullable: false),
                    codcategoria = table.Column<int>(type: "int", nullable: false),
                    nombreOferta = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    requisitos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    modalidad = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: false),
                    ubicacion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    fechaInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    fechaCierre = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__oferta__05A1245E756D380F", x => x.idOferta);
                    table.ForeignKey(
                        name: "FK__oferta__codEmple__412EB0B6",
                        column: x => x.codEmpleador,
                        principalTable: "empleador",
                        principalColumn: "idEmpleador");
                    table.ForeignKey(
                        name: "FK__oferta__codcateg__4222D4EF",
                        column: x => x.codcategoria,
                        principalTable: "categoria",
                        principalColumn: "idCategoria");
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    nombreUsuario = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    apellidoUsuario = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    emailUsuario = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false),
                    claveUsuario = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    telefono = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    fechaNacimiento = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__usuario__645723A65855DC05", x => x.idUsuario);
                    table.ForeignKey(
                        name: "FK__usuario__idUsuar__3D5E1FD2",
                        column: x => x.idUsuario,
                        principalTable: "candidato",
                        principalColumn: "idCandidato");
                    table.ForeignKey(
                        name: "FK__usuario__idUsuar__3E52440B",
                        column: x => x.idUsuario,
                        principalTable: "empleador",
                        principalColumn: "idEmpleador");
                });

            migrationBuilder.CreateTable(
                name: "postulacion",
                columns: table => new
                {
                    codPostulacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codOferta = table.Column<int>(type: "int", nullable: false),
                    codCandidato = table.Column<int>(type: "int", nullable: false),
                    codEmpleador = table.Column<int>(type: "int", nullable: false),
                    curriculum = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    fechaSubida = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__postulac__89F785E8B7836A92", x => x.codPostulacion);
                    table.ForeignKey(
                        name: "FK__postulaci__codCa__44FF419A",
                        column: x => x.codCandidato,
                        principalTable: "candidato",
                        principalColumn: "idCandidato");
                    table.ForeignKey(
                        name: "FK__postulaci__codEm__46E78A0C",
                        column: x => x.codEmpleador,
                        principalTable: "empleador",
                        principalColumn: "idEmpleador");
                    table.ForeignKey(
                        name: "FK__postulaci__codOf__45F365D3",
                        column: x => x.codOferta,
                        principalTable: "oferta",
                        principalColumn: "idOferta");
                });

            migrationBuilder.CreateIndex(
                name: "IX_oferta_codcategoria",
                table: "oferta",
                column: "codcategoria");

            migrationBuilder.CreateIndex(
                name: "IX_oferta_codEmpleador",
                table: "oferta",
                column: "codEmpleador");

            migrationBuilder.CreateIndex(
                name: "IX_postulacion_codCandidato",
                table: "postulacion",
                column: "codCandidato");

            migrationBuilder.CreateIndex(
                name: "IX_postulacion_codEmpleador",
                table: "postulacion",
                column: "codEmpleador");

            migrationBuilder.CreateIndex(
                name: "IX_postulacion_codOferta",
                table: "postulacion",
                column: "codOferta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "postulacion");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "oferta");

            migrationBuilder.DropTable(
                name: "candidato");

            migrationBuilder.DropTable(
                name: "empleador");

            migrationBuilder.DropTable(
                name: "categoria");
        }
    }
}
