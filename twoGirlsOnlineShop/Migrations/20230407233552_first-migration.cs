using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace twoGirlsOnlineShop.Migrations
{
    /// <inheritdoc />
    public partial class firstmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    City = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    BuildingNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    UnitNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    PostCode = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsClose = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreditCardNumber = table.Column<int>(type: "int", maxLength: 19, nullable: false),
                    Year = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    Month = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    CVV2 = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditCards_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", maxLength: 64, nullable: false),
                    SalesPrice = table.Column<decimal>(type: "decimal(18,2)", maxLength: 64, nullable: false),
                    DiscountedPrice = table.Column<decimal>(type: "decimal(18,2)", maxLength: 64, nullable: false),
                    QuantityInStock = table.Column<int>(type: "int", maxLength: 16, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CardItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardItems_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryToProducts",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryToProducts", x => new { x.CategoryId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_CategoryToProducts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryToProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImagePaths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagePaths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagePaths_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImagePaths_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Review_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "Men's" },
                    { 2, null, "Women's" },
                    { 3, null, "New Collection" },
                    { 4, null, "Designer Outlet" },
                    { 5, null, "Fashion and Trends" },
                    { 6, null, "Sports Glasses" },
                    { 7, null, "Best Sellers" },
                    { 8, null, "favorite" },
                    { 9, null, "Fashion and Trends" },
                    { 10, null, "Glasses for Couples" },
                    { 11, null, "Discounted" },
                    { 12, null, "Most Visited" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "", 1255m, new DateTime(2023, 3, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7814), 1489m, 41, new DateTime(2023, 3, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7769), 1391m, "Reyban Genwux 941", null },
                    { 2, "", 1058m, new DateTime(2023, 3, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7824), 1014m, 48, new DateTime(2023, 2, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7820), 1379m, "Reyban Genwux 941", null },
                    { 3, "", 1191m, new DateTime(2023, 4, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7833), 1227m, 32, new DateTime(2023, 2, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7829), 1329m, "Reyban Genwux 941", null },
                    { 4, "", 1167m, new DateTime(2023, 3, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7842), 1451m, 42, new DateTime(2023, 4, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7838), 1094m, "Reyban Genwux 941", null },
                    { 5, "", 1406m, new DateTime(2023, 2, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7851), 1446m, 32, new DateTime(2023, 4, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7846), 1143m, "Reyban Genwux 941", null },
                    { 6, "", 1403m, new DateTime(2023, 4, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7859), 1417m, 28, new DateTime(2023, 3, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7855), 1017m, "Reyban Genwux 941", null },
                    { 7, "", 1184m, new DateTime(2023, 2, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7868), 1182m, 41, new DateTime(2023, 3, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7863), 1408m, "Reyban Genwux 941", null },
                    { 8, "", 1335m, new DateTime(2023, 3, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7880), 1134m, 41, new DateTime(2023, 2, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7872), 1271m, "Reyban Genwux 941", null },
                    { 9, "", 1293m, new DateTime(2023, 3, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7888), 1182m, 18, new DateTime(2023, 2, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7885), 1371m, "Reyban Genwux 941", null },
                    { 10, "", 1276m, new DateTime(2023, 2, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7895), 1087m, 24, new DateTime(2023, 3, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7891), 1313m, "Reyban Genwux 941", null },
                    { 11, "", 1145m, new DateTime(2023, 3, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7901), 1317m, 32, new DateTime(2023, 3, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7898), 1084m, "Reyban Genwux 941", null },
                    { 12, "", 1439m, new DateTime(2023, 3, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7908), 1063m, 42, new DateTime(2023, 4, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7904), 1386m, "Reyban Genwux 941", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ConfirmPassword", "Email", "FirstName", "IsAdmin", "Password", "PhoneNumber", "lastName" },
                values: new object[,]
                {
                    { 1, "123", "aqamiladam@gmail.com", "Milad", true, "123", "+420730834642", "Khalatbari" },
                    { 2, "123", "samanafra@gmail.com", "saman", false, "123", "+0985858585", "afrasiabi" }
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "IsClose", "UserId" },
                values: new object[] { 1, false, 1 });

            migrationBuilder.InsertData(
                table: "CategoryToProducts",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 3 },
                    { 1, 6 },
                    { 1, 9 },
                    { 1, 12 },
                    { 2, 1 },
                    { 2, 3 },
                    { 2, 7 },
                    { 2, 12 },
                    { 3, 1 },
                    { 3, 5 },
                    { 3, 7 },
                    { 3, 10 },
                    { 4, 1 },
                    { 4, 3 },
                    { 4, 9 },
                    { 4, 12 },
                    { 5, 1 },
                    { 5, 3 },
                    { 5, 8 },
                    { 6, 4 },
                    { 6, 9 },
                    { 6, 11 },
                    { 7, 1 },
                    { 7, 4 },
                    { 7, 8 },
                    { 7, 10 },
                    { 8, 2 },
                    { 8, 5 },
                    { 8, 7 },
                    { 8, 11 },
                    { 9, 2 },
                    { 9, 5 },
                    { 10, 2 },
                    { 10, 6 },
                    { 11, 2 },
                    { 11, 6 },
                    { 12, 2 }
                });

            migrationBuilder.InsertData(
                table: "ImagePaths",
                columns: new[] { "Id", "ProductId", "Url", "UserId" },
                values: new object[,]
                {
                    { 1, 1, "/image/sunglasses/v1.webp", null },
                    { 2, 1, "/image/sunglasses/v2.webp", null },
                    { 3, 2, "/image/sunglasses/a1.webp", null },
                    { 4, 2, "/image/sunglasses/a2.webp", null },
                    { 5, 3, "/image/sunglasses/b1.webp", null },
                    { 6, 3, "/image/sunglasses/b2.webp", null },
                    { 7, 4, "/image/sunglasses/c1.webp", null },
                    { 8, 4, "/image/sunglasses/c2.webp", null },
                    { 9, 5, "/image/sunglasses/e1.webp", null },
                    { 10, 5, "/image/sunglasses/e2.webp", null },
                    { 11, 6, "/image/sunglasses/f1.webp", null },
                    { 12, 6, "/image/sunglasses/f2.webp", null },
                    { 13, 7, "/image/sunglasses/g1.webp", null },
                    { 14, 7, "/image/sunglasses/g2.webp", null },
                    { 15, 8, "/image/sunglasses/s1.webp", null },
                    { 16, 8, "/image/sunglasses/s2.webp", null },
                    { 17, 9, "/image/sunglasses/h1.webp", null },
                    { 18, 9, "/image/sunglasses/h2.webp", null },
                    { 19, 10, "/image/sunglasses/w1.webp", null },
                    { 20, 10, "/image/sunglasses/w2.webp", null },
                    { 21, 11, "/image/sunglasses/r1.webp", null },
                    { 22, 11, "/image/sunglasses/r2.webp", null },
                    { 23, 12, "/image/sunglasses/q1.webp", null },
                    { 24, 12, "/image/sunglasses/q2.webp", null }
                });

            migrationBuilder.InsertData(
                table: "Review",
                columns: new[] { "Id", "Comment", "ProductId", "Rate", "UserId" },
                values: new object[,]
                {
                    { 1, "hamamash dagh bood nooshaba dadan porteghal zadan dan dadan !", 1, 1, 1 },
                    { 2, "hamamash dagh bood nooshaba dadan porteghal zadan dan dadan !", 2, 2, 1 },
                    { 3, "hamamash dagh bood nooshaba dadan porteghal zadan dan dadan !", 5, 3, 1 },
                    { 4, "hamamash dagh bood nooshaba dadan porteghal zadan dan dadan !", 6, 2, 1 },
                    { 5, "hamamash dagh bood nooshaba dadan porteghal zadan dan dadan !", 2, 5, 2 },
                    { 6, "hamamash dagh bood nooshaba dadan porteghal zadan dan dadan !", 1, 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "CardItems",
                columns: new[] { "Id", "CardId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 1, 2, 2 },
                    { 3, 1, 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CardItems_CardId",
                table: "CardItems",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_CardItems_ProductId",
                table: "CardItems",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_UserId",
                table: "Cards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryToProducts_ProductId",
                table: "CategoryToProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_UserId",
                table: "CreditCards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagePaths_ProductId",
                table: "ImagePaths",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagePaths_UserId",
                table: "ImagePaths",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_ProductId",
                table: "Review",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserId",
                table: "Review",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "CardItems");

            migrationBuilder.DropTable(
                name: "CategoryToProducts");

            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropTable(
                name: "ImagePaths");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
