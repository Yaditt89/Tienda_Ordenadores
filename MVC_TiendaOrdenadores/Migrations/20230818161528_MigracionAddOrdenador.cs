using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_TiendaOrdenadores.Migrations
{
    /// <inheritdoc />
    public partial class MigracionAddOrdenador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrdenadorId",
                table: "Componente",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ordenador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordenador", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Componente_OrdenadorId",
                table: "Componente",
                column: "OrdenadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Componente_Ordenador_OrdenadorId",
                table: "Componente",
                column: "OrdenadorId",
                principalTable: "Ordenador",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Componente_Ordenador_OrdenadorId",
                table: "Componente");

            migrationBuilder.DropTable(
                name: "Ordenador");

            migrationBuilder.DropIndex(
                name: "IX_Componente_OrdenadorId",
                table: "Componente");

            migrationBuilder.DropColumn(
                name: "OrdenadorId",
                table: "Componente");
        }
    }
}
