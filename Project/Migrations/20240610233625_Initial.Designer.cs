﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HotelBookingSystem.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240610233625_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressId"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("AddressId");

                    b.ToTable("Addresses");

                    b.HasData(
                        new
                        {
                            AddressId = 1,
                            City = "Giza",
                            Country = "Egypt"
                        },
                        new
                        {
                            AddressId = 2,
                            City = "Cairo",
                            Country = "Egypt"
                        },
                        new
                        {
                            AddressId = 3,
                            City = "Hurghada",
                            Country = "Egypt"
                        },
                        new
                        {
                            AddressId = 4,
                            City = "Luxor",
                            Country = "Egypt"
                        },
                        new
                        {
                            AddressId = 5,
                            City = "Aswan",
                            Country = "Egypt"
                        },
                        new
                        {
                            AddressId = 6,
                            City = "Hurghada",
                            Country = "Egypt"
                        },
                        new
                        {
                            AddressId = 7,
                            City = "Sharm ElSheikh",
                            Country = "Egypt"
                        },
                        new
                        {
                            AddressId = 8,
                            City = "Ain Sokhna",
                            Country = "Egypt"
                        },
                        new
                        {
                            AddressId = 9,
                            City = "Ras Sedr",
                            Country = "Egypt"
                        });
                });

            modelBuilder.Entity("Admin", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SuperAdminId")
                        .HasColumnType("int");

                    b.HasKey("AdminId");

                    b.HasIndex("SuperAdminId");

                    b.ToTable("Admins");

                    b.HasData(
                        new
                        {
                            AdminId = 1,
                            Email = "shady.waleed@gmail.com",
                            FirstName = "Shady",
                            IsActive = true,
                            LastName = "Waleed",
                            Password = "temp",
                            SuperAdminId = 1
                        },
                        new
                        {
                            AdminId = 2,
                            Email = "mohamed.salah@gmail.com",
                            FirstName = "Mohamed",
                            IsActive = true,
                            LastName = "Salah",
                            Password = "adminpass123",
                            SuperAdminId = 1
                        });
                });

            modelBuilder.Entity("Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingId"));

                    b.Property<DateTime>("CheckInDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CheckOutDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("BookingId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RoomId");

                    b.ToTable("Bookings");

                    b.HasData(
                        new
                        {
                            BookingId = 1,
                            CheckInDate = new DateTime(2024, 6, 11, 2, 36, 24, 538, DateTimeKind.Local).AddTicks(9015),
                            CheckOutDate = new DateTime(2024, 6, 13, 2, 36, 24, 538, DateTimeKind.Local).AddTicks(9018),
                            CustomerId = 1,
                            RoomId = 1,
                            Status = "Confirmed"
                        });
                });

            modelBuilder.Entity("Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsEmailVerified")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Wishlists")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            CustomerId = 1,
                            Email = "eslam.waleed@gmail.com",
                            FirstName = "Eslam",
                            IsEmailVerified = true,
                            LastName = "Waleed",
                            Password = "temp",
                            Wishlists = "[\"1\",\"2\",\"3\"]"
                        },
                        new
                        {
                            CustomerId = 2,
                            Email = "abdelrahman.sameh@gmail.com",
                            FirstName = "Abdelrahman",
                            IsEmailVerified = true,
                            LastName = "Sameh",
                            Password = "password123",
                            Wishlists = "[\"1\",\"2\"]"
                        },
                        new
                        {
                            CustomerId = 3,
                            Email = "Ahmed.Mohamed@gmail.com",
                            FirstName = "Ahmed",
                            IsEmailVerified = true,
                            LastName = "Mohamed",
                            Password = "password123",
                            Wishlists = "[\"4\",\"5\"]"
                        },
                        new
                        {
                            CustomerId = 4,
                            Email = "temp@gmail.com",
                            FirstName = "test",
                            IsEmailVerified = true,
                            LastName = "test",
                            Password = "temp",
                            Wishlists = "[\"4\",\"5\"]"
                        });
                });

            modelBuilder.Entity("Hotel", b =>
                {
                    b.Property<int>("HotelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HotelId"));

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<int>("AdminId")
                        .HasColumnType("int");

                    b.Property<string>("ContactInfo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Entertainments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrls")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("NumberOfAvailableRooms")
                        .HasColumnType("int");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.Property<int>("TotalNumberOfRooms")
                        .HasColumnType("int");

                    b.Property<double>("averageRating")
                        .HasColumnType("float");

                    b.HasKey("HotelId");

                    b.HasIndex("AddressId");

                    b.HasIndex("AdminId");

                    b.ToTable("Hotels");

                    b.HasData(
                        new
                        {
                            HotelId = 1,
                            AddressId = 1,
                            AdminId = 1,
                            ContactInfo = "0987654321",
                            Description = "A luxurious hotel with a view of the Nile.",
                            Entertainments = "[\"Pool\",\"Gym\",\"Spa\"]",
                            ImageUrls = "[\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/524850774.jpg?k=b203619100e42da199bd131cd859d3b07b22d3716a0b19f45c2d1efed3fe6ec1\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/520473485.jpg?k=1bf83b123f220000211613f553daed1cdd5215cb9b9d0c9fb2b02cf37e77f53b\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/520473637.jpg?k=1cdff3a2caf2bc09c841ce9a506e93e497d9884c97dfeabf348a980f4ee7c928\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/524852168.jpg?k=bf8c176cf3ed0609bffd758ae84e93c0afc23c3fa686cc502e3608f4e6e28ba4\\u0026o=\\u0026hp=1\"]",
                            IsActive = true,
                            Name = "Nile Palace Hotel",
                            NumberOfAvailableRooms = 120,
                            Rating = 4.0,
                            TotalNumberOfRooms = 150,
                            averageRating = 5.0
                        },
                        new
                        {
                            HotelId = 2,
                            AddressId = 2,
                            AdminId = 1,
                            ContactInfo = "1112223333",
                            Description = "Experience the pyramids from the comfort of your room.",
                            Entertainments = "[\"Pool\",\"Gym\",\"Restaurant\"]",
                            ImageUrls = "[\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/132392578.jpg?k=36f408eab4b81e6b93908f97bf948d9498e617ddd717c9b47a865accfd9ef034\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/134958505.jpg?k=144659ef64f3395d19a2e047fee7e492956aeb8d0c5a39e25edcd9d081012def\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/230095386.jpg?k=42c421355a2f88155e013adcd4fa47223ed925eba426c42c6c00347b3134e4c4\\u0026o=\\u0026hp=1\"]",
                            IsActive = true,
                            Name = "Pyramids View Hotel",
                            NumberOfAvailableRooms = 180,
                            Rating = 4.5,
                            TotalNumberOfRooms = 200,
                            averageRating = 0.0
                        },
                        new
                        {
                            HotelId = 3,
                            AddressId = 3,
                            AdminId = 2,
                            ContactInfo = "4445556666",
                            Description = "A resort with stunning views of the Red Sea.",
                            Entertainments = "[\"Pool\",\"Beach Access\",\"Diving Center\"]",
                            ImageUrls = "[\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/277906335.jpg?k=0728b13b2796fe7938cad934580ed9680335b065f15bf454c01d4bd6274a378e\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/277906344.jpg?k=ea9def902741b4cb697fea8e63e3973284ad934151d2a21adb8b5ee3b5aac483\\u0026o=\\u0026hp=1\"]",
                            IsActive = true,
                            Name = "Solymar Soma Beach",
                            NumberOfAvailableRooms = 100,
                            Rating = 3.5,
                            TotalNumberOfRooms = 120,
                            averageRating = 0.0
                        },
                        new
                        {
                            HotelId = 4,
                            AddressId = 4,
                            AdminId = 2,
                            ContactInfo = "7778889999",
                            Description = "Stay at the heart of ancient Luxor.",
                            Entertainments = "[\"Pool\",\"Gym\",\"Cultural Shows\"]",
                            ImageUrls = "[\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/476581584.jpg?k=f58890637be490f3c2ac308b6477fcfd39318c1645aa8955965c46e37dfe0cd6\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/476581592.jpg?k=4828f1841a092779d151c19ac5cf1858f89fc8cd9ec616f52587820158e2582a\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/476581589.jpg?k=da6d3f9a30c7d8260c37396bc3b4a154f52b586d0cb0058644cb6eb96547d3ea\\u0026o=\\u0026hp=1\"]",
                            IsActive = true,
                            Name = "Pyramisa Hotel Luxor",
                            NumberOfAvailableRooms = 150,
                            Rating = 4.0,
                            TotalNumberOfRooms = 180,
                            averageRating = 0.0
                        },
                        new
                        {
                            HotelId = 5,
                            AddressId = 5,
                            AdminId = 2,
                            ContactInfo = "9990001111",
                            Description = "Relax and unwind at the serene Aswan Retreat.",
                            Entertainments = "[\"Pool\",\"Spa\",\"Nile Cruise\"]",
                            ImageUrls = "[\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/426899015.jpg?k=bf203184a29ba54a17019673775e942257d9a104d37edd5bd70c21d1c14df131\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/433682205.jpg?k=54b4255731815f8d441aba48caff95b42978da7418f9910c2f449ec14b04f731\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/433682175.jpg?k=ca637a1a53f9f59cce26359a09b50d49a02d6f89475581fe23b55f54beb16847\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/426526344.jpg?k=58a01e11e894798d38595a17f834ae4db31bc20ccc12ce6a268ce29443f31ccc\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/346689229.jpg?k=765e02a4817f6d94e9262571c3de6aa2b6b0e8809caf660ca0a6532f6e7b01f0\\u0026o=\\u0026hp=1\"]",
                            IsActive = true,
                            Name = "Kato Dool Wellness Aswan Resort",
                            NumberOfAvailableRooms = 80,
                            Rating = 3.5,
                            TotalNumberOfRooms = 100,
                            averageRating = 0.0
                        },
                        new
                        {
                            HotelId = 6,
                            AddressId = 6,
                            AdminId = 1,
                            ContactInfo = "9990001111",
                            Description = "Relax and unwind at the serene Aswan Retreat.",
                            Entertainments = "[\"Pool\",\"Spa\",\"Cinema\",\"Gym\"]",
                            ImageUrls = "[\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/537868801.jpg?k=0be00f52a11ffb2ab5b0fa76d9c240b6216281d37325733b4d9f4c47c33316ca\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/538108568.jpg?k=b4056a34bd591de23ea755e29ebed5ac49d442697d1e0cd8770fecb2150ec780\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/540489415.jpg?k=988a95c60f3b8fa03eb98d8de7df0ba41e8e3e8c4f0b3f7a6666c898ddbd16c4\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/540468991.jpg?k=8fb435baa87c07a4886ebd70219647d49f4098a33664cef7a19daefe36bf3138\\u0026o=\\u0026hp=1\"]",
                            IsActive = true,
                            Name = "Davinci Beach Hotel",
                            NumberOfAvailableRooms = 50,
                            Rating = 6.0,
                            TotalNumberOfRooms = 90,
                            averageRating = 0.0
                        },
                        new
                        {
                            HotelId = 7,
                            AddressId = 7,
                            AdminId = 2,
                            ContactInfo = "9990001111",
                            Description = "Relax and unwind at the serene Aswan Retreat.",
                            Entertainments = "[\"Pool\",\"Spa\",\"Nile Cruise\",\"Gym\"]",
                            ImageUrls = "[\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/476642414.jpg?k=b299afe8fb788d2f07ed8d455d2a5ee108b7aa9d1dd158bce7f6e8594b6b23da\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/469282264.jpg?k=ffbb57a1619377bfc2d9798bef37560a69c95c430c1c212163862f7ca1fb322d\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/476642883.jpg?k=0829695a81fb80a84ef6351e0b2d0e65650de7c2e8b53612c4f28f15c5e737a3\\u0026o=\\u0026hp=1\"]",
                            IsActive = true,
                            Name = "Ghazala Gardens",
                            NumberOfAvailableRooms = 180,
                            Rating = 5.0,
                            TotalNumberOfRooms = 190,
                            averageRating = 0.0
                        },
                        new
                        {
                            HotelId = 8,
                            AddressId = 8,
                            AdminId = 2,
                            ContactInfo = "9990001111",
                            Description = "Relax and unwind at the serene Aswan Retreat.",
                            Entertainments = "[\"Pool\",\"Spa\",\"Cinema\",\"Gym\",\"Beach\"]",
                            ImageUrls = "[\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/549114940.jpg?k=9797a229e13a6051352066b66ffd1ec6ba1b72e98491a49cff2e2355f643ed8a\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/549114935.jpg?k=8babd6e32091e5b4cf72d6a3d841c5d4657c5313f5d5e0edee64dc84f89598c5\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/549114978.jpg?k=3e81221800f42cf658213d7fcf2f70d9fdbf2f2e92d3db1d753ff90b39b605fa\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/549114996.jpg?k=ecb0e70288f4faba4fde7587f319c4c67729a59bdbfce6eb0596ac50c8127ad9\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/549114877.jpg?k=972b540ac1e6a145d237c70de8774911874a9ab5dff26165ee607352866f9fef\\u0026o=\\u0026hp=1\"]",
                            IsActive = true,
                            Name = "Lasirena Mini Egypt",
                            NumberOfAvailableRooms = 40,
                            Rating = 4.2000000000000002,
                            TotalNumberOfRooms = 110,
                            averageRating = 0.0
                        },
                        new
                        {
                            HotelId = 9,
                            AddressId = 9,
                            AdminId = 1,
                            ContactInfo = "9990001111",
                            Description = "Relax and unwind at the serene Aswan Retreat.",
                            Entertainments = "[\"Pool\",\"Cinema\",\"Gym\",\"Beach\"]",
                            ImageUrls = "[\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/535840098.jpg?k=79166f7ca744f0c6d60d45a1133ee53e383c91821ceb2da87fc20039ed86cfa9\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/541431499.jpg?k=5b0997797e626a7a60b30ff4af5a48bfd31b04946cf03e40634e2b4af9a7a998\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/541433214.jpg?k=7eb3ed246be88900fc95b3113c9d38fcb0c3bc1bae863e7d6938e14dfd84b5b2\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/542945965.jpg?k=4d8266ddb1c72ea2167c67fe11b88f98e5c914e817ce7cfc09b0be941128c0bd\\u0026o=\\u0026hp=1\",\"https://cf.bstatic.com/xdata/images/hotel/max1024x768/541251754.jpg?k=0229dc6a0187d48f92635e7a14d1ba1e1c90b42affee2128ee0fea8051a60960\\u0026o=\\u0026hp=1\"]",
                            IsActive = true,
                            Name = "Nozha Beach",
                            NumberOfAvailableRooms = 70,
                            Rating = 6.7000000000000002,
                            TotalNumberOfRooms = 130,
                            averageRating = 0.0
                        });
                });

            modelBuilder.Entity("PendingReq", b =>
                {
                    b.Property<int>("PendingReqId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PendingReqId"));

                    b.Property<int>("AdminID")
                        .HasColumnType("int");

                    b.Property<string>("AdminMail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdminName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SuperAdminID")
                        .HasColumnType("int");

                    b.HasKey("PendingReqId");

                    b.ToTable("PendingReqs");
                });

            modelBuilder.Entity("Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewId"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("ReviewId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("HotelId");

                    b.ToTable("Reviews");

                    b.HasData(
                        new
                        {
                            ReviewId = 1,
                            Comment = "Great stay!",
                            CustomerId = 1,
                            Date = new DateTime(2024, 6, 11, 2, 36, 24, 538, DateTimeKind.Local).AddTicks(8951),
                            HotelId = 1,
                            Rating = 8
                        },
                        new
                        {
                            ReviewId = 2,
                            Comment = "Bad Service...",
                            CustomerId = 2,
                            Date = new DateTime(2024, 6, 11, 2, 36, 24, 538, DateTimeKind.Local).AddTicks(8989),
                            HotelId = 1,
                            Rating = 2
                        });
                });

            modelBuilder.Entity("Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("RoomType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isAvailable")
                        .HasColumnType("bit");

                    b.HasKey("RoomId");

                    b.HasIndex("HotelId");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            RoomId = 1,
                            Description = "A deluxe room",
                            HotelId = 1,
                            Price = 200.0,
                            RoomType = "Single",
                            isAvailable = true
                        },
                        new
                        {
                            RoomId = 2,
                            Description = "A Triple room",
                            HotelId = 1,
                            Price = 400.0,
                            RoomType = "Double",
                            isAvailable = true
                        });
                });

            modelBuilder.Entity("SuperAdmin", b =>
                {
                    b.Property<int>("SuperAdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SuperAdminId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValue("admin");

                    b.Property<string>("Password")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValue("admin");

                    b.HasKey("SuperAdminId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("Password")
                        .IsUnique();

                    b.ToTable("SuperAdmins");

                    b.HasData(
                        new
                        {
                            SuperAdminId = 1,
                            Name = "admin",
                            Password = "admin"
                        });
                });

            modelBuilder.Entity("Admin", b =>
                {
                    b.HasOne("SuperAdmin", "SuperAdmin")
                        .WithMany()
                        .HasForeignKey("SuperAdminId");

                    b.Navigation("SuperAdmin");
                });

            modelBuilder.Entity("Booking", b =>
                {
                    b.HasOne("Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Hotel", b =>
                {
                    b.HasOne("Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Admin", "HotelOwner")
                        .WithMany("Hotels")
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("HotelOwner");
                });

            modelBuilder.Entity("Review", b =>
                {
                    b.HasOne("Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hotel", "Hotel")
                        .WithMany("Reviews")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("Room", b =>
                {
                    b.HasOne("Hotel", "Hotel")
                        .WithMany("Rooms")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("Admin", b =>
                {
                    b.Navigation("Hotels");
                });

            modelBuilder.Entity("Hotel", b =>
                {
                    b.Navigation("Reviews");

                    b.Navigation("Rooms");
                });
#pragma warning restore 612, 618
        }
    }
}
