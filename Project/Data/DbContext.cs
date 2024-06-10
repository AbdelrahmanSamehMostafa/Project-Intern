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
        SuperAdminSeed.Seed(modelBuilder);
        AdminSeed.Seed(modelBuilder);
        CustomerSeed.Seed(modelBuilder);
        HotelSeed.Seed(modelBuilder);
        AddressSeed.Seed(modelBuilder);
        RoomSeed.Seed(modelBuilder);
        ReviewSeed.Seed(modelBuilder);
        BookingSeed.Seed(modelBuilder); 
    }

    public void InitializeDatabase()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
}
