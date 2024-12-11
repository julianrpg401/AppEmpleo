using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppEmpleo.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrencyType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Salario",
                table: "Ofertas",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AddColumn<string>(
                name: "Moneda",
                table: "Ofertas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Moneda",
                table: "Ofertas");

            migrationBuilder.AlterColumn<decimal>(
                name: "Salario",
                table: "Ofertas",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);
        }
    }
}
