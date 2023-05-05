using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TwoGirls.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class firstmigbuilddb : Migration
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
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalesPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountedPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantityInStock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RoleId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                    table.ForeignKey(
                        name: "FK_Roles_Roles_RoleId1",
                        column: x => x.RoleId1,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "TransactionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ActiveCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
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
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    City = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    BuildingNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
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
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    OwnerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreditCardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    CVV = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
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
                name: "Favorites",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => new { x.UserId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_Favorites_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    Rate = table.Column<double>(type: "float", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Finaly = table.Column<bool>(type: "bit", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_TransactionTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "TransactionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                columns: new[] { "Id", "Description", "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice", "Title" },
                values: new object[,]
                {
                    { 1, "", 1166m, new DateTime(2023, 4, 5, 11, 34, 2, 405, DateTimeKind.Local).AddTicks(7737), 1228m, 33, new DateTime(2023, 4, 5, 11, 34, 2, 405, DateTimeKind.Local).AddTicks(7732), 1296m, "Reyban Genwux 941" },
                    { 2, "", 1121m, new DateTime(2023, 5, 5, 11, 34, 2, 405, DateTimeKind.Local).AddTicks(7745), 1483m, 18, new DateTime(2023, 4, 5, 11, 34, 2, 405, DateTimeKind.Local).AddTicks(7741), 1139m, "Reyban Genwux 941" },
                    { 3, "", 1215m, new DateTime(2023, 3, 5, 11, 34, 2, 405, DateTimeKind.Local).AddTicks(7751), 1146m, 21, new DateTime(2023, 3, 5, 11, 34, 2, 405, DateTimeKind.Local).AddTicks(7748), 1068m, "Reyban Genwux 941" },
                    { 4, "", 1081m, new DateTime(2023, 3, 5, 11, 34, 2, 405, DateTimeKind.Local).AddTicks(7758), 1049m, 23, new DateTime(2023, 5, 5, 11, 34, 2, 405, DateTimeKind.Local).AddTicks(7754), 1109m, "Reyban Genwux 941" },
                    { 5, "", 1409m, new DateTime(2023, 4, 5, 11, 34, 2, 405, DateTimeKind.Local).AddTicks(7765), 1155m, 21, new DateTime(2023, 4, 5, 11, 34, 2, 405, DateTimeKind.Local).AddTicks(7761), 1010m, "Reyban Genwux 941" },
                    { 6, "", 1357m, new DateTime(2023, 4, 5, 11, 34, 2, 405, DateTimeKind.Local).AddTicks(7771), 1452m, 44, new DateTime(2023, 3, 5, 11, 34, 2, 405, DateTimeKind.Local).AddTicks(7768), 1217m, "Reyban Genwux 941" },
                    { 7, "", 1251m, new DateTime(2023, 5, 5, 11, 34, 2, 405, DateTimeKind.Local).AddTicks(7779), 1281m, 40, new DateTime(2023, 5, 5, 11, 34, 2, 405, DateTimeKind.Local).AddTicks(7775), 1309m, "Reyban Genwux 941" },
                    { 8, "", 1381m, new DateTime(2023, 5, 5, 11, 34, 2, 405, DateTimeKind.Local).AddTicks(7785), 1203m, 28, new DateTime(2023, 5, 5, 11, 34, 2, 405, DateTimeKind.Local).AddTicks(7782), 1207m, "Reyban Genwux 941" },
                    { 9, "", 1310m, new DateTime(2023, 3, 5, 11, 34, 2, 405, DateTimeKind.Local).AddTicks(7792), 1284m, 47, new DateTime(2023, 5, 5, 11, 34, 2, 405, DateTimeKind.Local).AddTicks(7789), 1035m, "Reyban Genwux 941" },
                    { 10, "", 1189m, new DateTime(2023, 4, 5, 11, 34, 2, 405, DateTimeKind.Local).AddTicks(7799), 1238m, 35, new DateTime(2023, 3, 5, 11, 34, 2, 405, DateTimeKind.Local).AddTicks(7796), 1108m, "Reyban Genwux 941" },
                    { 11, "", 1413m, new DateTime(2023, 5, 5, 11, 34, 2, 405, DateTimeKind.Local).AddTicks(7806), 1424m, 26, new DateTime(2023, 3, 5, 11, 34, 2, 405, DateTimeKind.Local).AddTicks(7802), 1046m, "Reyban Genwux 941" },
                    { 12, "", 1240m, new DateTime(2023, 4, 5, 11, 34, 2, 405, DateTimeKind.Local).AddTicks(7812), 1245m, 12, new DateTime(2023, 4, 5, 11, 34, 2, 405, DateTimeKind.Local).AddTicks(7809), 1397m, "Reyban Genwux 941" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleId1", "RoleTitle" },
                values: new object[] { 1, null, "Leader" });

            migrationBuilder.InsertData(
                table: "TransactionTypes",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Deposit" },
                    { 2, "Withdraw" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ActiveCode", "Email", "FirstName", "ImagePath", "IsActive", "LastName", "Password", "PhoneNumber", "RegisterDate" },
                values: new object[,]
                {
                    { 1, "50975f6e0555441db20afedb3fd1e02f", "aqamiladam@gmail.com", "Milad", "/image/user-avatar/Milad_profile.jpg", true, "Khalatbari", "$HASH$V1$jhUAa6x2XLTQSttEBvsxYw==$Ib1JCvml1e9BLlaqTxYe+ugz3qHe/aig6ks9sNge0lk=", "+420730834642", new DateTime(2023, 5, 5, 11, 34, 2, 405, DateTimeKind.Local).AddTicks(7637) },
                    { 2, "90e2b14b48cc40deafcd275d96c50b94", "samanafra@gmail.com", "saman", "/image/user-avatar/saman_profile.jpg", true, "afrasiabi", "$HASH$V1$jhUAa6x2XLTQSttEBvsxYw==$Ib1JCvml1e9BLlaqTxYe+ugz3qHe/aig6ks9sNge0lk=", "+0985858585", new DateTime(2023, 5, 5, 11, 34, 2, 405, DateTimeKind.Local).AddTicks(7677) }
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "CreateDate", "IsClose", "UserId" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1 });

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
                columns: new[] { "Id", "Name", "ProductId", "Url", "UserId" },
                values: new object[,]
                {
                    { 1, "v1.webp", 1, "/image/sunglasses/v1.webp", null },
                    { 2, "v2.webp", 1, "/image/sunglasses/v2.webp", null },
                    { 3, "a1.webp", 2, "/image/sunglasses/a1.webp", null },
                    { 4, "a2.webp", 2, "/image/sunglasses/a2.webp", null },
                    { 5, "b1.webp", 3, "/image/sunglasses/b1.webp", null },
                    { 6, "b2.webp", 3, "/image/sunglasses/b2.webp", null },
                    { 7, "c1.webp", 4, "/image/sunglasses/c1.webp", null },
                    { 8, "c2.webp", 4, "/image/sunglasses/c2.webp", null },
                    { 9, "e1.webp", 5, "/image/sunglasses/e1.webp", null },
                    { 10, "e2.webp", 5, "/image/sunglasses/e2.webp", null },
                    { 11, "f1.webp", 6, "/image/sunglasses/f1.webp", null },
                    { 12, "f2.webp", 6, "/image/sunglasses/f2.webp", null },
                    { 13, "g1.webp", 7, "/image/sunglasses/g1.webp", null },
                    { 14, "g2.webp", 7, "/image/sunglasses/g2.webp", null },
                    { 15, "s1.webp", 8, "/image/sunglasses/s1.webp", null },
                    { 16, "s2.webp", 8, "/image/sunglasses/s2.webp", null },
                    { 17, "h1.webp", 9, "/image/sunglasses/h1.webp", null },
                    { 18, "h2.webp", 9, "/image/sunglasses/h2.webp", null },
                    { 19, "w1.webp", 10, "/image/sunglasses/w1.webp", null },
                    { 20, "w2.webp", 10, "/image/sunglasses/w2.webp", null },
                    { 21, "r1.webp", 11, "/image/sunglasses/r1.webp", null },
                    { 22, "r2.webp", 11, "/image/sunglasses/r2.webp", null },
                    { 23, "q1.webp", 12, "/image/sunglasses/q1.webp", null },
                    { 24, "q2.webp", 12, "/image/sunglasses/q2.webp", null }
                });

            migrationBuilder.InsertData(
                table: "Review",
                columns: new[] { "Id", "Comment", "ProductId", "Rate", "UserId" },
                values: new object[,]
                {
                    { 1, "hamamash dagh bood nooshaba dadan porteghal zadan dan dadan !", 1, 1.0, 1 },
                    { 2, "hamamash dagh bood nooshaba dadan porteghal zadan dan dadan !", 2, 2.0, 1 },
                    { 3, "hamamash dagh bood nooshaba dadan porteghal zadan dan dadan !", 5, 3.0, 1 },
                    { 4, "hamamash dagh bood nooshaba dadan porteghal zadan dan dadan !", 6, 2.0, 1 },
                    { 5, "hamamash dagh bood nooshaba dadan porteghal zadan dan dadan !", 2, 5.0, 2 },
                    { 6, "hamamash dagh bood nooshaba dadan porteghal zadan dan dadan !", 1, 3.0, 2 }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "RoleId", "UserId" },
                values: new object[] { 1, 1, 1 });

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
                column: "ProductId");

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
                name: "IX_Favorites_ProductId",
                table: "Favorites",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagePaths_ProductId",
                table: "ImagePaths",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagePaths_UserId",
                table: "ImagePaths",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_ProductId",
                table: "Review",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserId",
                table: "Review",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleId1",
                table: "Roles",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TypeId",
                table: "Transactions",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
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
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "ImagePaths");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "TransactionTypes");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
