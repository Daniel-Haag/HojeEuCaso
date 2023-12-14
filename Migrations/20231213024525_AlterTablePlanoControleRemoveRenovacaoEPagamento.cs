using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HojeEuCaso.Migrations
{
    public partial class AlterTablePlanoControleRemoveRenovacaoEPagamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataProximaRenovacao",
                table: "Planos");

            migrationBuilder.DropColumn(
                name: "Pago",
                table: "Planos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
