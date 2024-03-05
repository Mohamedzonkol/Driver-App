using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Driver.Entites.DbsSet;
using Microsoft.EntityFrameworkCore;

namespace Driver.DataServicers.Data
{
    public class AppDbContext:DbContext
    {
       public virtual DbSet<Drivers> Drivers { get; set; }
       public virtual DbSet<Achevment> Achevments { get; set; }

       public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
       {
       }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
           base.OnModelCreating(modelBuilder);
           modelBuilder.Entity<Achevment>(entity =>
           {
               entity.HasOne(o => o.Driver).WithMany(m => m.Achevments)
                   .HasForeignKey(f => f.DriverId).OnDelete(DeleteBehavior.NoAction)
                   .HasConstraintName("FK_AchevmentsAndDriver");
           });
       }
    }
}
