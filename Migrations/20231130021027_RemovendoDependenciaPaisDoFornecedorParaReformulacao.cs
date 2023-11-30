using Microsoft.EntityFrameworkCore.Migrations;

namespace HojeEuCaso.Migrations
{
    public partial class RemovendoDependenciaPaisDoFornecedorParaReformulacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fornecedores_Paises_PaisID",
                table: "Fornecedores");

            migrationBuilder.DropIndex(
                name: "IX_Fornecedores_PaisID",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "PaisID",
                table: "Fornecedores");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaisID",
                table: "Fornecedores",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedores_PaisID",
                table: "Fornecedores",
                column: "PaisID");

            migrationBuilder.AddForeignKey(
                name: "FK_Fornecedores_Paises_PaisID",
                table: "Fornecedores",
                column: "PaisID",
                principalTable: "Paises",
                principalColumn: "PaisID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
