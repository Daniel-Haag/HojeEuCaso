using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HojeEuCaso.Migrations
{
    public partial class createTablePlanoFornecedor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlanosFornecedores",
                columns: table => new
                {
                    PlanoFornecedorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FornecedorID = table.Column<int>(type: "int", nullable: false),
                    PlanoID = table.Column<int>(type: "int", nullable: false),
                    DataProximaRenovacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Pago = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanosFornecedores", x => x.PlanoFornecedorID);
                    table.ForeignKey(
                        name: "FK_PlanosFornecedores_Fornecedores_FornecedorID",
                        column: x => x.FornecedorID,
                        principalTable: "Fornecedores",
                        principalColumn: "FornecedorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanosFornecedores_Planos_PlanoID",
                        column: x => x.PlanoID,
                        principalTable: "Planos",
                        principalColumn: "PlanoID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PlanosFornecedores_FornecedorID",
                table: "PlanosFornecedores",
                column: "FornecedorID");

            migrationBuilder.CreateIndex(
                name: "IX_PlanosFornecedores_PlanoID",
                table: "PlanosFornecedores",
                column: "PlanoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanosFornecedores");
        }
    }
}
