﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using RestaurantSystemApplication.Models;
using System;

namespace RestaurantSystemApplication.Migrations
{
    [DbContext(typeof(RestaurantContext))]
    [Migration("20180525013846_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("RestaurantSystemApplication.Models.Plate", b =>
                {
                    b.Property<int>("PlateID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Ingredients");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.HasKey("PlateID");

                    b.ToTable("Plates");
                });

            modelBuilder.Entity("RestaurantSystemApplication.Models.Restaurant", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("Name");

                    b.Property<string>("OwnerName");

                    b.Property<string>("Phone");

                    b.Property<DateTime>("RegistrationDate");

                    b.Property<string>("State");

                    b.HasKey("ID");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("RestaurantSystemApplication.Models.Serve", b =>
                {
                    b.Property<int>("ServeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FoodType");

                    b.Property<int>("PlateID");

                    b.Property<int>("RestaurantID");

                    b.HasKey("ServeID");

                    b.HasIndex("PlateID");

                    b.HasIndex("RestaurantID");

                    b.ToTable("Serves");
                });

            modelBuilder.Entity("RestaurantSystemApplication.Models.Serve", b =>
                {
                    b.HasOne("RestaurantSystemApplication.Models.Plate", "Plate")
                        .WithMany("Serves")
                        .HasForeignKey("PlateID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RestaurantSystemApplication.Models.Restaurant", "Restaurant")
                        .WithMany("Serves")
                        .HasForeignKey("RestaurantID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}