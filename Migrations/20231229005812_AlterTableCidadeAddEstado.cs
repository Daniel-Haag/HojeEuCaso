using Microsoft.EntityFrameworkCore.Migrations;

namespace HojeEuCaso.Migrations
{
    public partial class AlterTableCidadeAddEstado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstadoID",
                table: "Cidades",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cidades_EstadoID",
                table: "Cidades",
                column: "EstadoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cidades_Estados_EstadoID",
                table: "Cidades",
                column: "EstadoID",
                principalTable: "Estados",
                principalColumn: "EstadoID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cidades_Estados_EstadoID",
                table: "Cidades");

            migrationBuilder.DropIndex(
                name: "IX_Cidades_EstadoID",
                table: "Cidades");

            migrationBuilder.DropColumn(
                name: "EstadoID",
                table: "Cidades");
        }
    }
}
