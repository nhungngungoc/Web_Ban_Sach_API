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
        public DbSet<Category> categorys { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderDetails> orderDetails { get; set; }
        //public DbSet<Xa> XA { get; set; }
        public DbSet<Huyen> HUYEN { get; set; }
        public DbSet<Tinh> TINH { get; set; }
        public DbSet<Xa> XA { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("tbl_User");
                e.HasKey(p => p.Id);
                e.Property(p => p.HoVaTen).HasMaxLength(100);
                e.Property(p => p.MatKhau).IsRequired().HasMaxLength(500);
            });
            modelBuilder.Entity<Category>(e =>
            {
                e.ToTable("tbl_Category");
                e.HasKey(p => p.Id);
                e.Property(p => p.Name).IsRequired().HasMaxLength(100);
            });
            modelBuilder.Entity<Product>(e =>
            {
                e.ToTable("tbl_Product");
                e.HasKey(p => p.Id);
                e.Property(p => p.TenSP).IsRequired().HasMaxLength(100);
                
            });
            modelBuilder.Entity<Cart>(e =>
            {
                e.ToTable("tbl_Cart");
                e.HasKey(p => p.Id);
                e.HasOne(c => c.Product).WithMany(p => p.carts).HasForeignKey(p => p.ProductId);
                e.HasOne(u => u.user).WithMany(fr => fr.carts).HasForeignKey(u => u.UserId);
            });

            modelBuilder.Entity<Order>(e =>
            {
                e.ToTable("tbl_Order");
                e.HasKey(p => p.Id);
                e.HasOne(u => u.user).WithMany(fr => fr.Orders).HasForeignKey(u => u.UserId);
            });
            modelBuilder.Entity<OrderDetails>(e =>
            {
                e.ToTable("tbl_OrderDetail");
                e.HasKey(p => p.Id);
                e.HasOne(c => c.order).WithMany(p => p.orderDetails).HasForeignKey(p => p.OrderId);
                e.HasOne(u => u.product).WithMany(fr => fr.OrderDetails).HasForeignKey(u => u.ProductId);
            });
        }
    }
}
