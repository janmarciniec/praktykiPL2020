using Microsoft.EntityFrameworkCore.Migrations;

namespace Praktyki.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Days",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Poniedziałek" },
                    { 2, "Wtorek" },
                    { 3, "Środa" },
                    { 4, "Czwartek" },
                    { 5, "Piątek" }
                });

            migrationBuilder.InsertData(
                table: "Hours",
                columns: new[] { "Id", "Hours" },
                values: new object[,]
                {
                    { 1, "8-10" },
                    { 2, "10-12" },
                    { 3, "12-14" },
                    { 4, "14-16" },
                    { 5, "16-18" },
                    { 6, "18-20" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DeleteData(
                table: "Hours",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hours",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hours",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hours",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Hours",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Hours",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
