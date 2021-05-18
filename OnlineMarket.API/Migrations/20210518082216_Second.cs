using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineMarket.API.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AverageRating",
                table: "Products",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ProductReviewId",
                table: "Image",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Image_ProductReviewId",
                table: "Image",
                column: "ProductReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Reviews_ProductReviewId",
                table: "Image",
                column: "ProductReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Reviews_ProductReviewId",
                table: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Image_ProductReviewId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductReviewId",
                table: "Image");
        }
    }
}
