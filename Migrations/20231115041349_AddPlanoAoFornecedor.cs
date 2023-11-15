using Microsoft.EntityFrameworkCore.Migrations;

namespace HojeEuCaso.Migrations
{
    public partial class AddPlanoAoFornecedor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlanoID",
                table: "Fornecedores",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedores_PlanoID",
                table: "Fornecedores",
                column: "PlanoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Fornecedores_Planos_PlanoID",
                table: "Fornecedores",
                column: "PlanoID",
                principalTable: "Planos",
                principalColumn: "PlanoID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fornecedores_Planos_PlanoID",
                table: "Fornecedores");

            migrationBuilder.DropIndex(
                name: "IX_Fornecedores_PlanoID",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "PlanoID",
                table: "Fornecedores");
        }
    }
}
