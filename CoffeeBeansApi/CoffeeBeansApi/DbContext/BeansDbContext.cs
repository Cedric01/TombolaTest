using CoffeeBeansApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CoffeeBeansApi.DbContext;

public class BeansDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public BeansDbContext(DbContextOptions<BeansDbContext> options) : base(options) { }

    public DbSet<AllBeans> AllBeans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AllBeans>().HasKey(b => b.Id);
    }
}



