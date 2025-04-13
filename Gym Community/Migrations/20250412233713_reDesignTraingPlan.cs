using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_Community.Migrations
{
    /// <inheritdoc />
    public partial class reDesignTraingPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyExercises");

            migrationBuilder.DropTable(
                name: "DailyMeals");

            migrationBuilder.DropTable(
                name: "WorkoutDays");

            migrationBuilder.CreateTable(
                name: "WeekPlan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientPlanId = table.Column<int>(type: "int", nullable: false),
                    WeekName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeekPlan_ClientPlans_ClientPlanId",
                        column: x => x.ClientPlanId,
                        principalTable: "ClientPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyPlan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeekPlanId = table.Column<int>(type: "int", nullable: false),
                    ExtraTips = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DayDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayNumber = table.Column<int>(type: "int", nullable: false),
                    DailyPlanJson = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyPlan_WeekPlan_WeekPlanId",
                        column: x => x.WeekPlanId,
                        principalTable: "WeekPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyPlan_WeekPlanId",
                table: "DailyPlan",
                column: "WeekPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_WeekPlan_ClientPlanId",
                table: "WeekPlan",
                column: "ClientPlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyPlan");

            migrationBuilder.DropTable(
                name: "WeekPlan");

            migrationBuilder.CreateTable(
                name: "DailyExercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reps = table.Column<int>(type: "int", nullable: false),
                    Sets = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyExercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyMeals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MealId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SuggestedTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyMeals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyMeals_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientPlanId = table.Column<int>(type: "int", nullable: false),
                    DailyPlanJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DayDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutDays_ClientPlans_ClientPlanId",
                        column: x => x.ClientPlanId,
                        principalTable: "ClientPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyExercises_ExerciseId",
                table: "DailyExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyMeals_MealId",
                table: "DailyMeals",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutDays_ClientPlanId",
                table: "WorkoutDays",
                column: "ClientPlanId");
        }
    }
}
