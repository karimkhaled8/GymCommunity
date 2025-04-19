using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_Community.Migrations
{
    /// <inheritdoc />
    public partial class updateclientinfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "ClientInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoverImg",
                table: "ClientInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "bodyFat",
                table: "ClientInfo",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio",
                table: "ClientInfo");

            migrationBuilder.DropColumn(
                name: "CoverImg",
                table: "ClientInfo");

            migrationBuilder.DropColumn(
                name: "bodyFat",
                table: "ClientInfo");
        }
    }
}
