using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym_Community.Migrations
{
    /// <inheritdoc />
    public partial class planpayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "paymentId",
                table: "TrainingPlans",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPlans_paymentId",
                table: "TrainingPlans",
                column: "paymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPlans_Payments_paymentId",
                table: "TrainingPlans",
                column: "paymentId",
                principalTable: "Payments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPlans_Payments_paymentId",
                table: "TrainingPlans");

            migrationBuilder.DropIndex(
                name: "IX_TrainingPlans_paymentId",
                table: "TrainingPlans");

            migrationBuilder.DropColumn(
                name: "paymentId",
                table: "TrainingPlans");
        }
    }
}
