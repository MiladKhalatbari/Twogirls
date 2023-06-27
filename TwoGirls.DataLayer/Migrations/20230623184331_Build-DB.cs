using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable


namespace TwoGirls.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class BuildDB : Migration
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
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiscountCodes",
                columns: table => new
                {
                    DiscountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UseableCount = table.Column<int>(type: "int", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DiscountPercent = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EendDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountCodes", x => x.DiscountId);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_Permissions_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Permissions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "productTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
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
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemNumber = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalesPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountedPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantityInStock = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_productTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "productTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
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
                    ActiveCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
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
                name: "Carts",
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
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
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
                    ProductId = table.Column<int>(type: "int", nullable: true),
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
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
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
                name: "UserDiscountCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DiscountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDiscountCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDiscountCodes_DiscountCodes_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "DiscountCodes",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDiscountCodes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsPosted = table.Column<bool>(type: "bit", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    OrderPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PostDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DiscountPercent = table.Column<int>(type: "int", nullable: false),
                    IsFinally = table.Column<bool>(type: "bit", nullable: false),
                    CartId1 = table.Column<int>(type: "int", nullable: true),
                    UserId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Carts_CartId1",
                        column: x => x.CartId1,
                        principalTable: "Carts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "IsDelete", "Name", "ParentId" },
                values: new object[,]
                {
                    { 1, null, false, "Men's", null },
                    { 2, null, false, "Women's", null },
                    { 3, null, false, "Featured Categories", null },
                    { 4, null, false, "New Collection", 1 },
                    { 5, null, false, "Most Visited", 1 },
                    { 6, null, false, "Best Sellers", 1 },
                    { 7, null, false, "Discounted", 1 },
                    { 8, null, false, "New Collection", 2 },
                    { 9, null, false, "Most Visited", 2 },
                    { 10, null, false, "Best Sellers", 2 },
                    { 11, null, false, "Discounted", 2 },
                    { 12, null, false, "Fashion and Trends", 3 },
                    { 13, null, false, "Designer Outlet", 3 },
                    { 14, null, false, "Sports Glasses", 3 },
                    { 15, null, false, "Glasses for Couples", 3 }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "ParentId", "Title" },
                values: new object[] { 1, null, "Admin Panel" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "IsDelete", "RoleTitle" },
                values: new object[,]
                {
                    { 1, false, "Owner" },
                    { 2, false, "Admin" },
                    { 3, false, "Staff" },
                    { 4, false, "User" }
                });

            migrationBuilder.InsertData(
                table: "TransactionTypes",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Deposit" },
                    { 2, "Withdraw" }
                });

            migrationBuilder.InsertData(
                table: "productTypes",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Sunglasses" },
                    { 2, "Eyeglasses" }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "ParentId", "Title" },
                values: new object[,]
                {
                    { 2, 1, "Manage Users" },
                    { 3, 1, "Manage Permissions" },
                    { 4, 1, "Manage Products" },
                    { 5, 1, "Manage Orders" },
                    { 6, 1, "Manage Discounts" },
                    { 7, 1, "Manage Categories" },
                    { 8, 1, "Manage Transactions" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "DiscountedPrice", "IsDelete", "ItemNumber", "ProductTypeId", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice", "Title" },
                values: new object[,]
                {
                    { 1, "", 1324m, false, 43555, 1, new DateTime(2023, 4, 23, 20, 43, 30, 376, DateTimeKind.Local).AddTicks(9779), 1082m, 36, new DateTime(2023, 5, 23, 20, 43, 30, 376, DateTimeKind.Local).AddTicks(9742), 1405m, "Reyban Genwux 941" },
                    { 2, "", 1328m, false, 43481, 1, new DateTime(2023, 5, 23, 20, 43, 30, 376, DateTimeKind.Local).AddTicks(9812), 1077m, 41, new DateTime(2023, 4, 23, 20, 43, 30, 376, DateTimeKind.Local).AddTicks(9800), 1308m, "Reyban Genwux 941" },
                    { 3, "", 1287m, false, 51079, 1, new DateTime(2023, 6, 23, 20, 43, 30, 376, DateTimeKind.Local).AddTicks(9841), 1356m, 12, new DateTime(2023, 6, 23, 20, 43, 30, 376, DateTimeKind.Local).AddTicks(9829), 1077m, "Reyban Genwux 941" },
                    { 4, "", 1056m, false, 50868, 1, new DateTime(2023, 4, 23, 20, 43, 30, 376, DateTimeKind.Local).AddTicks(9875), 1364m, 49, new DateTime(2023, 5, 23, 20, 43, 30, 376, DateTimeKind.Local).AddTicks(9861), 1260m, "Reyban Genwux 941" },
                    { 5, "", 1059m, false, 84937, 1, new DateTime(2023, 4, 23, 20, 43, 30, 376, DateTimeKind.Local).AddTicks(9902), 1405m, 40, new DateTime(2023, 4, 23, 20, 43, 30, 376, DateTimeKind.Local).AddTicks(9890), 1236m, "Reyban Genwux 941" },
                    { 6, "", 1170m, false, 39315, 1, new DateTime(2023, 6, 23, 20, 43, 30, 376, DateTimeKind.Local).AddTicks(9934), 1388m, 48, new DateTime(2023, 5, 23, 20, 43, 30, 376, DateTimeKind.Local).AddTicks(9918), 1018m, "Reyban Genwux 941" },
                    { 7, "", 1016m, false, 85211, 1, new DateTime(2023, 6, 23, 20, 43, 30, 376, DateTimeKind.Local).AddTicks(9961), 1265m, 20, new DateTime(2023, 6, 23, 20, 43, 30, 376, DateTimeKind.Local).AddTicks(9947), 1234m, "Reyban Genwux 941" },
                    { 8, "", 1280m, false, 71769, 1, new DateTime(2023, 6, 23, 20, 43, 30, 376, DateTimeKind.Local).AddTicks(9990), 1315m, 42, new DateTime(2023, 4, 23, 20, 43, 30, 376, DateTimeKind.Local).AddTicks(9974), 1305m, "Reyban Genwux 941" },
                    { 9, "", 1157m, false, 50838, 1, new DateTime(2023, 4, 23, 20, 43, 30, 377, DateTimeKind.Local).AddTicks(16), 1149m, 26, new DateTime(2023, 5, 23, 20, 43, 30, 377, DateTimeKind.Local).AddTicks(5), 1201m, "Reyban Genwux 941" },
                    { 10, "", 1291m, false, 77878, 1, new DateTime(2023, 5, 23, 20, 43, 30, 377, DateTimeKind.Local).AddTicks(41), 1153m, 31, new DateTime(2023, 5, 23, 20, 43, 30, 377, DateTimeKind.Local).AddTicks(31), 1218m, "Reyban Genwux 941" },
                    { 11, "", 1138m, false, 88003, 1, new DateTime(2023, 4, 23, 20, 43, 30, 377, DateTimeKind.Local).AddTicks(70), 1475m, 27, new DateTime(2023, 5, 23, 20, 43, 30, 377, DateTimeKind.Local).AddTicks(56), 1099m, "Reyban Genwux 941" },
                    { 12, "", 1343m, false, 40370, 1, new DateTime(2023, 4, 23, 20, 43, 30, 377, DateTimeKind.Local).AddTicks(96), 1348m, 48, new DateTime(2023, 5, 23, 20, 43, 30, 377, DateTimeKind.Local).AddTicks(83), 1343m, "Reyban Genwux 941" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ActiveCode", "Email", "FirstName", "ImagePath", "IsActive", "IsDelete", "LastName", "Password", "PhoneNumber", "RegisterDate", "RoleId" },
                values: new object[,]
                {
                    { 1, "50975f6e0555441db20afedb3fd1e02f", "aqamiladam@gmail.com", "Milad", "/image/user-avatar/Milad_profile.jpg", true, false, "Khalatbari", "$HASH$V1$jhUAa6x2XLTQSttEBvsxYw==$Ib1JCvml1e9BLlaqTxYe+ugz3qHe/aig6ks9sNge0lk=", "+420730834642", new DateTime(2023, 6, 23, 20, 43, 30, 376, DateTimeKind.Local).AddTicks(9496), 1 },
                    { 2, "90e2b14b48cc40deafcd275d96c50b94", "samanafra@gmail.com", "saman", "/image/user-avatar/saman_profile.jpg", true, false, "afrasiabi", "$HASH$V1$jhUAa6x2XLTQSttEBvsxYw==$Ib1JCvml1e9BLlaqTxYe+ugz3qHe/aig6ks9sNge0lk=", "+0985858585", new DateTime(2023, 6, 23, 20, 43, 30, 376, DateTimeKind.Local).AddTicks(9643), 1 }
                });

            migrationBuilder.InsertData(
                table: "Carts",
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
                table: "Permissions",
                columns: new[] { "Id", "ParentId", "Title" },
                values: new object[,]
                {
                    { 9, 2, "Add or Edit" },
                    { 10, 2, "Delete" },
                    { 11, 2, "Recover" },
                    { 12, 2, "Watch Deleted list" },
                    { 13, 3, "Add or Edit" },
                    { 14, 3, "Delete" },
                    { 15, 3, "Recover" },
                    { 16, 3, "Watch Deleted list" },
                    { 17, 4, "Add or Edit" },
                    { 18, 4, "Delete" },
                    { 19, 4, "Recover" },
                    { 20, 4, "Watch Deleted list" },
                    { 21, 5, "Post" },
                    { 22, 5, "Unpost" },
                    { 23, 5, "Watch Posted list" },
                    { 24, 6, "Add or Edit" },
                    { 25, 6, "Delete" },
                    { 26, 6, "Recover" },
                    { 27, 6, "Watch Deleted list" },
                    { 28, 7, "Add or Edit" },
                    { 29, 7, "Delete" },
                    { 30, 7, "Recover" },
                    { 31, 7, "Watch Deleted list" },
                    { 32, 8, "Add or Edit" },
                    { 33, 8, "Delete" },
                    { 34, 8, "Recover" },
                    { 35, 8, "Watch Deleted list" }
                });

            migrationBuilder.InsertData(
                table: "Review",
                columns: new[] { "Id", "Comment", "Date", "ProductId", "Rate", "UserId" },
                values: new object[,]
                {
                    { 1, "hamamash dagh bood nooshaba dadan porteghal zadan dan dadan !", new DateTime(2023, 5, 28, 20, 43, 30, 377, DateTimeKind.Local).AddTicks(517), 1, 1.0, 1 },
                    { 2, "hamamash dagh bood nooshaba dadan porteghal zadan dan dadan !", new DateTime(2023, 6, 14, 20, 43, 30, 377, DateTimeKind.Local).AddTicks(538), 2, 2.0, 1 },
                    { 3, "hamamash dagh bood nooshaba dadan porteghal zadan dan dadan !", new DateTime(2023, 5, 4, 20, 43, 30, 377, DateTimeKind.Local).AddTicks(550), 5, 3.0, 1 },
                    { 4, "hamamash dagh bood nooshaba dadan porteghal zadan dan dadan !", new DateTime(2023, 6, 3, 20, 43, 30, 377, DateTimeKind.Local).AddTicks(561), 6, 2.0, 1 },
                    { 5, "hamamash dagh bood nooshaba dadan porteghal zadan dan dadan !", new DateTime(2023, 6, 18, 20, 43, 30, 377, DateTimeKind.Local).AddTicks(573), 2, 5.0, 2 },
                    { 6, "hamamash dagh bood nooshaba dadan porteghal zadan dan dadan !", new DateTime(2023, 5, 31, 20, 43, 30, 377, DateTimeKind.Local).AddTicks(583), 1, 3.0, 2 }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 2, 2, 1 },
                    { 4, 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "CartItems",
                columns: new[] { "Id", "CartId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 1, 2, 2 },
                    { 3, 1, 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 3, 9, 1 },
                    { 5, 13, 1 },
                    { 6, 14, 1 },
                    { 7, 15, 1 },
                    { 8, 16, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
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
                name: "IX_Orders_AddressId",
                table: "Orders",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CartId",
                table: "Orders",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CartId1",
                table: "Orders",
                column: "CartId1",
                unique: true,
                filter: "[CartId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId1",
                table: "Orders",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ParentId",
                table: "Permissions",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_ProductId",
                table: "Review",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserId",
                table: "Review",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TypeId",
                table: "Transactions",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDiscountCodes_DiscountId",
                table: "UserDiscountCodes",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDiscountCodes_UserId",
                table: "UserDiscountCodes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "CategoryToProducts");

            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "ImagePaths");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "UserDiscountCodes");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "TransactionTypes");

            migrationBuilder.DropTable(
                name: "DiscountCodes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "productTypes");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
