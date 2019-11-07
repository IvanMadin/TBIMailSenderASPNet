using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmailManager.Data.Migrations
{
    public partial class checking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sender",
                table: "Emails",
                newName: "SenderName");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "LoanApplications",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnDate",
                table: "LoanApplications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedByUserId",
                table: "LoanApplications",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnDate",
                table: "LoanApplications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByUserId",
                table: "LoanApplications",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOnDate",
                table: "LoanApplications",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "Emails",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnDate",
                table: "Emails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedByUserId",
                table: "Emails",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnDate",
                table: "Emails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByUserId",
                table: "Emails",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOnDate",
                table: "Emails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderEmail",
                table: "Emails",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "ClientDatas",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnDate",
                table: "ClientDatas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedByUserId",
                table: "ClientDatas",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnDate",
                table: "ClientDatas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByUserId",
                table: "ClientDatas",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOnDate",
                table: "ClientDatas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnDate",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedByUserId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnDate",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByUserId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOnDate",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "771f568e-a7d5-496b-90c4-72ff997368e6",
                column: "ConcurrencyStamp",
                value: "761266f1-df99-43c4-b979-df8118d82f86");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93c66dd9-11c5-4836-b104-a9c333549530",
                column: "ConcurrencyStamp",
                value: "d4c872ce-76a7-4008-94d3-7ee23750642a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "565dfbc0-2681-4f29-bc97-a619eacf339c",
                columns: new[] { "ConcurrencyStamp", "CreatedOnDate", "PasswordHash" },
                values: new object[] { "ce22a611-355a-4e05-bb92-6d36d445c491", new DateTime(2019, 11, 7, 20, 16, 6, 286, DateTimeKind.Local).AddTicks(4746), "AQAAAAEAACcQAAAAENQiIXD2QQIyWpcWgGIoxlDYghfLJ/xlI6n1CBph0H7smEUz6WArG1Z2EZggzIS5qA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fe86f129-41f3-4ab8-a61c-5f47239a1393",
                columns: new[] { "ConcurrencyStamp", "CreatedOnDate", "PasswordHash" },
                values: new object[] { "9185c213-3e16-4ea1-b60d-a637653c8aa2", new DateTime(2019, 11, 7, 20, 16, 6, 275, DateTimeKind.Local).AddTicks(4278), "AQAAAAEAACcQAAAAEK82jt+dt1L6bR+OC6wSwCUUb8efrWAYI7AnA/wPrkCLDahphjk78lDhE6T5VX6L2A==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "LoanApplications");

            migrationBuilder.DropColumn(
                name: "CreatedOnDate",
                table: "LoanApplications");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "LoanApplications");

            migrationBuilder.DropColumn(
                name: "DeletedOnDate",
                table: "LoanApplications");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "LoanApplications");

            migrationBuilder.DropColumn(
                name: "ModifiedOnDate",
                table: "LoanApplications");

            migrationBuilder.DropColumn(
                name: "CreatedOnDate",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "DeletedOnDate",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "ModifiedOnDate",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "SenderEmail",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "ClientDatas");

            migrationBuilder.DropColumn(
                name: "CreatedOnDate",
                table: "ClientDatas");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "ClientDatas");

            migrationBuilder.DropColumn(
                name: "DeletedOnDate",
                table: "ClientDatas");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "ClientDatas");

            migrationBuilder.DropColumn(
                name: "ModifiedOnDate",
                table: "ClientDatas");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatedOnDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DeletedOnDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ModifiedOnDate",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "SenderName",
                table: "Emails",
                newName: "Sender");

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "Emails",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

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
        }
    }
}
