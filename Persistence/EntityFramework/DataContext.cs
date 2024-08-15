using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityFramework;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        => optionsBuilder.UseLazyLoadingProxies();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().Property(x => x.Id).HasColumnName("id");
        modelBuilder.Entity<Order>().Property(x => x.ClientId).HasColumnName("client_id");
        modelBuilder.Entity<Order>().Property(x => x.DateStart).HasColumnName("date_start");
        modelBuilder.Entity<Order>().Property(x => x.DateEnd).HasColumnName("date_end");
        modelBuilder.Entity<Order>().Property(x => x.HubStartId).HasColumnName("hub_start_id");
        modelBuilder.Entity<Order>().Property(x => x.HubEndId).HasColumnName("hub_end_id");
        modelBuilder.Entity<Order>().Property(x => x.Price).HasColumnName("price");
        modelBuilder.Entity<Order>().Property(x => x.Costs).HasColumnName("costs");
        modelBuilder.Entity<Order>().Property(x => x.IsDeleted).HasColumnName("is_deleted");
        

        modelBuilder.Entity<Container>().Property(x => x.Id).HasColumnName("id");
        modelBuilder.Entity<Container>().Property(x => x.OrderId).HasColumnName("order_id");


        modelBuilder.Entity<DownTime>().Property(x => x.Id).HasColumnName("id");
        modelBuilder.Entity<DownTime>().Property(x => x.OrderId).HasColumnName("order_id");
        modelBuilder.Entity<DownTime>().Property(x => x.DateStart).HasColumnName("date_start");
        modelBuilder.Entity<DownTime>().Property(x => x.DateEnd).HasColumnName("date_end");
        modelBuilder.Entity<DownTime>().Property(x => x.HubId).HasColumnName("hub_id");
        modelBuilder.Entity<DownTime>().Property(x => x.IsDeleted).HasColumnName("is_deleted");
    }
}