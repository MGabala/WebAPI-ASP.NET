﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPI_ASP.NET6.Db;

#nullable disable

namespace WebAPI_ASP.NET6.Migrations
{
    [DbContext(typeof(ProductDb))]
    [Migration("20220412162948_ProductsDBWorkWithDB")]
    partial class ProductsDBWorkWithDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.4");

            modelBuilder.Entity("WebAPI_ASP.NET6.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Desc")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<float>("Price")
                        .HasColumnType("REAL");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WebAPI.Models.TypeOfProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Color")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("TypeOfProduct");
                });

            modelBuilder.Entity("WebAPI.Models.TypeOfProduct", b =>
                {
                    b.HasOne("WebAPI_ASP.NET6.Entities.Product", null)
                        .WithMany("TypeOfProduct")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("WebAPI_ASP.NET6.Entities.Product", b =>
                {
                    b.Navigation("TypeOfProduct");
                });
#pragma warning restore 612, 618
        }
    }
}
