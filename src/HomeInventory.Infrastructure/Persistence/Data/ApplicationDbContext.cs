using System.Reflection;
using HomeInventory.Application.Common.Interfaces;
using HomeInventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Infrastructure.Persistence.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Item> Items => Set<Item>();
    public DbSet<Owner> Owners => Set<Owner>();
    public DbSet<WarrantyInfo> WarrantyInfos => Set<WarrantyInfo>();
    public DbSet<Tag> Tags => Set<Tag>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

}