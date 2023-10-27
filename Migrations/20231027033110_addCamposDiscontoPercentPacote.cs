using Microsoft.EntityFrameworkCore.Migrations;

namespace HojeEuCaso.Migrations
{
    public partial class addCamposDiscontoPercentPacote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DescontoDomingo",
                table: "Pacotes",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DescontoQuartaFeira",
                table: "Pacotes",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DescontoQuintaFeira",
                table: "Pacotes",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DescontoSabado",
                table: "Pacotes",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DescontoSegundaFeira",
                table: "Pacotes",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DescontoSextaFeira",
                table: "Pacotes",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DescontoTercaFeira",
                table: "Pacotes",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescontoDomingo",
                table: "Pacotes");

            migrationBuilder.DropColumn(
                name: "DescontoQuartaFeira",
                table: "Pacotes");

            migrationBuilder.DropColumn(
                name: "DescontoQuintaFeira",
                table: "Pacotes");

            migrationBuilder.DropColumn(
                name: "DescontoSabado",
                table: "Pacotes");

            migrationBuilder.DropColumn(
                name: "DescontoSegundaFeira",
                table: "Pacotes");

            migrationBuilder.DropColumn(
                name: "DescontoSextaFeira",
                table: "Pacotes");

            migrationBuilder.DropColumn(
                name: "DescontoTercaFeira",
                table: "Pacotes");
        }
    }
}
