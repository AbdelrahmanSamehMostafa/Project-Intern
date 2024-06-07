using Azure.Core;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<SuperAdmin> SuperAdmins { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<PendingReq> PendingReqs { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SuperAdmin>()
            .HasIndex(sa => sa.Name)
            .IsUnique();

        modelBuilder.Entity<SuperAdmin>()
            .HasIndex(sa => sa.Password)
            .IsUnique();

        modelBuilder.Entity<SuperAdmin>()
            .Property(sa => sa.Name)
            .HasDefaultValue("admin");

        modelBuilder.Entity<SuperAdmin>()
            .Property(sa => sa.Password)
            .HasDefaultValue("admin");

        modelBuilder.Entity<SuperAdmin>().HasData(new SuperAdmin("admin", "admin"));

        modelBuilder.Entity<Admin>().HasData(
            new Admin
            {
                AdminId = 1,
                SuperAdminId = 1,
                FirstName = "Shady",
                LastName = "Waleed",
                Email = "shady.waleed@gmail.com",
                Password = "adminpass123"
            },
            new Admin
            {
                AdminId = 2,
                SuperAdminId = 1,
                FirstName = "Mohamed",
                LastName = "Salah",
                Email = "mohamed.salah@gmail.com",
                Password = "adminpass123"
            }
        );

        modelBuilder.Entity<Customer>().HasData(
            new Customer
            {
                CustomerId = 1,
                FirstName = "Eslam",
                LastName = "Waleed",
                Email = "eslam.waleed@gmail.com",
                Password = "password123"
            },
            new Customer
            {
                CustomerId = 2,
                FirstName = "Abdelrahman",
                LastName = "Sameh",
                Email = "abdelrahman.sameh@gmail.com",
                Password = "password123"
            }
        );

        // Seed Hotels first
        modelBuilder.Entity<Hotel>().HasData(
            new Hotel
            {
                HotelId = 1,
                AdminId = 1,
                Name = "Nile Palace Hotel",
                Rating = 4,
                TotalNumberOfRooms = 150,
                NumberOfAvailableRooms = 120,
                ContactInfo = "0987654321",
                AddressId = 1,
                Entertainments = new List<string> { "Pool", "Gym", "Spa" },
                Description = "A luxurious hotel with a view of the Nile.",
                ImageUrls = new List<string>
                {
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/524850774.jpg?k=b203619100e42da199bd131cd859d3b07b22d3716a0b19f45c2d1efed3fe6ec1&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/520473485.jpg?k=1bf83b123f220000211613f553daed1cdd5215cb9b9d0c9fb2b02cf37e77f53b&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/520473637.jpg?k=1cdff3a2caf2bc09c841ce9a506e93e497d9884c97dfeabf348a980f4ee7c928&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/524852168.jpg?k=bf8c176cf3ed0609bffd758ae84e93c0afc23c3fa686cc502e3608f4e6e28ba4&o=&hp=1",
                },
                IsActive = true
            },
            new Hotel
            {
                HotelId = 2,
                AdminId = 1,
                Name = "Pyramids View Hotel",
                Rating = 4.5,
                TotalNumberOfRooms = 200,
                NumberOfAvailableRooms = 180,
                ContactInfo = "1112223333",
                AddressId = 2,
                Entertainments = new List<string> { "Pool", "Gym", "Restaurant" },
                Description = "Experience the pyramids from the comfort of your room.",
                ImageUrls = new List<string>
                {
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/132392578.jpg?k=36f408eab4b81e6b93908f97bf948d9498e617ddd717c9b47a865accfd9ef034&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/134958505.jpg?k=144659ef64f3395d19a2e047fee7e492956aeb8d0c5a39e25edcd9d081012def&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/230095386.jpg?k=42c421355a2f88155e013adcd4fa47223ed925eba426c42c6c00347b3134e4c4&o=&hp=1",
                },
                IsActive = true
            },
            new Hotel
            {
                HotelId = 3,
                AdminId = 2,
                Name = "Solymar Soma Beach",
                Rating = 3.5,
                TotalNumberOfRooms = 120,
                NumberOfAvailableRooms = 100,
                ContactInfo = "4445556666",
                AddressId = 3,
                Entertainments = new List<string> { "Pool", "Beach Access", "Diving Center" },
                Description = "A resort with stunning views of the Red Sea.",
                ImageUrls = new List<string>
                {
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/277906335.jpg?k=0728b13b2796fe7938cad934580ed9680335b065f15bf454c01d4bd6274a378e&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/277906344.jpg?k=ea9def902741b4cb697fea8e63e3973284ad934151d2a21adb8b5ee3b5aac483&o=&hp=1",
                },
                IsActive = true
            },
            new Hotel
            {
                HotelId = 4,
                AdminId = 2,
                Name = "Pyramisa Hotel Luxor",
                Rating = 4,
                TotalNumberOfRooms = 180,
                NumberOfAvailableRooms = 150,
                ContactInfo = "7778889999",
                AddressId = 4,
                Entertainments = new List<string> { "Pool", "Gym", "Cultural Shows" },
                Description = "Stay at the heart of ancient Luxor.",
                ImageUrls = new List<string>
                {
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/476581584.jpg?k=f58890637be490f3c2ac308b6477fcfd39318c1645aa8955965c46e37dfe0cd6&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/476581592.jpg?k=4828f1841a092779d151c19ac5cf1858f89fc8cd9ec616f52587820158e2582a&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/476581589.jpg?k=da6d3f9a30c7d8260c37396bc3b4a154f52b586d0cb0058644cb6eb96547d3ea&o=&hp=1",
                },
                IsActive = true
            },
            new Hotel
            {
                HotelId = 5,
                AdminId = 2,
                Name = "Kato Dool Wellness Aswan Resort",
                Rating = 3.5,
                TotalNumberOfRooms = 100,
                NumberOfAvailableRooms = 80,
                ContactInfo = "9990001111",
                AddressId = 5,
                Entertainments = new List<string> { "Pool", "Spa", "Nile Cruise" },
                Description = "Relax and unwind at the serene Aswan Retreat.",
                ImageUrls = new List<string>
                {
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/426899015.jpg?k=bf203184a29ba54a17019673775e942257d9a104d37edd5bd70c21d1c14df131&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/433682205.jpg?k=54b4255731815f8d441aba48caff95b42978da7418f9910c2f449ec14b04f731&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/433682175.jpg?k=ca637a1a53f9f59cce26359a09b50d49a02d6f89475581fe23b55f54beb16847&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/426526344.jpg?k=58a01e11e894798d38595a17f834ae4db31bc20ccc12ce6a268ce29443f31ccc&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/346689229.jpg?k=765e02a4817f6d94e9262571c3de6aa2b6b0e8809caf660ca0a6532f6e7b01f0&o=&hp=1",
                },
                IsActive = true
            },
            new Hotel
            {
                HotelId = 6,
                AdminId = 1,
                Name = "Davinci Beach Hotel",
                Rating = 6,
                TotalNumberOfRooms = 90,
                NumberOfAvailableRooms = 50,
                ContactInfo = "9990001111",
                AddressId = 6,
                Entertainments = new List<string> { "Pool", "Spa", "Cinema" ,"Gym"},
                Description = "Relax and unwind at the serene Aswan Retreat.",
                ImageUrls = new List<string>
                {
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/537868801.jpg?k=0be00f52a11ffb2ab5b0fa76d9c240b6216281d37325733b4d9f4c47c33316ca&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/538108568.jpg?k=b4056a34bd591de23ea755e29ebed5ac49d442697d1e0cd8770fecb2150ec780&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/540489415.jpg?k=988a95c60f3b8fa03eb98d8de7df0ba41e8e3e8c4f0b3f7a6666c898ddbd16c4&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/540468991.jpg?k=8fb435baa87c07a4886ebd70219647d49f4098a33664cef7a19daefe36bf3138&o=&hp=1",
                },
                IsActive = true
            },
            new Hotel
            {
                HotelId = 7,
                AdminId = 2,
                Name = "Ghazala Gardens",
                Rating = 5,
                TotalNumberOfRooms = 190,
                NumberOfAvailableRooms = 180,
                ContactInfo = "9990001111",
                AddressId = 7,
                Entertainments = new List<string> { "Pool", "Spa", "Nile Cruise" ,"Gym"},
                Description = "Relax and unwind at the serene Aswan Retreat.",
                ImageUrls = new List<string>
                {
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/476642414.jpg?k=b299afe8fb788d2f07ed8d455d2a5ee108b7aa9d1dd158bce7f6e8594b6b23da&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/469282264.jpg?k=ffbb57a1619377bfc2d9798bef37560a69c95c430c1c212163862f7ca1fb322d&o=&hp=1",
                    "https://cf.bstatic.com/xdata/images/hotel/max1024x768/476642883.jpg?k=0829695a81fb80a84ef6351e0b2d0e65650de7c2e8b53612c4f28f15c5e737a3&o=&hp=1",
                },
                IsActive = true
            }

        );


        // Seed Addresses next
        modelBuilder.Entity<Address>().HasData(
            new Address
            {
                AddressId = 1,
                City = "Giza",
                Country = "Egypt",
            },
            new Address
            {
                AddressId = 2,
                City = "Cairo",
                Country = "Egypt",
            },
            new Address
            {
                AddressId = 3,
                City = "Alex",
                Country = "Egypt",
            },
            new Address
            {
                AddressId = 4,
                City = "Aswan",
                Country = "Egypt",
            },
            new Address
            {
                AddressId = 5,
                City = "Faiyum",
                Country = "Egypt",
            },
            new Address
            {
                AddressId = 6,
                City = "Hurghada",
                Country = "Egypt",
            }, 
            new Address
            {
                AddressId = 7,
                City = "Sharm ElSheikh",
                Country = "Egypt",
            }


        );

        modelBuilder.Entity<Room>().HasData(
            new Room
            {
                HotelId = 1,
                RoomId = 1,
                RoomType = "Deluxe",
                Description = "A deluxe room",
                Price = 200,
                Availability = true
            },
            new Room
            {
                HotelId = 1,
                RoomId = 2,
                RoomType = "Triple",
                Description = "A Triple room",
                Price = 400,
                Availability = true
            }
        );

        modelBuilder.Entity<Review>().HasData(
            new Review
            {
                ReviewId = 1,
                Comment = "Great stay!",
                Rating = 5,
                CustomerId = 1,
                HotelId = 1
            }
        );

        modelBuilder.Entity<Booking>().HasData(
            new Booking
            {
                BookingId = 1,
                CustomerId = 1,
                RoomId = 1,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(2),
                Status = "Confirmed"
            }
        );


    }
    public void InitializeDatabase()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
}
