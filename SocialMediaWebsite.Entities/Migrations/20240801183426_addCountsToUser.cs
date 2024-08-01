using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaWebsite.Entities.Migrations
{
    /// <inheritdoc />
    public partial class addCountsToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<uint>(
                name: "FollowerCount",
                table: "AspNetUsers",
                type: "int unsigned",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "FollowingCount",
                table: "AspNetUsers",
                type: "int unsigned",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "PostCount",
                table: "AspNetUsers",
                type: "int unsigned",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.UpdateData(
                table: "InteractionTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 8, 1, 18, 34, 25, 812, DateTimeKind.Utc).AddTicks(9924));

            migrationBuilder.UpdateData(
                table: "InteractionTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 8, 1, 18, 34, 25, 812, DateTimeKind.Utc).AddTicks(9926));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FollowerCount",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FollowingCount",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PostCount",
                table: "AspNetUsers");

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
    }
}
