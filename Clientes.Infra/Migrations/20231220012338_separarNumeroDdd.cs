using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clientes.Infra.Migrations
{
    /// <inheritdoc />
    public partial class separarNumeroDdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Telefone_Numero",
                table: "Telefone");

            migrationBuilder.AddColumn<string>(
                name: "Ddd",
                table: "Telefone",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Telefone_Ddd_Numero",
                table: "Telefone",
                columns: new[] { "Ddd", "Numero" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Telefone_Ddd_Numero",
                table: "Telefone");

            migrationBuilder.DropColumn(
                name: "Ddd",
                table: "Telefone");

            migrationBuilder.CreateIndex(
                name: "IX_Telefone_Numero",
                table: "Telefone",
                column: "Numero",
                unique: true);
        }
    }
}
