using Microsoft.EntityFrameworkCore.Migrations;

namespace HojeEuCaso.Migrations
{
    public partial class EstadoPacote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstadoID",
                table: "Pacotes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pacotes_EstadoID",
                table: "Pacotes",
                column: "EstadoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacotes_Estados_EstadoID",
                table: "Pacotes",
                column: "EstadoID",
                principalTable: "Estados",
                principalColumn: "EstadoID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacotes_Estados_EstadoID",
                table: "Pacotes");

            migrationBuilder.DropIndex(
                name: "IX_Pacotes_EstadoID",
                table: "Pacotes");

            migrationBuilder.DropColumn(
                name: "EstadoID",
                table: "Pacotes");
        }
    }
}
