using Microsoft.EntityFrameworkCore.Migrations;

namespace HojeEuCaso.Migrations
{
    public partial class CidadePacote2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CidadeID",
                table: "Pacotes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pacotes_CidadeID",
                table: "Pacotes",
                column: "CidadeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacotes_Cidades_CidadeID",
                table: "Pacotes",
                column: "CidadeID",
                principalTable: "Cidades",
                principalColumn: "CidadeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacotes_Cidades_CidadeID",
                table: "Pacotes");

            migrationBuilder.DropIndex(
                name: "IX_Pacotes_CidadeID",
                table: "Pacotes");

            migrationBuilder.DropColumn(
                name: "CidadeID",
                table: "Pacotes");
        }
    }
}
