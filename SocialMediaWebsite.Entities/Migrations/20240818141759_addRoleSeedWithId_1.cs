using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaWebsite.Entities.Migrations
{
    /// <inheritdoc />
    public partial class addRoleSeedWithId_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1", null, "AppUser", "APPUSER" });

            migrationBuilder.UpdateData(
                table: "InteractionTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 8, 18, 14, 17, 59, 8, DateTimeKind.Utc).AddTicks(3754));

            migrationBuilder.UpdateData(
                table: "InteractionTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 8, 18, 14, 17, 59, 8, DateTimeKind.Utc).AddTicks(3756));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

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
    }
}
