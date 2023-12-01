using Microsoft.EntityFrameworkCore.Migrations;

namespace HojeEuCaso.Migrations
{
    public partial class teste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fornecedores_Paises_PaisID",
                table: "Fornecedores");

            migrationBuilder.AlterColumn<int>(
                name: "PaisID",
                table: "Fornecedores",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Fornecedores_Paises_PaisID",
                table: "Fornecedores",
                column: "PaisID",
                principalTable: "Paises",
                principalColumn: "PaisID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fornecedores_Paises_PaisID",
                table: "Fornecedores");

            migrationBuilder.AlterColumn<int>(
                name: "PaisID",
                table: "Fornecedores",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Fornecedores_Paises_PaisID",
                table: "Fornecedores",
                column: "PaisID",
                principalTable: "Paises",
                principalColumn: "PaisID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
