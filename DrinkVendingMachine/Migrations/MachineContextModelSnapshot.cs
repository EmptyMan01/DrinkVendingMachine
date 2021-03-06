// <auto-generated />
using System;
using DrinkVendingMachine.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DrinkVendingMachine.Migrations
{
    [DbContext(typeof(MachineContext))]
    partial class MachineContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DrinkVendingMachine.Models.Coins", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Five")
                        .HasColumnType("int");

                    b.Property<int?>("One")
                        .HasColumnType("int");

                    b.Property<int?>("Ten")
                        .HasColumnType("int");

                    b.Property<int?>("Two")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Coins");
                });

            modelBuilder.Entity("DrinkVendingMachine.Models.Memory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Five")
                        .HasColumnType("int");

                    b.Property<int>("One")
                        .HasColumnType("int");

                    b.Property<int>("ProductsIdProduct")
                        .HasColumnType("int");

                    b.Property<int>("Sum")
                        .HasColumnType("int");

                    b.Property<int>("Ten")
                        .HasColumnType("int");

                    b.Property<int>("Two")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductsIdProduct");

                    b.ToTable("Memory");
                });

            modelBuilder.Entity("DrinkVendingMachine.Models.Products", b =>
                {
                    b.Property<int>("IdProduct")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NameSprite")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Sprite")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdProduct")
                        .HasName("PK__Products__2E8946D4DFAE69AA");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("DrinkVendingMachine.Models.Sprites", b =>
                {
                    b.Property<int>("IdSprite")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("IdSprite")
                        .HasName("PK__Sprites__CBDCF78DF64EC46F");

                    b.ToTable("Sprites");
                });

            modelBuilder.Entity("DrinkVendingMachine.Models.Memory", b =>
                {
                    b.HasOne("DrinkVendingMachine.Models.Products", "IdProductsNavigation")
                        .WithMany("Me")
                        .HasForeignKey("ProductsIdProduct")
                        .HasConstraintName("FK_Products_Memory")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
