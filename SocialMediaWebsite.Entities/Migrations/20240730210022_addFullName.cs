using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaWebsite.Entities.Migrations
{
    /// <inheritdoc />
    public partial class addFullName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "InteractionTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 7, 30, 21, 0, 21, 919, DateTimeKind.Utc).AddTicks(3046));

            migrationBuilder.UpdateData(
                table: "InteractionTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 7, 30, 21, 0, 21, 919, DateTimeKind.Utc).AddTicks(3048));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "InteractionTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 7, 19, 13, 56, 24, 491, DateTimeKind.Utc).AddTicks(1136));

            migrationBuilder.UpdateData(
                table: "InteractionTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 7, 19, 13, 56, 24, 491, DateTimeKind.Utc).AddTicks(1138));
        }
    }
}
