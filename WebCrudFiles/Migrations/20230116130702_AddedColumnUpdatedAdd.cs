using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCrudFiles.Migrations
{
    public partial class AddedColumnUpdatedAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAd",
                table: "Files",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateAd",
                table: "Files");
        }
    }
}
