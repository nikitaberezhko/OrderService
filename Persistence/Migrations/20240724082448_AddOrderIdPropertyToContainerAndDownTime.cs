using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderIdPropertyToContainerAndDownTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Container_Orders_OrderId",
                table: "Container");

            migrationBuilder.DropForeignKey(
                name: "FK_DownTime_Orders_OrderId",
                table: "DownTime");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "DownTime",
                newName: "order_id");

            migrationBuilder.RenameIndex(
                name: "IX_DownTime_OrderId",
                table: "DownTime",
                newName: "IX_DownTime_order_id");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Container",
                newName: "order_id");

            migrationBuilder.RenameIndex(
                name: "IX_Container_OrderId",
                table: "Container",
                newName: "IX_Container_order_id");

            migrationBuilder.AlterColumn<Guid>(
                name: "order_id",
                table: "DownTime",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "order_id",
                table: "Container",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Container_Orders_order_id",
                table: "Container",
                column: "order_id",
                principalTable: "Orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DownTime_Orders_order_id",
                table: "DownTime",
                column: "order_id",
                principalTable: "Orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Container_Orders_order_id",
                table: "Container");

            migrationBuilder.DropForeignKey(
                name: "FK_DownTime_Orders_order_id",
                table: "DownTime");

            migrationBuilder.RenameColumn(
                name: "order_id",
                table: "DownTime",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_DownTime_order_id",
                table: "DownTime",
                newName: "IX_DownTime_OrderId");

            migrationBuilder.RenameColumn(
                name: "order_id",
                table: "Container",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Container_order_id",
                table: "Container",
                newName: "IX_Container_OrderId");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId",
                table: "DownTime",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId",
                table: "Container",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Container_Orders_OrderId",
                table: "Container",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_DownTime_Orders_OrderId",
                table: "DownTime",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "id");
        }
    }
}
