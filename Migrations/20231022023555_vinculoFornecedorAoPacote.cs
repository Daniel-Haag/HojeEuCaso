using Microsoft.EntityFrameworkCore.Migrations;

namespace HojeEuCaso.Migrations
{
    public partial class vinculoFornecedorAoPacote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FornecedorID",
                table: "Pacotes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pacotes_FornecedorID",
                table: "Pacotes",
                column: "FornecedorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacotes_Fornecedores_FornecedorID",
                table: "Pacotes",
                column: "FornecedorID",
                principalTable: "Fornecedores",
                principalColumn: "FornecedorID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacotes_Fornecedores_FornecedorID",
                table: "Pacotes");

            migrationBuilder.DropIndex(
                name: "IX_Pacotes_FornecedorID",
                table: "Pacotes");

            migrationBuilder.DropColumn(
                name: "FornecedorID",
                table: "Pacotes");
        }
    }
}
