using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineMarket.API.Migrations
{
    public partial class sodfaihdoifj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_WishList_WishListId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_WishListId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WishListId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "WishList",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemUserId",
                table: "WishList",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WishList_ProductId",
                table: "WishList",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WishList_SystemUserId",
                table: "WishList",
                column: "SystemUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WishList_AspNetUsers_SystemUserId",
                table: "WishList",
                column: "SystemUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WishList_Products_ProductId",
                table: "WishList",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishList_AspNetUsers_SystemUserId",
                table: "WishList");

            migrationBuilder.DropForeignKey(
                name: "FK_WishList_Products_ProductId",
                table: "WishList");

            migrationBuilder.DropIndex(
                name: "IX_WishList_ProductId",
                table: "WishList");

            migrationBuilder.DropIndex(
                name: "IX_WishList_SystemUserId",
                table: "WishList");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "WishList");

            migrationBuilder.DropColumn(
                name: "SystemUserId",
                table: "WishList");

            migrationBuilder.AddColumn<int>(
                name: "WishListId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WishListId",
                table: "AspNetUsers",
                column: "WishListId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_WishList_WishListId",
                table: "AspNetUsers",
                column: "WishListId",
                principalTable: "WishList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
