﻿// <auto-generated />
using System;
using Biogas_BackendEF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Biogas_BackendEF.Migrations
{
    [DbContext(typeof(BiogasDataBaseContext))]
    [Migration("20250205140347_BiogasAdded")]
    partial class BiogasAdded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Biogas_BackendEF.Models.Biogas", b =>
                {
                    b.Property<int>("BiogasId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BiogasId"));

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("ProducerId")
                        .HasColumnType("int");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("BiogasId");

                    b.HasIndex("ProducerId");

                    b.ToTable("BiogasInventory");
                });

            modelBuilder.Entity("Biogas_BackendEF.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OrderId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<int>("BiogasId")
                        .HasColumnType("int")
                        .HasColumnName("BiogasId");

                    b.Property<double>("Quantity")
                        .HasColumnType("float")
                        .HasColumnName("Quantity");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Status");

                    b.Property<string>("TransactionId")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("TransactionId");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UId");

                    b.HasKey("OrderId");

                    b.HasIndex("BiogasId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Biogas_BackendEF.Models.Producer", b =>
                {
                    b.Property<int>("ProducerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ProducerId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProducerId"));

                    b.Property<int>("ProductionCapacity")
                        .HasColumnType("int")
                        .HasColumnName("ProductionCapacity");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Status");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("ProducerId");

                    b.HasIndex("UserId");

                    b.ToTable("Producers");
                });

            modelBuilder.Entity("Biogas_BackendEF.Models.User", b =>
                {
                    b.Property<int>("UId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Password");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Role");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Username");

                    b.HasKey("UId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Biogas_BackendEF.Models.WasteContributor", b =>
                {
                    b.Property<int>("ContributorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ContributorId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContributorId"));

                    b.Property<int?>("CollectedBy")
                        .HasColumnType("int")
                        .HasColumnName("CollectedBy");

                    b.Property<DateOnly>("ContributionDate")
                        .HasColumnType("date")
                        .HasColumnName("ContributionDate");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Status");

                    b.Property<int>("WasteQuantity")
                        .HasColumnType("int")
                        .HasColumnName("WasteQuantity");

                    b.Property<string>("WasteType")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("WasteType");

                    b.Property<int>("userId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("ContributorId");

                    b.HasIndex("CollectedBy");

                    b.HasIndex("userId");

                    b.ToTable("WasteContributor");
                });

            modelBuilder.Entity("Biogas_BackendEF.Models.Biogas", b =>
                {
                    b.HasOne("Biogas_BackendEF.Models.Producer", "Producer")
                        .WithMany()
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producer");
                });

            modelBuilder.Entity("Biogas_BackendEF.Models.Order", b =>
                {
                    b.HasOne("Biogas_BackendEF.Models.Biogas", "Biogas")
                        .WithMany()
                        .HasForeignKey("BiogasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Biogas_BackendEF.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Biogas");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Biogas_BackendEF.Models.Producer", b =>
                {
                    b.HasOne("Biogas_BackendEF.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Biogas_BackendEF.Models.WasteContributor", b =>
                {
                    b.HasOne("Biogas_BackendEF.Models.Producer", "Producer")
                        .WithMany()
                        .HasForeignKey("CollectedBy");

                    b.HasOne("Biogas_BackendEF.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producer");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
