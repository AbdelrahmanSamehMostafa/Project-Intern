using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class Initialseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Admins",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PendingReqs",
                columns: table => new
                {
                    PendingReqId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuperAdminID = table.Column<int>(type: "int", nullable: false),
                    SuperAdminId = table.Column<int>(type: "int", nullable: false),
                    AdminID = table.Column<int>(type: "int", nullable: true),
                    AdminId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingReqs", x => x.PendingReqId);
                    table.ForeignKey(
                        name: "FK_PendingReqs_Admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admins",
                        principalColumn: "AdminId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PendingReqs_SuperAdmins_SuperAdminId",
                        column: x => x.SuperAdminId,
                        principalTable: "SuperAdmins",
                        principalColumn: "SuperAdminId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 1,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 2,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 1,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 6, 5, 15, 52, 41, 401, DateTimeKind.Local).AddTicks(683), new DateTime(2024, 6, 7, 15, 52, 41, 401, DateTimeKind.Local).AddTicks(684) });

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "HotelId",
                keyValue: 1,
                columns: new[] { "ContactInfo", "Name", "NumberOfAvailableRooms", "Rating", "TotalNumberOfRooms" },
                values: new object[] { "0987654321", "Nile Palace Hotel", 120, 8.0, 150 });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "HotelId", "AdminId", "ContactInfo", "Description", "Entertainments", "ImageUrls", "IsActive", "Name", "NumberOfAvailableRooms", "Rating", "TotalNumberOfRooms" },
                values: new object[,]
                {
                    { 2, 1, "1112223333", null, null, null, true, "Pyramids View Hotel", 180, 9.0, 200 },
                    { 3, 1, "4445556666", null, null, null, true, "Red Sea Resort", 100, 7.0, 120 },
                    { 4, 2, "7778889999", null, null, null, true, "Luxor Palace", 150, 8.0, 180 },
                    { 5, 2, "9990001111", null, null, null, true, "Aswan Retreat", 80, 7.0, 100 }
                });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 6, 5, 15, 52, 41, 401, DateTimeKind.Local).AddTicks(652));

            migrationBuilder.CreateIndex(
                name: "IX_PendingReqs_AdminId",
                table: "PendingReqs",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_PendingReqs_SuperAdminId",
                table: "PendingReqs",
                column: "SuperAdminId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PendingReqs");

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "HotelId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "HotelId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "HotelId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "HotelId",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Admins");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: 1,
                columns: new[] { "CheckInDate", "CheckOutDate" },
                values: new object[] { new DateTime(2024, 6, 4, 19, 50, 26, 900, DateTimeKind.Local).AddTicks(7435), new DateTime(2024, 6, 6, 19, 50, 26, 900, DateTimeKind.Local).AddTicks(7438) });

            migrationBuilder.UpdateData(
                table: "Hotels",
                keyColumn: "HotelId",
                keyValue: 1,
                columns: new[] { "ContactInfo", "Name", "NumberOfAvailableRooms", "Rating", "TotalNumberOfRooms" },
                values: new object[] { "1234567890", "Hotel 1", 90, 4.5, 100 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewId",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 6, 4, 19, 50, 26, 900, DateTimeKind.Local).AddTicks(7381));
        }
    }
}
