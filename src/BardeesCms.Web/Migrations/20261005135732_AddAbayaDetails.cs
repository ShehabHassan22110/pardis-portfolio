using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BardeesCms.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddAbayaDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Abayas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AbayaImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AbayaId = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaptionAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AltText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AltTextAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbayaImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbayaImages_Abayas_AbayaId",
                        column: x => x.AbayaId,
                        principalTable: "Abayas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbayaImages_AbayaId",
                table: "AbayaImages",
                column: "AbayaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbayaImages");

            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Abayas");
        }
    }
}
