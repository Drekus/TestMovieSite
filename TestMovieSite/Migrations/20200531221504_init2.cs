using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestMovieSite.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "DownloaderId", "PublishingDate", "Title" },
                values: new object[] { 1, "Movie №0 description", "Movie №0 director", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie №0 title" });
        }
    }
}
