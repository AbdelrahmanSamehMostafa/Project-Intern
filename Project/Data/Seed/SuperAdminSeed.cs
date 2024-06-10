
using Microsoft.EntityFrameworkCore;
public static class SuperAdminSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
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
    }
}
