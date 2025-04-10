using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_Community.Migrations
{
    /// <inheritdoc />
    public partial class fixWorkoutDaysFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutDays_StaticPlans_StaticPlanId",
                table: "WorkoutDays");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutDays_StaticPlanId",
                table: "WorkoutDays");

            migrationBuilder.DropColumn(
                name: "StaticPlanId",
                table: "WorkoutDays");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StaticPlanId",
                table: "WorkoutDays",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutDays_StaticPlanId",
                table: "WorkoutDays",
                column: "StaticPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutDays_StaticPlans_StaticPlanId",
                table: "WorkoutDays",
                column: "StaticPlanId",
                principalTable: "StaticPlans",
                principalColumn: "Id");
        }
    }
}
