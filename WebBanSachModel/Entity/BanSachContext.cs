using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBanSachModel.Entity
{
    public class BanSachContext : DbContext
    {
        public BanSachContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("tbl_User");
                e.HasKey(p => p.Id);
                e.Property(p => p.HoVaTen).IsRequired().HasMaxLength(100);
                e.Property(p => p.MatKhau).IsRequired().HasMaxLength(500);
            });
        }
    }
}
