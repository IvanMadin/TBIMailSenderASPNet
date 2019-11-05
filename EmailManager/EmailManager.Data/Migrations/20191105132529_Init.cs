using Microsoft.EntityFrameworkCore.Migrations;

namespace EmailManager.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "771f568e-a7d5-496b-90c4-72ff997368e6",
                column: "ConcurrencyStamp",
                value: "dc6aa649-7169-42e0-ab75-70d013f5799e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93c66dd9-11c5-4836-b104-a9c333549530",
                column: "ConcurrencyStamp",
                value: "9451e005-7595-458e-8114-bc3ed2445747");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "565dfbc0-2681-4f29-bc97-a619eacf339c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fade8e17-668d-4b96-aba9-8c32843e6f23", "AQAAAAEAACcQAAAAEInKuocEP0xAqOw+opL4fvf3cFfYEIEFA31atIIP1l3K4QbWpRXcgmTb7wK2gkm8GQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fe86f129-41f3-4ab8-a61c-5f47239a1393",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5d19c232-c39e-49cc-a58b-3fdefdc198c4", "AQAAAAEAACcQAAAAEHpLinKcqbrE9T5xq9tv8i4z6F0bZ13C3aSt6qZL5RevwtYnTJGgfILKcLKQXjrERQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "771f568e-a7d5-496b-90c4-72ff997368e6",
                column: "ConcurrencyStamp",
                value: "d779cec9-9942-40a2-8b3a-22dde872baa1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93c66dd9-11c5-4836-b104-a9c333549530",
                column: "ConcurrencyStamp",
                value: "70c17d4f-fa69-4da8-a9b3-7265ca0151ed");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "565dfbc0-2681-4f29-bc97-a619eacf339c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "155b8817-d9da-4198-addb-8010a3ecd97e", "AQAAAAEAACcQAAAAEHH/mA05gIiLf8ZfPJk+M6Y/hZ1Ak83eLge/rYi+YjacEz8+5oUIMKn5TXO79tJtqw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fe86f129-41f3-4ab8-a61c-5f47239a1393",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "102c59a7-8943-4c2c-bbdf-4e0ba24e95ba", "AQAAAAEAACcQAAAAEIn2wSsOFaBjiwxKHi9og6kySWoWqcNwXEOwsC01jih6KENPIgR/GS7aYwXC26Yb3g==" });
        }
    }
}
