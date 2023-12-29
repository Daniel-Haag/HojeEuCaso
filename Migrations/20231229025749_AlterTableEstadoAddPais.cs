using Microsoft.EntityFrameworkCore.Migrations;

namespace HojeEuCaso.Migrations
{
    public partial class AlterTableEstadoAddPais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaisID",
                table: "Estados",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estados_PaisID",
                table: "Estados",
                column: "PaisID");

            migrationBuilder.AddForeignKey(
                name: "FK_Estados_Paises_PaisID",
                table: "Estados",
                column: "PaisID",
                principalTable: "Paises",
                principalColumn: "PaisID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estados_Paises_PaisID",
                table: "Estados");

            migrationBuilder.DropIndex(
                name: "IX_Estados_PaisID",
                table: "Estados");

            migrationBuilder.DropColumn(
                name: "PaisID",
                table: "Estados");
        }
    }
}
