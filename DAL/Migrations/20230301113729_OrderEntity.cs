using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class OrderEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "8c561f25-de26-4006-ab3a-e5421fbf5b59");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "e8931795-86de-4321-b1ea-12f494a70da3");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c5f542f8-7700-466d-abc2-eb9802780d4b", "815af01f-42a2-478a-b811-a3eeda1aa1a3", "Visitor", "VISITOR" },
                    { "f7e7887d-6190-4d9f-b5f2-52c6e1da18fa", "84bc1941-a95c-4761-b788-4d56993cb2ec", "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "c5f542f8-7700-466d-abc2-eb9802780d4b");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "f7e7887d-6190-4d9f-b5f2-52c6e1da18fa");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8c561f25-de26-4006-ab3a-e5421fbf5b59", "768579c2-b83f-4e5a-9810-244a1dff5866", "Visitor", "VISITOR" },
                    { "e8931795-86de-4321-b1ea-12f494a70da3", "6f706e99-60c3-4817-847d-63c3d4ea0f5a", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
