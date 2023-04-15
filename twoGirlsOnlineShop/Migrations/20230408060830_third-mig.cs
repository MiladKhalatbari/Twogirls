using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace twoGirlsOnlineShop.Migrations
{
    /// <inheritdoc />
    public partial class thirdmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Cards",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1001m, new DateTime(2023, 3, 8, 8, 8, 29, 750, DateTimeKind.Local).AddTicks(7551), 1056m, 27, new DateTime(2023, 4, 8, 8, 8, 29, 750, DateTimeKind.Local).AddTicks(7506), 1176m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1083m, new DateTime(2023, 2, 8, 8, 8, 29, 750, DateTimeKind.Local).AddTicks(7560), 1018m, 31, new DateTime(2023, 3, 8, 8, 8, 29, 750, DateTimeKind.Local).AddTicks(7556), 1081m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1335m, new DateTime(2023, 3, 8, 8, 8, 29, 750, DateTimeKind.Local).AddTicks(7566), 1054m, 48, new DateTime(2023, 4, 8, 8, 8, 29, 750, DateTimeKind.Local).AddTicks(7563), 1311m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1017m, new DateTime(2023, 3, 8, 8, 8, 29, 750, DateTimeKind.Local).AddTicks(7572), 1168m, 43, new DateTime(2023, 4, 8, 8, 8, 29, 750, DateTimeKind.Local).AddTicks(7569), 1047m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1167m, new DateTime(2023, 2, 8, 8, 8, 29, 750, DateTimeKind.Local).AddTicks(7580), 1123m, 30, new DateTime(2023, 2, 8, 8, 8, 29, 750, DateTimeKind.Local).AddTicks(7577), 1069m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1022m, new DateTime(2023, 3, 8, 8, 8, 29, 750, DateTimeKind.Local).AddTicks(7587), 1207m, 29, new DateTime(2023, 4, 8, 8, 8, 29, 750, DateTimeKind.Local).AddTicks(7584), 1254m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1114m, new DateTime(2023, 4, 8, 8, 8, 29, 750, DateTimeKind.Local).AddTicks(7594), 1439m, 26, new DateTime(2023, 3, 8, 8, 8, 29, 750, DateTimeKind.Local).AddTicks(7590), 1200m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1225m, new DateTime(2023, 4, 8, 8, 8, 29, 750, DateTimeKind.Local).AddTicks(7600), 1264m, 31, new DateTime(2023, 2, 8, 8, 8, 29, 750, DateTimeKind.Local).AddTicks(7597), 1090m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1038m, new DateTime(2023, 4, 8, 8, 8, 29, 750, DateTimeKind.Local).AddTicks(7606), 1453m, 39, new DateTime(2023, 4, 8, 8, 8, 29, 750, DateTimeKind.Local).AddTicks(7603), 1086m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { new DateTime(2023, 3, 8, 8, 8, 29, 750, DateTimeKind.Local).AddTicks(7614), 1041m, 16, new DateTime(2023, 4, 8, 8, 8, 29, 750, DateTimeKind.Local).AddTicks(7609), 1001m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1009m, new DateTime(2023, 2, 8, 8, 8, 29, 750, DateTimeKind.Local).AddTicks(7621), 1390m, 41, new DateTime(2023, 4, 8, 8, 8, 29, 750, DateTimeKind.Local).AddTicks(7617), 1068m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1440m, new DateTime(2023, 3, 8, 8, 8, 29, 750, DateTimeKind.Local).AddTicks(7627), 1383m, 14, new DateTime(2023, 3, 8, 8, 8, 29, 750, DateTimeKind.Local).AddTicks(7624), 1446m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Cards");

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
                columns: new[] { "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { new DateTime(2023, 3, 8, 7, 31, 33, 851, DateTimeKind.Local).AddTicks(2525), 1456m, 23, new DateTime(2023, 3, 8, 7, 31, 33, 851, DateTimeKind.Local).AddTicks(2522), 1224m });

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
        }
    }
}
