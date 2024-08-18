using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaWebsite.Entities.Migrations
{
    /// <inheritdoc />
    public partial class addRoleSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e56af770-1048-4ee9-a195-15c5912c1be4", null, "AppUser", "APPUSER" });

            migrationBuilder.UpdateData(
                table: "InteractionTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 8, 18, 11, 8, 23, 303, DateTimeKind.Utc).AddTicks(8284));

            migrationBuilder.UpdateData(
                table: "InteractionTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 8, 18, 11, 8, 23, 303, DateTimeKind.Utc).AddTicks(8286));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e56af770-1048-4ee9-a195-15c5912c1be4");

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
