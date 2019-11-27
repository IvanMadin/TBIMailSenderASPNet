using Microsoft.EntityFrameworkCore.Migrations;

namespace EmailManager.Data.Migrations
{
    public partial class AddedIsLocked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Emails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "771f568e-a7d5-496b-90c4-72ff997368e6",
                column: "ConcurrencyStamp",
                value: "1e742652-526f-4b28-9497-07b86d8ea041");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93c66dd9-11c5-4836-b104-a9c333549530",
                column: "ConcurrencyStamp",
                value: "4edf0f0d-940f-4119-9181-3239d4fe578d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "565dfbc0-2681-4f29-bc97-a619eacf339c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d2ab59f8-6375-4314-a679-46ea27fbc851", "AQAAAAEAACcQAAAAEIF6RFcFHfptvJaiQ2giYc5PEorDFvx7cNit+nG/gUFOsD+sa58D4JN4Sqx9yvKM8Q==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fe86f129-41f3-4ab8-a61c-5f47239a1393",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "533fa9a4-4287-4046-bdcb-4eee5ad9234d", "AQAAAAEAACcQAAAAEPuI9rMtkBJw5qixHAwYvLwqE21ClxwmWt+OJ2k2vRTB9jfcybdNtPgHPF6WFnbvkA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Emails");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "771f568e-a7d5-496b-90c4-72ff997368e6",
                column: "ConcurrencyStamp",
                value: "cfa5727d-a365-455a-b486-cd50e5b32a5f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93c66dd9-11c5-4836-b104-a9c333549530",
                column: "ConcurrencyStamp",
                value: "df0a6900-7a7c-4eaf-8d9e-6e8207810937");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "565dfbc0-2681-4f29-bc97-a619eacf339c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3435e608-00f6-4940-8ee3-6108da462228", "AQAAAAEAACcQAAAAEJ4vQc4uYZxWu8sHL/wFYENTOzu9xlmPHB7Hk2ofqVh7VTAoVHM6VDG7JflK18wpmw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fe86f129-41f3-4ab8-a61c-5f47239a1393",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e7133e5b-df25-4e41-b71c-1f4dc258bc88", "AQAAAAEAACcQAAAAEMzyrSqwNNQpzENjeQppJe8xADgje0msImG1jKCJdfAIfRcNZ7N2mvR5rJQtBsNc7w==" });
        }
    }
}
