using Microsoft.EntityFrameworkCore.Migrations;

namespace DrinkVendingMachine.Migrations
{
    public partial class Machine22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    One = table.Column<int>(nullable: true),
                    Two = table.Column<int>(nullable: true),
                    Five = table.Column<int>(nullable: true),
                    Ten = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    IdProduct = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sprite = table.Column<string>(nullable: true),
                    NameSprite = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Products__2E8946D4DFAE69AA", x => x.IdProduct);
                });

            migrationBuilder.CreateTable(
                name: "Sprites",
                columns: table => new
                {
                    IdSprite = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Sprites__CBDCF78DF64EC46F", x => x.IdSprite);
                });

            migrationBuilder.CreateTable(
                name: "Memory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    One = table.Column<int>(nullable: false),
                    Two = table.Column<int>(nullable: false),
                    Five = table.Column<int>(nullable: false),
                    Ten = table.Column<int>(nullable: false),
                    Sum = table.Column<int>(nullable: false),
                    ProductsIdProduct = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Memory",
                        column: x => x.ProductsIdProduct,
                        principalTable: "Products",
                        principalColumn: "IdProduct",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Memory_ProductsIdProduct",
                table: "Memory",
                column: "ProductsIdProduct");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coins");

            migrationBuilder.DropTable(
                name: "Memory");

            migrationBuilder.DropTable(
                name: "Sprites");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
