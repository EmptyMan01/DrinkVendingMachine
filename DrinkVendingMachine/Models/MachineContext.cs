using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;

namespace DrinkVendingMachine.Models
{
    public partial class MachineContext : DbContext
    {
        public MachineContext()
        {
        }

        public MachineContext(DbContextOptions<MachineContext> options)
            : base(options)
        {
            // Database.EnsureCreated();
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //}

        public virtual DbSet<Coins> Coins { get; set; }
        public virtual DbSet<Memory> Memory { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Sprites> Sprites { get; set; }
        public virtual DbSet<User> USERS { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=PC-Lite;Initial Catalog=Machine;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.IdProduct)
                    .HasName("PK__Products__2E8946D4DFAE69AA");

                entity.Property(e => e.Sprite);
                entity.Property(e => e.NameSprite);
                entity.Property(e => e.Quantity);

            });
            //modelBuilder.Entity<IdentityUserLogin<int>>()
            //      modelBuilder.Entity<IdentityUserLogin<int>>()
            //     .Property(login => login.UserId)
            //     .ForMySQLHasColumnType("PK")
            //     .UseSqlServerIdentityColumn()
            //     .UseMySQLAutoIncrementColumn("AI");
           
            

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<Memory>(entity =>
            {
                entity.HasKey(e => e.Id);

     

                entity.HasOne(d => d.IdProductsNavigation)
                    .WithMany(p => p.Me)
                    .HasForeignKey(d => d.ProductsIdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Memory");
            });

            modelBuilder.Entity<Sprites>(entity =>
            {
                entity.HasKey(e => e.IdSprite)
                    .HasName("PK__Sprites__CBDCF78DF64EC46F");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
