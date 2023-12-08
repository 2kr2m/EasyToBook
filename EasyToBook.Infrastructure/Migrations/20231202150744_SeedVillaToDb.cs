using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EasyToBook.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedVillaToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "CreatedAt", "Description", "ImageUrl", "Name", "Occupancy", "Price", "Sqft", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, null, "Royal Villa Royal Villa Royal Villa Royal Villa Royal Villa Royal Villa Royal Villa Royal Villa", "https://assets.palmspringslife.com/wp-content/uploads/2018/12/26160745/villa-royale.jpg", "Royal Villa", 4, 200.0, 550, null },
                    { 2, null, "Royal Villa Royal Villa Royal Villa Royal Villa Royal Villa Royal Villa Royal Villa Royal Villa", "https://assets.palmspringslife.com/wp-content/uploads/2018/12/26160745/villa-royale.jpg", "Royal Villa", 4, 200.0, 550, null },
                    { 3, null, "Royal Villa Royal Villa Royal Villa Royal Villa Royal Villa Royal Villa Royal Villa Royal Villa", "https://assets.palmspringslife.com/wp-content/uploads/2018/12/26160745/villa-royale.jpg", "Royal Villa", 4, 200.0, 550, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
