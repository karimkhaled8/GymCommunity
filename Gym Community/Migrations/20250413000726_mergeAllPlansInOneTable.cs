using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_Community.Migrations
{
    /// <inheritdoc />
    public partial class mergeAllPlansInOneTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeekPlan_ClientPlans_ClientPlanId",
                table: "WeekPlan");

            migrationBuilder.DropTable(
                name: "ClientPlans");

            migrationBuilder.DropTable(
                name: "StaticDailyExercises");

            migrationBuilder.DropTable(
                name: "StaticDailyMeals");

            migrationBuilder.DropTable(
                name: "StaticWorkoutDays");

            migrationBuilder.DropTable(
                name: "StaticPlans");

            migrationBuilder.RenameColumn(
                name: "ClientPlanId",
                table: "WeekPlan",
                newName: "TrainingPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_WeekPlan_ClientPlanId",
                table: "WeekPlan",
                newName: "IX_WeekPlan_TrainingPlanId");

            migrationBuilder.CreateTable(
                name: "TrainingPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoachId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsStaticPlan = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DurationMonths = table.Column<int>(type: "int", nullable: false),
                    FrequencyPerWeek = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CaloricTarget = table.Column<int>(type: "int", nullable: false),
                    ProteinPercentage = table.Column<int>(type: "int", nullable: false),
                    CarbsPercentage = table.Column<int>(type: "int", nullable: false),
                    FatsPercentage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingPlans_AspNetUsers_CoachId",
                        column: x => x.CoachId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPlans_CoachId",
                table: "TrainingPlans",
                column: "CoachId");

            migrationBuilder.AddForeignKey(
                name: "FK_WeekPlan_TrainingPlans_TrainingPlanId",
                table: "WeekPlan",
                column: "TrainingPlanId",
                principalTable: "TrainingPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeekPlan_TrainingPlans_TrainingPlanId",
                table: "WeekPlan");

            migrationBuilder.DropTable(
                name: "TrainingPlans");

            migrationBuilder.RenameColumn(
                name: "TrainingPlanId",
                table: "WeekPlan",
                newName: "ClientPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_WeekPlan_TrainingPlanId",
                table: "WeekPlan",
                newName: "IX_WeekPlan_ClientPlanId");

            migrationBuilder.CreateTable(
                name: "ClientPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CoachId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CaloricTarget = table.Column<int>(type: "int", nullable: false),
                    CarbsPercentage = table.Column<int>(type: "int", nullable: false),
                    DurationMonths = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FatsPercentage = table.Column<int>(type: "int", nullable: false),
                    FrequencyPerWeek = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProteinPercentage = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientPlans_AspNetUsers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientPlans_AspNetUsers_CoachId",
                        column: x => x.CoachId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaticDailyExercises",
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
                    table.PrimaryKey("PK_StaticDailyExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaticDailyExercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaticDailyMeals",
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
                    table.PrimaryKey("PK_StaticDailyMeals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaticDailyMeals_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaticPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaloricTarget = table.Column<int>(type: "int", nullable: false),
                    CarbsPercentage = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FatsPercentage = table.Column<int>(type: "int", nullable: false),
                    FrequencyPerWeek = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProteinPercentage = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaticWorkoutDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaticPlanId = table.Column<int>(type: "int", nullable: false),
                    DailyPlanJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DayDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticWorkoutDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaticWorkoutDays_StaticPlans_StaticPlanId",
                        column: x => x.StaticPlanId,
                        principalTable: "StaticPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientPlans_ClientId",
                table: "ClientPlans",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientPlans_CoachId",
                table: "ClientPlans",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_StaticDailyExercises_ExerciseId",
                table: "StaticDailyExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_StaticDailyMeals_MealId",
                table: "StaticDailyMeals",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_StaticWorkoutDays_StaticPlanId",
                table: "StaticWorkoutDays",
                column: "StaticPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_WeekPlan_ClientPlans_ClientPlanId",
                table: "WeekPlan",
                column: "ClientPlanId",
                principalTable: "ClientPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
