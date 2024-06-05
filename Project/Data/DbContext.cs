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

        modelBuilder.Entity<Admin>().HasData(new Admin
        {
            AdminId = 1,
            SuperAdminId = 1,
            FirstName = "Shady",
            LastName = "Waleed",
            Email = "shady.waleed@gmail.com",
            Password = "adminpass123",
        });

        modelBuilder.Entity<Customer>().HasData(
            new Customer
            {
                CustomerId = 1,
                FirstName = "Eslam",
                LastName = "Waleed",
                Email = "eslam.waleed@gmail.com",
                Password = "password123"
            }
        );

        modelBuilder.Entity<Customer>().HasData(
            new Customer
            {
                CustomerId = 2,
                FirstName = "Abdelrahman",
                LastName = "Sameh",
                Email = "abdelrahman.sameh@gmail.com",
                Password = "password123"
            }
        );

        modelBuilder.Entity<Address>().HasData(
            new Address
            {
                HotelId = 1,
                AddressId = 1,
                City = "Giza",
                Country = "Egypt"
            }
        );

        modelBuilder.Entity<Hotel>().HasData(new Hotel
        {
            HotelId = 1,
            AdminId = 1,
            Name = "Hotel 1",
            Rating = 4.5,
            TotalNumberOfRooms = 100,
            NumberOfAvailableRooms = 90,
            ContactInfo = "1234567890",
            IsActive = true,
        });

        modelBuilder.Entity<Room>().HasData(
            new Room
            {
                HotelId = 1,
                RoomId = 2,
                RoomType = "Triple",
                Description = "A Triple room",
                Price = 400,
                Availability = true,
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
                Availability = true,

            }
        );

        modelBuilder.Entity<Admin>().HasData(new Admin
        {
            AdminId = 2,
            SuperAdminId = 1,
            FirstName = "Mohamed",
            LastName = "Salah",
            Email = "mohamed.salah@gmail.com",
            Password = "adminpass123",
        });

        modelBuilder.Entity<Review>().HasData(new Review
        {
            ReviewId = 1,
            Comment = "Great stay!",
            Rating = 5,
            CustomerId = 1,
            HotelId = 1
        });

        modelBuilder.Entity<Booking>().HasData(new Booking
        {
            BookingId = 1,
            CustomerId = 1,
            RoomId = 1,
            CheckInDate = DateTime.Now,
            CheckOutDate = DateTime.Now.AddDays(2),
            Status = "Confirmed"
        });
    }

    public void InitializeDatabase()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
}
