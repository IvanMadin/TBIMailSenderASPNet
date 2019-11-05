using Microsoft.EntityFrameworkCore.Migrations;

namespace EmailManager.Data.Migrations
{
    public partial class AddedStatsuSeeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "771f568e-a7d5-496b-90c4-72ff997368e6",
                column: "ConcurrencyStamp",
                value: "2036dbdc-5f7d-4f65-b5c3-e55d7f65abeb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93c66dd9-11c5-4836-b104-a9c333549530",
                column: "ConcurrencyStamp",
                value: "ed6b193e-5839-4aa8-9d60-048912a0d40f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "565dfbc0-2681-4f29-bc97-a619eacf339c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4a877f8b-b9fb-4786-aab7-1b3a6c080bd1", "AQAAAAEAACcQAAAAEMVxINJVlkyq2wvrRXWd1rSf+2aJ790Vr3kp+/Xl9krB/UVr8rEbSg0r9SiuntFz5A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fe86f129-41f3-4ab8-a61c-5f47239a1393",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1cb06ee4-1176-4dcf-b813-db4830dd8984", "AQAAAAEAACcQAAAAEIK5ywStGlzRJmtJCxZ2IhwXU4NrFJN1La5pwIhpQAqfGscO7NLCKfn/5r5Cv+z5Kw==" });

            migrationBuilder.InsertData(
                table: "StatusApplications",
                columns: new[] { "Id", "StatusType" },
                values: new object[,]
                {
                    { "61cb6584-591b-4560-bc4a-a89950b15cc3", "New" },
                    { "645ad030-3b7f-47fb-93e1-1c9315b34673", "Open" },
                    { "6c60cb0a-5395-49b1-abfd-40a4db7a355a", "Closed" }
                });

            migrationBuilder.InsertData(
                table: "StatusEmails",
                columns: new[] { "Id", "StatusType" },
                values: new object[,]
                {
                    { "a0e53404-d40e-4a1e-8fe5-9a5fc0139ed9", "Not Reviewed" },
                    { "165e4e23-7fed-4bd6-a859-530026625ffc", "Invalid Application" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StatusApplications",
                keyColumn: "Id",
                keyValue: "61cb6584-591b-4560-bc4a-a89950b15cc3");

            migrationBuilder.DeleteData(
                table: "StatusApplications",
                keyColumn: "Id",
                keyValue: "645ad030-3b7f-47fb-93e1-1c9315b34673");

            migrationBuilder.DeleteData(
                table: "StatusApplications",
                keyColumn: "Id",
                keyValue: "6c60cb0a-5395-49b1-abfd-40a4db7a355a");

            migrationBuilder.DeleteData(
                table: "StatusEmails",
                keyColumn: "Id",
                keyValue: "165e4e23-7fed-4bd6-a859-530026625ffc");

            migrationBuilder.DeleteData(
                table: "StatusEmails",
                keyColumn: "Id",
                keyValue: "a0e53404-d40e-4a1e-8fe5-9a5fc0139ed9");

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
    }
}
