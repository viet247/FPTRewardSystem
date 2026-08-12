using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FPTRewardSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class AddingWalletsColumnForMerchantProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MerchantProfileId",
                table: "Wallets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "MerchantProfileId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "MerchantProfileId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                column: "MerchantProfileId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_MerchantProfileId",
                table: "Wallets",
                column: "MerchantProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_MerchantProfile_MerchantProfileId",
                table: "Wallets",
                column: "MerchantProfileId",
                principalTable: "MerchantProfile",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_MerchantProfile_MerchantProfileId",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_MerchantProfileId",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "MerchantProfileId",
                table: "Wallets");
        }
    }
}
