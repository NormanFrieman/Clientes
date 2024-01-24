using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clientes.Infra.Migrations
{
    /// <inheritdoc />
    public partial class criarCamposData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Cliente",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFim",
                table: "Cliente",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "DataFim",
                table: "Cliente");
        }
    }
}
