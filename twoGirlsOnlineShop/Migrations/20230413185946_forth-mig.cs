using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace twoGirlsOnlineShop.Migrations
{
    /// <inheritdoc />
    public partial class forthmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ImagePaths",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "ImagePaths",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "v1.webp");

            migrationBuilder.UpdateData(
                table: "ImagePaths",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "v2.webp");

            migrationBuilder.UpdateData(
                table: "ImagePaths",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "a1.webp");

            migrationBuilder.UpdateData(
                table: "ImagePaths",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "a2.webp");

            migrationBuilder.UpdateData(
                table: "ImagePaths",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "b1.webp");

            migrationBuilder.UpdateData(
                table: "ImagePaths",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "b2.webp");

            migrationBuilder.UpdateData(
                table: "ImagePaths",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "c1.webp");

            migrationBuilder.UpdateData(
                table: "ImagePaths",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "c2.webp");

            migrationBuilder.UpdateData(
                table: "ImagePaths",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "e1.webp");

            migrationBuilder.UpdateData(
                table: "ImagePaths",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "e2.webp");

            migrationBuilder.UpdateData(
                table: "ImagePaths",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "f1.webp");

            migrationBuilder.UpdateData(
                table: "ImagePaths",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "f2.webp");

            migrationBuilder.UpdateData(
                table: "ImagePaths",
                keyColumn: "Id",
                keyValue: 13,
                column: "Name",
                value: "g1.webp");

            migrationBuilder.UpdateData(
                table: "ImagePaths",
                keyColumn: "Id",
                keyValue: 14,
                column: "Name",
                value: "g2.webp");

            migrationBuilder.UpdateData(
                table: "ImagePaths",
                keyColumn: "Id",
                keyValue: 15,
                column: "Name",
                value: "s1.webp");

            migrationBuilder.UpdateData(
                table: "ImagePaths",
                keyColumn: "Id",
                keyValue: 16,
                column: "Name",
                value: "s2.webp");

            migrationBuilder.UpdateData(
                table: "ImagePaths",
                keyColumn: "Id",
                keyValue: 17,
                column: "Name",
                value: "h1.webp");

            migrationBuilder.UpdateData(
                table: "ImagePaths",
                keyColumn: "Id",
                keyValue: 18,
                column: "Name",
                value: "h2.webp");

            migrationBuilder.UpdateData(
                table: "ImagePaths",
                keyColumn: "Id",
                keyValue: 19,
                column: "Name",
                value: "w1.webp");

            migrationBuilder.UpdateData(
                table: "ImagePaths",
                keyColumn: "Id",
                keyValue: 20,
                column: "Name",
                value: "w2.webp");

            migrationBuilder.UpdateData(
                table: "ImagePaths",
                keyColumn: "Id",
                keyValue: 21,
                column: "Name",
                value: "r1.webp");

            migrationBuilder.UpdateData(
                table: "ImagePaths",
                keyColumn: "Id",
                keyValue: 22,
                column: "Name",
                value: "r2.webp");

            migrationBuilder.UpdateData(
                table: "ImagePaths",
                keyColumn: "Id",
                keyValue: 23,
                column: "Name",
                value: "q1.webp");

            migrationBuilder.UpdateData(
                table: "ImagePaths",
                keyColumn: "Id",
                keyValue: 24,
                column: "Name",
                value: "q2.webp");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1296m, new DateTime(2023, 2, 13, 20, 59, 46, 355, DateTimeKind.Local).AddTicks(6326), 1128m, 34, new DateTime(2023, 4, 13, 20, 59, 46, 355, DateTimeKind.Local).AddTicks(6276), 1036m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1307m, new DateTime(2023, 2, 13, 20, 59, 46, 355, DateTimeKind.Local).AddTicks(6335), 1409m, 39, new DateTime(2023, 3, 13, 20, 59, 46, 355, DateTimeKind.Local).AddTicks(6331), 1488m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1341m, new DateTime(2023, 3, 13, 20, 59, 46, 355, DateTimeKind.Local).AddTicks(6341), 1176m, 29, new DateTime(2023, 3, 13, 20, 59, 46, 355, DateTimeKind.Local).AddTicks(6338), 1416m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1440m, new DateTime(2023, 4, 13, 20, 59, 46, 355, DateTimeKind.Local).AddTicks(6348), 1214m, 37, new DateTime(2023, 3, 13, 20, 59, 46, 355, DateTimeKind.Local).AddTicks(6344), 1375m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1436m, new DateTime(2023, 2, 13, 20, 59, 46, 355, DateTimeKind.Local).AddTicks(6355), 1144m, 29, new DateTime(2023, 4, 13, 20, 59, 46, 355, DateTimeKind.Local).AddTicks(6352), 1342m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1451m, new DateTime(2023, 4, 13, 20, 59, 46, 355, DateTimeKind.Local).AddTicks(6362), 1044m, 42, new DateTime(2023, 2, 13, 20, 59, 46, 355, DateTimeKind.Local).AddTicks(6358), 1014m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1020m, new DateTime(2023, 3, 13, 20, 59, 46, 355, DateTimeKind.Local).AddTicks(6369), 1496m, 44, new DateTime(2023, 4, 13, 20, 59, 46, 355, DateTimeKind.Local).AddTicks(6365), 1090m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1459m, new DateTime(2023, 3, 13, 20, 59, 46, 355, DateTimeKind.Local).AddTicks(6376), 1438m, 46, new DateTime(2023, 3, 13, 20, 59, 46, 355, DateTimeKind.Local).AddTicks(6373), 1205m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1410m, new DateTime(2023, 2, 13, 20, 59, 46, 355, DateTimeKind.Local).AddTicks(6383), 1015m, 17, new DateTime(2023, 2, 13, 20, 59, 46, 355, DateTimeKind.Local).AddTicks(6380), 1259m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1495m, new DateTime(2023, 2, 13, 20, 59, 46, 355, DateTimeKind.Local).AddTicks(6390), 1444m, 50, new DateTime(2023, 2, 13, 20, 59, 46, 355, DateTimeKind.Local).AddTicks(6387), 1119m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1153m, new DateTime(2023, 4, 13, 20, 59, 46, 355, DateTimeKind.Local).AddTicks(6397), 1006m, 20, new DateTime(2023, 2, 13, 20, 59, 46, 355, DateTimeKind.Local).AddTicks(6393), 1171m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1152m, new DateTime(2023, 2, 13, 20, 59, 46, 355, DateTimeKind.Local).AddTicks(6403), 1120m, 12, new DateTime(2023, 4, 13, 20, 59, 46, 355, DateTimeKind.Local).AddTicks(6400), 1122m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ImagePaths");

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
                columns: new[] { "DiscountedPrice", "PurchaseDate", "PurchasePrice", "QuantityInStock", "ReleaseDate", "SalesPrice" },
                values: new object[] { 1256m, new DateTime(2023, 3, 8, 8, 8, 29, 750, DateTimeKind.Local).AddTicks(7614), 1041m, 16, new DateTime(2023, 4, 8, 8, 8, 29, 750, DateTimeKind.Local).AddTicks(7609), 1001m });

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
    }
}
