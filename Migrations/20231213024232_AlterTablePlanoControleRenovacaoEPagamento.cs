using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HojeEuCaso.Migrations
{
    public partial class AlterTablePlanoControleRenovacaoEPagamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "Planos",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataProximaRenovacao",
                table: "Planos",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Pago",
                table: "Planos",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataProximaRenovacao",
                table: "Planos");

            migrationBuilder.DropColumn(
                name: "Pago",
                table: "Planos");

            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "Planos",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");
        }
    }
}
