using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class addproductIduserIdtoComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Product_ProductID",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Users_UserId",
                table: "Comment");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "9f97a9c9-f44c-4aaf-b53e-c471937b089e");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "a7b6047b-66a3-4d0b-83ac-c31275991c71");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "b5c4e3d0-328e-42d1-8dc6-d6c751bd9971");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "Comment",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ProductID",
                table: "Comment",
                newName: "IX_Comment_ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Comment",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "Comment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "98086a6b-bcce-41c7-a0ab-adaf0d48e6ce", "7122dc1b-2acd-49ae-8751-7aef768e2c32", "Administrator", "ADMINISTRATOR" },
                    { "c3c00d0a-01a9-447b-9011-a32b809ca899", "582efbba-96ca-4641-928a-617c1a40e1c9", "Customer", "CUSTOMER" },
                    { "f71d25aa-8bac-44bc-ac88-61ffc68d32bb", "72a59656-29b4-4858-a731-c7b3f53921d2", "ShopOwner", "SHOPOWNER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Product_ProductId",
                table: "Comment",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Users_UserId",
                table: "Comment",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Product_ProductId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Users_UserId",
                table: "Comment");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "98086a6b-bcce-41c7-a0ab-adaf0d48e6ce");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "c3c00d0a-01a9-447b-9011-a32b809ca899");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "f71d25aa-8bac-44bc-ac88-61ffc68d32bb");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Comment",
                newName: "ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ProductId",
                table: "Comment",
                newName: "IX_Comment_ProductID");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Comment",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<long>(
                name: "ProductID",
                table: "Comment",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9f97a9c9-f44c-4aaf-b53e-c471937b089e", "adf466e0-85d4-4c72-a9fe-02f5188b9e4a", "ShopOwner", "SHOPOWNER" },
                    { "a7b6047b-66a3-4d0b-83ac-c31275991c71", "88a40654-06df-406b-a4b1-809cf19cb334", "Administrator", "ADMINISTRATOR" },
                    { "b5c4e3d0-328e-42d1-8dc6-d6c751bd9971", "ce740bed-d6f5-4583-8fac-dd8a0c3d06d7", "Customer", "CUSTOMER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Product_ProductID",
                table: "Comment",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Users_UserId",
                table: "Comment",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
