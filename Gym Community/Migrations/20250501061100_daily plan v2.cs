using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_Community.Migrations
{
    /// <inheritdoc />
    public partial class dailyplanv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalCalories",
                table: "DailyPlan",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalCarbs",
                table: "DailyPlan",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalFats",
                table: "DailyPlan",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalProtein",
                table: "DailyPlan",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCalories",
                table: "DailyPlan");

            migrationBuilder.DropColumn(
                name: "TotalCarbs",
                table: "DailyPlan");

            migrationBuilder.DropColumn(
                name: "TotalFats",
                table: "DailyPlan");

            migrationBuilder.DropColumn(
                name: "TotalProtein",
                table: "DailyPlan");
        }
    }
}
