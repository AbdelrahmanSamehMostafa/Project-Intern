using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wishlists = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEmailVerified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "PendingReqs",
                columns: table => new
                {
                    PendingReqId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuperAdminID = table.Column<int>(type: "int", nullable: false),
                    AdminID = table.Column<int>(type: "int", nullable: false),
                    AdminName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminMail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingReqs", x => x.PendingReqId);
                });

            migrationBuilder.CreateTable(
                name: "SuperAdmins",
                columns: table => new
                {
                    SuperAdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "admin"),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "admin")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperAdmins", x => x.SuperAdminId);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuperAdminId = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                    table.ForeignKey(
                        name: "FK_Admins_SuperAdmins_SuperAdminId",
                        column: x => x.SuperAdminId,
                        principalTable: "SuperAdmins",
                        principalColumn: "SuperAdminId");
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    HotelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    TotalNumberOfRooms = table.Column<int>(type: "int", nullable: false),
                    NumberOfAvailableRooms = table.Column<int>(type: "int", nullable: false),
                    Entertainments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ContactInfo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageUrls = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    averageRating = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.HotelId);
                    table.ForeignKey(
                        name: "FK_Hotels_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hotels_Admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admins",
                        principalColumn: "AdminId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    RoomType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    isAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                    table.ForeignKey(
                        name: "FK_Rooms_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "AddressId", "City", "Country" },
                values: new object[,]
                {
                    { 1, "Giza", "Egypt" },
                    { 2, "Cairo", "Egypt" },
                    { 3, "Hurghada", "Egypt" },
                    { 4, "Luxor", "Egypt" },
                    { 5, "Aswan", "Egypt" },
                    { 6, "Hurghada", "Egypt" },
                    { 7, "Sharm ElSheikh", "Egypt" },
                    { 8, "Ain Sokhna", "Egypt" },
                    { 9, "Ras Sedr", "Egypt" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Email", "FirstName", "IsEmailVerified", "LastName", "Password", "Wishlists" },
                values: new object[,]
                {
                    { 1, "eslam.waleed@gmail.com", "Eslam", true, "Waleed", "temp", "[\"1\",\"2\",\"3\"]" },
                    { 2, "abdelrahman.sameh@gmail.com", "Abdelrahman", true, "Sameh", "password123", "[\"1\",\"2\"]" },
                    { 3, "Ahmed.Mohamed@gmail.com", "Ahmed", true, "Mohamed", "password123", "[\"4\",\"5\"]" },
                    { 4, "temp@gmail.com", "test", true, "test", "temp", "[\"4\",\"5\"]" }
                });

            migrationBuilder.InsertData(
                table: "SuperAdmins",
                columns: new[] { "SuperAdminId", "Name", "Password" },
                values: new object[] { 1, "admin", "admin" });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "AdminId", "Email", "FirstName", "IsActive", "LastName", "Password", "SuperAdminId" },
                values: new object[,]
                {
                    { 1, "shady.waleed@gmail.com", "Shady", true, "Waleed", "temp", 1 },
                    { 2, "mohamed.salah@gmail.com", "Mohamed", true, "Salah", "adminpass123", 1 }
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "HotelId", "AddressId", "AdminId", "ContactInfo", "Description", "Entertainments", "ImageUrls", "IsActive", "Name", "NumberOfAvailableRooms", "Rating", "TotalNumberOfRooms", "averageRating" },
                values: new object[,]
                {
                    { 1, 1, 1, "0987654321", "A luxurious hotel with a view of the Nile.", "[\"Pool\",\"Gym\",\"Spa\"]", "[\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/524850774.jpg?k=b203619100e42da199bd131cd859d3b07b22d3716a0b19f45c2d1efed3fe6ec1\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/520473485.jpg?k=1bf83b123f220000211613f553daed1cdd5215cb9b9d0c9fb2b02cf37e77f53b\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/520473637.jpg?k=1cdff3a2caf2bc09c841ce9a506e93e497d9884c97dfeabf348a980f4ee7c928\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/524852168.jpg?k=bf8c176cf3ed0609bffd758ae84e93c0afc23c3fa686cc502e3608f4e6e28ba4\\u0026o=\\u0026hp=1\"]", true, "Nile Palace Hotel", 120, 4.0, 150, 5.0 },
                    { 2, 2, 1, "1112223333", "Experience the pyramids from the comfort of your room.", "[\"Pool\",\"Gym\",\"Restaurant\"]", "[\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/132392578.jpg?k=36f408eab4b81e6b93908f97bf948d9498e617ddd717c9b47a865accfd9ef034\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/134958505.jpg?k=144659ef64f3395d19a2e047fee7e492956aeb8d0c5a39e25edcd9d081012def\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/230095386.jpg?k=42c421355a2f88155e013adcd4fa47223ed925eba426c42c6c00347b3134e4c4\\u0026o=\\u0026hp=1\"]", true, "Pyramids View Hotel", 180, 4.5, 200, 0.0 },
                    { 3, 3, 2, "4445556666", "A resort with stunning views of the Red Sea.", "[\"Pool\",\"Beach Access\",\"Diving Center\"]", "[\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/277906335.jpg?k=0728b13b2796fe7938cad934580ed9680335b065f15bf454c01d4bd6274a378e\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/277906344.jpg?k=ea9def902741b4cb697fea8e63e3973284ad934151d2a21adb8b5ee3b5aac483\\u0026o=\\u0026hp=1\"]", true, "Solymar Soma Beach", 100, 3.5, 120, 0.0 },
                    { 4, 4, 2, "7778889999", "Stay at the heart of ancient Luxor.", "[\"Pool\",\"Gym\",\"Cultural Shows\"]", "[\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/476581584.jpg?k=f58890637be490f3c2ac308b6477fcfd39318c1645aa8955965c46e37dfe0cd6\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/476581592.jpg?k=4828f1841a092779d151c19ac5cf1858f89fc8cd9ec616f52587820158e2582a\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/476581589.jpg?k=da6d3f9a30c7d8260c37396bc3b4a154f52b586d0cb0058644cb6eb96547d3ea\\u0026o=\\u0026hp=1\"]", true, "Pyramisa Hotel Luxor", 150, 4.0, 180, 0.0 },
                    { 5, 5, 2, "9990001111", "Relax and unwind at the serene Aswan Retreat.", "[\"Pool\",\"Spa\",\"Nile Cruise\"]", "[\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/426899015.jpg?k=bf203184a29ba54a17019673775e942257d9a104d37edd5bd70c21d1c14df131\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/433682205.jpg?k=54b4255731815f8d441aba48caff95b42978da7418f9910c2f449ec14b04f731\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/433682175.jpg?k=ca637a1a53f9f59cce26359a09b50d49a02d6f89475581fe23b55f54beb16847\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/426526344.jpg?k=58a01e11e894798d38595a17f834ae4db31bc20ccc12ce6a268ce29443f31ccc\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/346689229.jpg?k=765e02a4817f6d94e9262571c3de6aa2b6b0e8809caf660ca0a6532f6e7b01f0\\u0026o=\\u0026hp=1\"]", true, "Kato Dool Wellness Aswan Resort", 80, 3.5, 100, 0.0 },
                    { 6, 6, 1, "9990001111", "Relax and unwind at the serene Aswan Retreat.", "[\"Pool\",\"Spa\",\"Cinema\",\"Gym\"]", "[\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/537868801.jpg?k=0be00f52a11ffb2ab5b0fa76d9c240b6216281d37325733b4d9f4c47c33316ca\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/538108568.jpg?k=b4056a34bd591de23ea755e29ebed5ac49d442697d1e0cd8770fecb2150ec780\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/540489415.jpg?k=988a95c60f3b8fa03eb98d8de7df0ba41e8e3e8c4f0b3f7a6666c898ddbd16c4\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/540468991.jpg?k=8fb435baa87c07a4886ebd70219647d49f4098a33664cef7a19daefe36bf3138\\u0026o=\\u0026hp=1\"]", true, "Davinci Beach Hotel", 50, 6.0, 90, 0.0 },
                    { 7, 7, 2, "9990001111", "Relax and unwind at the serene Aswan Retreat.", "[\"Pool\",\"Spa\",\"Nile Cruise\",\"Gym\"]", "[\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/476642414.jpg?k=b299afe8fb788d2f07ed8d455d2a5ee108b7aa9d1dd158bce7f6e8594b6b23da\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/469282264.jpg?k=ffbb57a1619377bfc2d9798bef37560a69c95c430c1c212163862f7ca1fb322d\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/476642883.jpg?k=0829695a81fb80a84ef6351e0b2d0e65650de7c2e8b53612c4f28f15c5e737a3\\u0026o=\\u0026hp=1\"]", true, "Ghazala Gardens", 180, 5.0, 190, 0.0 },
                    { 8, 8, 2, "9990001111", "Relax and unwind at the serene Aswan Retreat.", "[\"Pool\",\"Spa\",\"Cinema\",\"Gym\",\"Beach\"]", "[\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/549114940.jpg?k=9797a229e13a6051352066b66ffd1ec6ba1b72e98491a49cff2e2355f643ed8a\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/549114935.jpg?k=8babd6e32091e5b4cf72d6a3d841c5d4657c5313f5d5e0edee64dc84f89598c5\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/549114978.jpg?k=3e81221800f42cf658213d7fcf2f70d9fdbf2f2e92d3db1d753ff90b39b605fa\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/549114996.jpg?k=ecb0e70288f4faba4fde7587f319c4c67729a59bdbfce6eb0596ac50c8127ad9\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/549114877.jpg?k=972b540ac1e6a145d237c70de8774911874a9ab5dff26165ee607352866f9fef\\u0026o=\\u0026hp=1\"]", true, "Lasirena Mini Egypt", 40, 4.2000000000000002, 110, 0.0 },
                    { 9, 9, 1, "9990001111", "Relax and unwind at the serene Aswan Retreat.", "[\"Pool\",\"Cinema\",\"Gym\",\"Beach\"]", "[\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/535840098.jpg?k=79166f7ca744f0c6d60d45a1133ee53e383c91821ceb2da87fc20039ed86cfa9\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/541431499.jpg?k=5b0997797e626a7a60b30ff4af5a48bfd31b04946cf03e40634e2b4af9a7a998\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/541433214.jpg?k=7eb3ed246be88900fc95b3113c9d38fcb0c3bc1bae863e7d6938e14dfd84b5b2\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/542945965.jpg?k=4d8266ddb1c72ea2167c67fe11b88f98e5c914e817ce7cfc09b0be941128c0bd\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/541251754.jpg?k=0229dc6a0187d48f92635e7a14d1ba1e1c90b42affee2128ee0fea8051a60960\\u0026o=\\u0026hp=1\"]", true, "Nozha Beach", 70, 6.7000000000000002, 130, 0.0 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ReviewId", "Comment", "CustomerId", "Date", "HotelId", "Rating" },
                values: new object[,]
                {
                    { 1, "Great stay!", 1, new DateTime(2024, 6, 11, 2, 36, 24, 538, DateTimeKind.Local).AddTicks(8951), 1, 8 },
                    { 2, "Bad Service...", 2, new DateTime(2024, 6, 11, 2, 36, 24, 538, DateTimeKind.Local).AddTicks(8989), 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "RoomId", "Description", "HotelId", "Price", "RoomType", "isAvailable" },
                values: new object[,]
                {
                    { 1, "A deluxe room", 1, 200.0, "Single", true },
                    { 2, "A Triple room", 1, 400.0, "Double", true }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "BookingId", "CheckInDate", "CheckOutDate", "CustomerId", "RoomId", "Status" },
                values: new object[] { 1, new DateTime(2024, 6, 11, 2, 36, 24, 538, DateTimeKind.Local).AddTicks(9015), new DateTime(2024, 6, 13, 2, 36, 24, 538, DateTimeKind.Local).AddTicks(9018), 1, 1, "Confirmed" });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_SuperAdminId",
                table: "Admins",
                column: "SuperAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomerId",
                table: "Bookings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomId",
                table: "Bookings",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_AddressId",
                table: "Hotels",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_AdminId",
                table: "Hotels",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CustomerId",
                table: "Reviews",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_HotelId",
                table: "Reviews",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HotelId",
                table: "Rooms",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_SuperAdmins_Name",
                table: "SuperAdmins",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SuperAdmins_Password",
                table: "SuperAdmins",
                column: "Password",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "PendingReqs");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "SuperAdmins");
        }
    }
}
