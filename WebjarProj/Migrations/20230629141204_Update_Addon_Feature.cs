using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebjarProj.Migrations
{
    /// <inheritdoc />
    public partial class Update_Addon_Feature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addons_Products_ProductId",
                table: "Addons");

            migrationBuilder.DropForeignKey(
                name: "FK_Features_Products_ProductId",
                table: "Features");

            migrationBuilder.DropIndex(
                name: "IX_Features_ProductId",
                table: "Features");

            migrationBuilder.DropIndex(
                name: "IX_Addons_ProductId",
                table: "Addons");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Addons");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Features",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Addons",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Features_ProductId",
                table: "Features",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Addons_ProductId",
                table: "Addons",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addons_Products_ProductId",
                table: "Addons",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Features_Products_ProductId",
                table: "Features",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
