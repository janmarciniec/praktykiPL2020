using Microsoft.EntityFrameworkCore.Migrations;

namespace Praktyki.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DayId",
                table: "Reservations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_DayId",
                table: "Reservations",
                column: "DayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Days_DayId",
                table: "Reservations",
                column: "DayId",
                principalTable: "Days",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Days_DayId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_DayId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "DayId",
                table: "Reservations");
        }
    }
}
