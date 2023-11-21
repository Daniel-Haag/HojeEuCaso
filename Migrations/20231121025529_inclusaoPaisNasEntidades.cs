using Microsoft.EntityFrameworkCore.Migrations;

namespace HojeEuCaso.Migrations
{
    public partial class inclusaoPaisNasEntidades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaisID",
                table: "UsuariosSistema",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaisID",
                table: "Pacotes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaisID",
                table: "Fornecedores",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosSistema_PaisID",
                table: "UsuariosSistema",
                column: "PaisID");

            migrationBuilder.CreateIndex(
                name: "IX_Pacotes_PaisID",
                table: "Pacotes",
                column: "PaisID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Pacotes_Paises_PaisID",
                table: "Pacotes",
                column: "PaisID",
                principalTable: "Paises",
                principalColumn: "PaisID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosSistema_Paises_PaisID",
                table: "UsuariosSistema",
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

            migrationBuilder.DropForeignKey(
                name: "FK_Pacotes_Paises_PaisID",
                table: "Pacotes");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosSistema_Paises_PaisID",
                table: "UsuariosSistema");

            migrationBuilder.DropIndex(
                name: "IX_UsuariosSistema_PaisID",
                table: "UsuariosSistema");

            migrationBuilder.DropIndex(
                name: "IX_Pacotes_PaisID",
                table: "Pacotes");

            migrationBuilder.DropIndex(
                name: "IX_Fornecedores_PaisID",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "PaisID",
                table: "UsuariosSistema");

            migrationBuilder.DropColumn(
                name: "PaisID",
                table: "Pacotes");

            migrationBuilder.DropColumn(
                name: "PaisID",
                table: "Fornecedores");
        }
    }
}
