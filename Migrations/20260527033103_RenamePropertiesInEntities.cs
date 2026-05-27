using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FPTRewardSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class RenamePropertiesInEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MerchantProfile_User_UserID",
                table: "MerchantProfile");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "User",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "MerchantProfile",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "MerchantProfile",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_MerchantProfile_UserID",
                table: "MerchantProfile",
                newName: "IX_MerchantProfile_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "FullName", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "wva@fpt.com", "Nguyen Van A", null, 3 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "ttb@fpt.com", "Tran Thi B", null, 3 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "cafe@merchant.com", "Chu Cua Hang Cafe", null, 2 }
                });

            migrationBuilder.InsertData(
                table: "MerchantProfile",
                columns: new[] { "Id", "Address", "StoreName", "UserId" },
                values: new object[] { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), "Toa nha Alpha, Hoa Lac", "FPT HighLands Cafe", new Guid("33333333-3333-3333-3333-333333333333") });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "UserId" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 500m, new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 300m, new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), 0m, new Guid("33333333-3333-3333-3333-333333333333") }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MerchantProfile_User_UserId",
                table: "MerchantProfile",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MerchantProfile_User_UserId",
                table: "MerchantProfile");

            migrationBuilder.DeleteData(
                table: "MerchantProfile",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "User",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "MerchantProfile",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MerchantProfile",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_MerchantProfile_UserId",
                table: "MerchantProfile",
                newName: "IX_MerchantProfile_UserID");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MerchantProfile_User_UserID",
                table: "MerchantProfile",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
