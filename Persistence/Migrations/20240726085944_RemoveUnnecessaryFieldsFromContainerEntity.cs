using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnnecessaryFieldsFromContainerEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "engaged_until",
                table: "Container");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "Container");

            migrationBuilder.DropColumn(
                name: "is_engaged",
                table: "Container");

            migrationBuilder.DropColumn(
                name: "iso_number",
                table: "Container");

            migrationBuilder.DropColumn(
                name: "type_id",
                table: "Container");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "engaged_until",
                table: "Container",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "Container",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_engaged",
                table: "Container",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "iso_number",
                table: "Container",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "type_id",
                table: "Container",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
