using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OrderManagement.DAL
{
    public partial class OrdersDBContext : DbContext
    {
        public OrdersDBContext()
        {
        }

        public OrdersDBContext(DbContextOptions<OrdersDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<ItemOrder> ItemOrder { get; set; }
        public virtual DbSet<Order> Order { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-K4NQ2IO;Initial Catalog=OrdersDB;Integrated Security=True;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Address1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Address3)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasColumnName("State_")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Suburb)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.ItemCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Tax).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<ItemOrder>(entity =>
            {
                entity.ToTable("Item_Order");

                entity.Property(e => e.ItemOrderId).HasColumnName("Item_OrderId");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ExclAmount)
                    .HasColumnName("Excl_amount")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.InclAmount)
                    .HasColumnName("Incl_amount")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Note)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TaxAmount)
                    .HasColumnName("Tax_amount")
                    .HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemOrder)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Item_Orde__ItemI__2A4B4B5E");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ItemOrder)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Item_Orde__Order__2B3F6F97");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.InvDate).HasColumnType("datetime");

                entity.Property(e => e.InvNo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Note)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ReferNo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TotExcl)
                    .HasColumnName("Tot_Excl")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TotIncl)
                    .HasColumnName("Tot_Incl")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TotTax)
                    .HasColumnName("Tot_Tax")
                    .HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__CustomerI__2C3393D0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
