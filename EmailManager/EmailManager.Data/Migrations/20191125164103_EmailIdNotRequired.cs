using Microsoft.EntityFrameworkCore.Migrations;

namespace EmailManager.Data.Migrations
{
    public partial class EmailIdNotRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanApplications_Emails_EmailId",
                table: "LoanApplications");

            migrationBuilder.DropIndex(
                name: "IX_LoanApplications_EmailId",
                table: "LoanApplications");

            migrationBuilder.AlterColumn<string>(
                name: "EmailId",
                table: "LoanApplications",
                nullable: true,
                oldClrType: typeof(string));

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

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplications_EmailId",
                table: "LoanApplications",
                column: "EmailId",
                unique: true,
                filter: "[EmailId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanApplications_Emails_EmailId",
                table: "LoanApplications",
                column: "EmailId",
                principalTable: "Emails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanApplications_Emails_EmailId",
                table: "LoanApplications");

            migrationBuilder.DropIndex(
                name: "IX_LoanApplications_EmailId",
                table: "LoanApplications");

            migrationBuilder.AlterColumn<string>(
                name: "EmailId",
                table: "LoanApplications",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "771f568e-a7d5-496b-90c4-72ff997368e6",
                column: "ConcurrencyStamp",
                value: "fcae7c83-e3ba-4c97-9e99-1bc303cf046c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93c66dd9-11c5-4836-b104-a9c333549530",
                column: "ConcurrencyStamp",
                value: "05889dd6-c11d-4e0f-a162-b23a6be7c242");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "565dfbc0-2681-4f29-bc97-a619eacf339c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8bc0e618-558a-4702-855b-729d96b8edd4", "AQAAAAEAACcQAAAAEHjV2SLjPDPn7bG8dEi4xTCThDTF1SF0x/NQUGIPiCKFU4IU068+xUXMYB9oN1syZA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fe86f129-41f3-4ab8-a61c-5f47239a1393",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d98b93de-ad5c-42f9-b49b-9bd6721ad86f", "AQAAAAEAACcQAAAAEFvX5yitf/Stim9hAq2k1WbEGEV/Z6cfDY9kzWi46XdMNEvBBnpMM7b+d3Mj8/ycHA==" });

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplications_EmailId",
                table: "LoanApplications",
                column: "EmailId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanApplications_Emails_EmailId",
                table: "LoanApplications",
                column: "EmailId",
                principalTable: "Emails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
