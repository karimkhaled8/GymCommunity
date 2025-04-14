using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_Community.Migrations
{
    /// <inheritdoc />
    public partial class QRCodeRowdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "rawData",
                table: "UserSubscriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rawData",
                table: "UserSubscriptions");
        }
    }
}
