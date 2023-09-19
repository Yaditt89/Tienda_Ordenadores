using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_TiendaOrdenadores.Migrations
{
    /// <inheritdoc />
    public partial class OrdenadorName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Ordenador",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Ordenador");
        }
    }
}
