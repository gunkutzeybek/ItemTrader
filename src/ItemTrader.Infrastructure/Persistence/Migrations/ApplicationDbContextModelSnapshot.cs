﻿// <auto-generated />
using System;
using ItemTrader.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ItemTrader.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ItemTrader.Domain.Entities.Proposal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OfferedItemId")
                        .HasColumnType("int");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProposedToId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("RequestedItemId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OfferedItemId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("ProposedToId");

                    b.HasIndex("RequestedItemId");

                    b.ToTable("Proposals");
                });

            modelBuilder.Entity("ItemTrader.Domain.Entities.TradeItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("TradeItems");
                });

            modelBuilder.Entity("ItemTrader.Domain.Entities.Trader", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AspNetUsers", t => t.ExcludeFromMigrations());
                });

            modelBuilder.Entity("ItemTrader.Domain.Entities.Proposal", b =>
                {
                    b.HasOne("ItemTrader.Domain.Entities.TradeItem", "OfferedItem")
                        .WithMany("OfferedProposals")
                        .HasForeignKey("OfferedItemId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ItemTrader.Domain.Entities.Trader", "Owner")
                        .WithMany("Proposals")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ItemTrader.Domain.Entities.Trader", "ProposedTo")
                        .WithMany("RecievedProposals")
                        .HasForeignKey("ProposedToId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ItemTrader.Domain.Entities.TradeItem", "RequestedItem")
                        .WithMany("RequestedProposals")
                        .HasForeignKey("RequestedItemId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("OfferedItem");

                    b.Navigation("Owner");

                    b.Navigation("ProposedTo");

                    b.Navigation("RequestedItem");
                });

            modelBuilder.Entity("ItemTrader.Domain.Entities.TradeItem", b =>
                {
                    b.HasOne("ItemTrader.Domain.Entities.Trader", "Owner")
                        .WithMany("TradeItems")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("ItemTrader.Domain.Entities.TradeItem", b =>
                {
                    b.Navigation("OfferedProposals");

                    b.Navigation("RequestedProposals");
                });

            modelBuilder.Entity("ItemTrader.Domain.Entities.Trader", b =>
                {
                    b.Navigation("Proposals");

                    b.Navigation("RecievedProposals");

                    b.Navigation("TradeItems");
                });
#pragma warning restore 612, 618
        }
    }
}
