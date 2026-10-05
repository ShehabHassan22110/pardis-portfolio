using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BardeesCms.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkshopSection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkshopSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Eyebrow = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EyebrowAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScheduleText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScheduleTextAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationTextAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Highlights = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HighlightsAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ButtonText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ButtonTextAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhatsAppMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhatsAppMessageAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkshopSections", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkshopSections");
        }
    }
}
