using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PalworldServerManagerClient.Migrations
{
    /// <inheritdoc />
    public partial class PropertyNamingFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IPAdresse",
                table: "ServerInfos",
                newName: "IPAddresse");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IPAddresse",
                table: "ServerInfos",
                newName: "IPAdresse");
        }
    }
}
