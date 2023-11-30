using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HojeEuCaso.Migrations
{
    public partial class addFotosServicos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FotosServicos",
                columns: table => new
                {
                    FotosServicosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PacoteID = table.Column<int>(type: "int", nullable: false),
                    CaminhoFoto = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FotosServicos", x => x.FotosServicosID);
                    table.ForeignKey(
                        name: "FK_FotosServicos_Pacotes_PacoteID",
                        column: x => x.PacoteID,
                        principalTable: "Pacotes",
                        principalColumn: "PacoteID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_FotosServicos_PacoteID",
                table: "FotosServicos",
                column: "PacoteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FotosServicos");
        }
    }
}
