using Microsoft.EntityFrameworkCore.Migrations;

namespace HojeEuCaso.Migrations
{
    public partial class ClausulasDeContratoParaFornecedor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FornecedorID",
                table: "ClausulasContratos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClausulasContratos_FornecedorID",
                table: "ClausulasContratos",
                column: "FornecedorID");

            migrationBuilder.AddForeignKey(
                name: "FK_ClausulasContratos_Fornecedores_FornecedorID",
                table: "ClausulasContratos",
                column: "FornecedorID",
                principalTable: "Fornecedores",
                principalColumn: "FornecedorID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClausulasContratos_Fornecedores_FornecedorID",
                table: "ClausulasContratos");

            migrationBuilder.DropIndex(
                name: "IX_ClausulasContratos_FornecedorID",
                table: "ClausulasContratos");

            migrationBuilder.DropColumn(
                name: "FornecedorID",
                table: "ClausulasContratos");
        }
    }
}
