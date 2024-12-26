using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppEmpleo.Migrations
{
    /// <inheritdoc />
    public partial class NullableId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ofertas_Reclutadores_ReclutadorId",
                table: "Ofertas");

            migrationBuilder.AlterColumn<int>(
                name: "ReclutadorId",
                table: "Ofertas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Ofertas_Reclutadores_ReclutadorId",
                table: "Ofertas",
                column: "ReclutadorId",
                principalTable: "Reclutadores",
                principalColumn: "ReclutadorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ofertas_Reclutadores_ReclutadorId",
                table: "Ofertas");

            migrationBuilder.AlterColumn<int>(
                name: "ReclutadorId",
                table: "Ofertas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ofertas_Reclutadores_ReclutadorId",
                table: "Ofertas",
                column: "ReclutadorId",
                principalTable: "Reclutadores",
                principalColumn: "ReclutadorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
