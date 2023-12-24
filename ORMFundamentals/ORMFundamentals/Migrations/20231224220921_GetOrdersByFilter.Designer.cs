﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ORMFundamentals.Entities;

#nullable disable

namespace ORMFundamentals.Migrations
{
    [DbContext(typeof(EFDbContext))]
    [Migration("20231224220921_GetOrdersByFilter")]
    partial class GetOrdersByFilter
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ORMFundamentals.Entities.Order", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<DateTime>("CreatedDate")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("getDate()");

                b.Property<int>("ProductId")
                    .HasColumnType("int");

                b.Property<string>("Status")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime>("UpdatedDate")
                    .HasColumnType("datetime2");

                b.HasKey("Id");

                b.HasIndex("ProductId");

                b.ToTable("Orders");
            });

            modelBuilder.Entity("ORMFundamentals.Entities.Product", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("Description")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<double>("Height")
                    .HasColumnType("float");

                b.Property<double>("Length")
                    .HasColumnType("float");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<double>("Weight")
                    .HasColumnType("float");

                b.Property<double>("Width")
                    .HasColumnType("float");

                b.HasKey("Id");

                b.ToTable("Products");
            });

            modelBuilder.Entity("ORMFundamentals.Entities.Order", b =>
            {
                b.HasOne("ORMFundamentals.Entities.Product", "Product")
                    .WithMany("Orders")
                    .HasForeignKey("ProductId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Product");
            });

            modelBuilder.Entity("ORMFundamentals.Entities.Product", b =>
            {
                b.Navigation("Orders");
            });
#pragma warning restore 612, 618
        }
    }
}
