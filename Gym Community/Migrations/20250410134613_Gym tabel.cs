using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_Community.Migrations
{
    /// <inheritdoc />
    public partial class Gymtabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gym",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gym", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gym_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "GymCoaches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GymId = table.Column<int>(type: "int", nullable: false),
                    CoachID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymCoaches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GymCoaches_AspNetUsers_CoachID",
                        column: x => x.CoachID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_GymCoaches_Gym_GymId",
                        column: x => x.GymId,
                        principalTable: "Gym",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "GymImgs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GymId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymImgs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GymImgs_Gym_GymId",
                        column: x => x.GymId,
                        principalTable: "Gym",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "GymPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GymId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DurationMonths = table.Column<int>(type: "int", nullable: false),
                    HasPrivateCoach = table.Column<bool>(type: "bit", nullable: false),
                    HasNutritionPlan = table.Column<bool>(type: "bit", nullable: false),
                    HasAccessToAllAreas = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GymPlans_Gym_GymId",
                        column: x => x.GymId,
                        principalTable: "Gym",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "UserSubscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GymId = table.Column<int>(type: "int", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    paymentStatus = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QrCodeData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsExpired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSubscriptions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserSubscriptions_GymPlans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "GymPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserSubscriptions_Gym_GymId",
                        column: x => x.GymId,
                        principalTable: "Gym",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gym_OwnerId",
                table: "Gym",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_GymCoaches_CoachID",
                table: "GymCoaches",
                column: "CoachID");

            migrationBuilder.CreateIndex(
                name: "IX_GymCoaches_GymId",
                table: "GymCoaches",
                column: "GymId");

            migrationBuilder.CreateIndex(
                name: "IX_GymImgs_GymId",
                table: "GymImgs",
                column: "GymId");

            migrationBuilder.CreateIndex(
                name: "IX_GymPlans_GymId",
                table: "GymPlans",
                column: "GymId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscriptions_GymId",
                table: "UserSubscriptions",
                column: "GymId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscriptions_PlanId",
                table: "UserSubscriptions",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscriptions_UserId",
                table: "UserSubscriptions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GymCoaches");

            migrationBuilder.DropTable(
                name: "GymImgs");

            migrationBuilder.DropTable(
                name: "UserSubscriptions");

            migrationBuilder.DropTable(
                name: "GymPlans");

            migrationBuilder.DropTable(
                name: "Gym");
        }
    }
}
