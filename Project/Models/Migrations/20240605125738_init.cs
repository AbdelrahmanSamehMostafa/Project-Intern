using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PendingReqs_Admins_AdminId",
                table: "PendingReqs");

            migrationBuilder.DropForeignKey(
                name: "FK_PendingReqs_SuperAdmins_SuperAdminId",
                table: "PendingReqs");

            migrationBuilder.DropIndex(
                name: "IX_PendingReqs_SuperAdminId",
                table: "PendingReqs");

            migrationBuilder.DropColumn(
                name: "SuperAdminID",
                table: "PendingReqs");

            migrationBuilder.RenameColumn(
                name: "SuperAdminId",
                table: "PendingReqs",
                newName: "SuperAdminID");

            migrationBuilder.AlterColumn<int>(
                name: "AdminId",
                table: "PendingReqs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AdminID",
                table: "PendingReqs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 1,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 6, 5, 15, 57, 38, 183, DateTimeKind.Local).AddTicks(3122), new DateTime(2024, 6, 7, 15, 57, 38, 183, DateTimeKind.Local).AddTicks(3123) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 6, 5, 15, 57, 38, 183, DateTimeKind.Local).AddTicks(3092));

            migrationBuilder.AddForeignKey(
                name: "FK_PendingReqs_Admins_AdminId",
                table: "PendingReqs",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "AdminId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PendingReqs_Admins_AdminId",
                table: "PendingReqs");

            migrationBuilder.RenameColumn(
                name: "SuperAdminID",
                table: "PendingReqs",
                newName: "SuperAdminId");

            migrationBuilder.AlterColumn<int>(
                name: "AdminId",
                table: "PendingReqs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AdminID",
                table: "PendingReqs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SuperAdminID",
                table: "PendingReqs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 1,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 6, 5, 15, 52, 41, 401, DateTimeKind.Local).AddTicks(683), new DateTime(2024, 6, 7, 15, 52, 41, 401, DateTimeKind.Local).AddTicks(684) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 6, 5, 15, 52, 41, 401, DateTimeKind.Local).AddTicks(652));

            migrationBuilder.CreateIndex(
                name: "IX_PendingReqs_SuperAdminId",
                table: "PendingReqs",
                column: "SuperAdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_PendingReqs_Admins_AdminId",
                table: "PendingReqs",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "AdminId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PendingReqs_SuperAdmins_SuperAdminId",
                table: "PendingReqs",
                column: "SuperAdminId",
                principalTable: "SuperAdmins",
                principalColumn: "SuperAdminId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
