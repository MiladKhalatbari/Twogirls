using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace twoGirlsOnlineShop.Migrations
{
    /// <inheritdoc />
    public partial class secoundmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CardItems_ProductId",
                table: "CardItems");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1227m, new DateTime(2023, 2, 8, 7, 31, 33, 851, DateTimeKind.Local).AddTicks(2454), 1001m, 35, new DateTime(2023, 3, 8, 7, 31, 33, 851, DateTimeKind.Local).AddTicks(2402), 1083m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1408m, new DateTime(2023, 3, 8, 7, 31, 33, 851, DateTimeKind.Local).AddTicks(2462), 1269m, 50, new DateTime(2023, 2, 8, 7, 31, 33, 851, DateTimeKind.Local).AddTicks(2458), 1238m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1402m, new DateTime(2023, 3, 8, 7, 31, 33, 851, DateTimeKind.Local).AddTicks(2469), 1392m, 17, new DateTime(2023, 2, 8, 7, 31, 33, 851, DateTimeKind.Local).AddTicks(2466), 1300m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1428m, new DateTime(2023, 4, 8, 7, 31, 33, 851, DateTimeKind.Local).AddTicks(2476), 1480m, 44, new DateTime(2023, 4, 8, 7, 31, 33, 851, DateTimeKind.Local).AddTicks(2473), 1087m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1078m, new DateTime(2023, 3, 8, 7, 31, 33, 851, DateTimeKind.Local).AddTicks(2484), 1332m, 36, new DateTime(2023, 4, 8, 7, 31, 33, 851, DateTimeKind.Local).AddTicks(2479), 1309m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1244m, new DateTime(2023, 3, 8, 7, 31, 33, 851, DateTimeKind.Local).AddTicks(2490), 1113m, 41, new DateTime(2023, 3, 8, 7, 31, 33, 851, DateTimeKind.Local).AddTicks(2487), 1136m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1251m, new DateTime(2023, 2, 8, 7, 31, 33, 851, DateTimeKind.Local).AddTicks(2501), 1294m, 31, new DateTime(2023, 3, 8, 7, 31, 33, 851, DateTimeKind.Local).AddTicks(2496), 1289m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1011m, new DateTime(2023, 2, 8, 7, 31, 33, 851, DateTimeKind.Local).AddTicks(2509), 1215m, 22, new DateTime(2023, 2, 8, 7, 31, 33, 851, DateTimeKind.Local).AddTicks(2505), 1003m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1238m, new DateTime(2023, 2, 8, 7, 31, 33, 851, DateTimeKind.Local).AddTicks(2518), 1105m, 33, new DateTime(2023, 3, 8, 7, 31, 33, 851, DateTimeKind.Local).AddTicks(2513), 1020m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1256m, new DateTime(2023, 3, 8, 7, 31, 33, 851, DateTimeKind.Local).AddTicks(2525), 1456m, 23, new DateTime(2023, 3, 8, 7, 31, 33, 851, DateTimeKind.Local).AddTicks(2522), 1224m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1031m, new DateTime(2023, 2, 8, 7, 31, 33, 851, DateTimeKind.Local).AddTicks(2534), 1226m, 43, new DateTime(2023, 4, 8, 7, 31, 33, 851, DateTimeKind.Local).AddTicks(2530), 1007m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1314m, new DateTime(2023, 3, 8, 7, 31, 33, 851, DateTimeKind.Local).AddTicks(2540), 1472m, 21, new DateTime(2023, 2, 8, 7, 31, 33, 851, DateTimeKind.Local).AddTicks(2537), 1008m });

            migrationBuilder.CreateIndex(
                name: "IX_CardItems_ProductId",
                table: "CardItems",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CardItems_ProductId",
                table: "CardItems");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1255m, new DateTime(2023, 3, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7814), 1489m, 41, new DateTime(2023, 3, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7769), 1391m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1058m, new DateTime(2023, 3, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7824), 1014m, 48, new DateTime(2023, 2, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7820), 1379m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1191m, new DateTime(2023, 4, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7833), 1227m, 32, new DateTime(2023, 2, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7829), 1329m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1167m, new DateTime(2023, 3, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7842), 1451m, 42, new DateTime(2023, 4, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7838), 1094m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1406m, new DateTime(2023, 2, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7851), 1446m, 32, new DateTime(2023, 4, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7846), 1143m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1403m, new DateTime(2023, 4, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7859), 1417m, 28, new DateTime(2023, 3, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7855), 1017m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1184m, new DateTime(2023, 2, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7868), 1182m, 41, new DateTime(2023, 3, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7863), 1408m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1335m, new DateTime(2023, 3, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7880), 1134m, 41, new DateTime(2023, 2, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7872), 1271m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1293m, new DateTime(2023, 3, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7888), 1182m, 18, new DateTime(2023, 2, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7885), 1371m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1276m, new DateTime(2023, 2, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7895), 1087m, 24, new DateTime(2023, 3, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7891), 1313m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1145m, new DateTime(2023, 3, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7901), 1317m, 32, new DateTime(2023, 3, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7898), 1084m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1439m, new DateTime(2023, 3, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7908), 1063m, 42, new DateTime(2023, 4, 8, 1, 35, 52, 261, DateTimeKind.Local).AddTicks(7904), 1386m });

            migrationBuilder.CreateIndex(
                name: "IX_CardItems_ProductId",
                table: "CardItems",
                column: "ProductId",
                unique: true);
        }
    }
}
