using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetCoreIdentityApp.Web.Migrations
{
    /// <inheritdoc />
    public partial class LoveBerfinSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LoveBerfin",
                columns: new[] { "Id", "LoveName", "Name" },
                values: new object[] { 1, "Canberk", "Berfin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LoveBerfin",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
