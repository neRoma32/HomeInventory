using HomeInventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HomeInventory.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Room> Rooms { get; }
    DbSet<Category> Categories { get; }
    DbSet<Item> Items { get; }
    DbSet<Owner> Owners { get; }

    DbSet<WarrantyInfo> WarrantyInfos { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    DbSet<Tag> Tags { get; }
}