using Microsoft.EntityFrameworkCore.Migrations;

namespace EmailManager.Data.Migrations
{
    public partial class AddedNewPropertyToSaveTheEmailId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChangedCloseToNewStatusForEmailId",
                table: "LoanApplications",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangedCloseToNewStatusForEmailId",
                table: "LoanApplications");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "771f568e-a7d5-496b-90c4-72ff997368e6",
                column: "ConcurrencyStamp",
                value: "f40acf09-a1ea-4545-8b5a-bc973ec2f033");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93c66dd9-11c5-4836-b104-a9c333549530",
                column: "ConcurrencyStamp",
                value: "d409caf3-7952-4eb2-8800-eee8798f64c5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "565dfbc0-2681-4f29-bc97-a619eacf339c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d270894e-2c7d-4ab9-9c2b-1d164a9bc7fc", "AQAAAAEAACcQAAAAEAfdrwyslNrSscOEZdqX4+qYD+QQLxmBFzw0EvYCXfc13ukXPyeowqPXMDFIHUzQ+A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fe86f129-41f3-4ab8-a61c-5f47239a1393",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "93edd13c-e532-4739-a537-c8a43b05251a", "AQAAAAEAACcQAAAAEE6y3y0ql3fBeMoFdJNtJWfGlkOuAhIzLPyQ0X+bQbi8DFpaWdUXMHTfAW40d9A+jw==" });
        }
    }
}
