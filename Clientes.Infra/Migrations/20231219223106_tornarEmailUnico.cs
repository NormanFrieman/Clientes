using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clientes.Infra.Migrations
{
    /// <inheritdoc />
    public partial class tornarEmailUnico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Email",
                table: "Cliente",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cliente_Email",
                table: "Cliente");
        }
    }
}
