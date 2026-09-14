using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BardeesCms.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddThemeLogos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoDark",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoLight",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoDark",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "LogoLight",
                table: "SiteSettings");
        }
    }
}
