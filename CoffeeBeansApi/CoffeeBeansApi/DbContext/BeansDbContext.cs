using CoffeeBeansApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CoffeeBeansApi.DbContext;

public class BeansDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public BeansDbContext(DbContextOptions<BeansDbContext> options) : base(options) { }

    public DbSet<Beans> Beans { get; set; }

    public DbSet<BeanSelectionHistory> BeanSelectionHistory { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Beans>().HasKey(b => b.Id);
        modelBuilder.Entity<BeanSelectionHistory>().HasKey(b => b.Id);

        modelBuilder.Entity<BeanSelectionHistory>()
            .HasOne<Beans>()
            .WithMany()
            .HasForeignKey(b => b.BeanId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}



