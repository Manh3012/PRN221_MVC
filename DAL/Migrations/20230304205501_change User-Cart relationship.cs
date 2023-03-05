using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class changeUserCartrelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "1ea7d8ca-0007-4370-a608-bd08e0c2d4da");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "3465f447-e82d-4313-9909-99b568d54079");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "5665b7a6-e32f-4496-a683-2c781c6cbae3");

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6c227c49-ab4f-4484-b67e-4161131c5e6c", "75e94c8f-932e-4bf2-9924-1b29e3d379be", "Customer", "CUSTOMER" },
                    { "a54f603b-3d3f-4df7-83f5-d20ac46a1cb9", "88a53f8c-1091-4d7e-8f6c-7fd8ace0809a", "ShopOwner", "SHOPOWNER" },
                    { "c65511b8-3fcc-49f0-9bda-7419f8912732", "e15909b5-9d3a-4d6f-a63b-1b7e0b9e3029", "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "6c227c49-ab4f-4484-b67e-4161131c5e6c");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "a54f603b-3d3f-4df7-83f5-d20ac46a1cb9");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "c65511b8-3fcc-49f0-9bda-7419f8912732");

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1ea7d8ca-0007-4370-a608-bd08e0c2d4da", "558b1e90-dc3d-48e3-8770-8c2e37d572bf", "Customer", "CUSTOMER" },
                    { "3465f447-e82d-4313-9909-99b568d54079", "4fa80854-9cad-4f3f-89f0-a398136c5bdc", "ShopOwner", "SHOPOWNER" },
                    { "5665b7a6-e32f-4496-a683-2c781c6cbae3", "682de5f2-323c-4275-8b21-38b248967d18", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
