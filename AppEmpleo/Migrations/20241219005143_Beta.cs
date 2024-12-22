using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppEmpleo.Migrations
{
    /// <inheritdoc />
    public partial class Beta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ofertas_Categorias_CategoriaId",
                table: "Ofertas");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaId",
                table: "Ofertas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Ofertas_Categorias_CategoriaId",
                table: "Ofertas",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ofertas_Categorias_CategoriaId",
                table: "Ofertas");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaId",
                table: "Ofertas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ofertas_Categorias_CategoriaId",
                table: "Ofertas",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
