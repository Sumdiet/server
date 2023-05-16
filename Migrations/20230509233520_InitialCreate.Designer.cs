﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NutriNow.Persistence;

#nullable disable

namespace NutriNow.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20230509233520_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("NutriNow.Domains.Food", b =>
                {
                    b.Property<int>("FoodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("FoodId"));

                    b.Property<string>("FoodName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MacroId")
                        .HasColumnType("integer");

                    b.HasKey("FoodId");

                    b.HasIndex("MacroId");

                    b.ToTable("Food");
                });

            modelBuilder.Entity("NutriNow.Domains.Macro", b =>
                {
                    b.Property<int>("MacroId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MacroId"));

                    b.Property<string>("Carbs")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("EntityId")
                        .HasColumnType("integer");

                    b.Property<string>("Fat")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Kcal")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Protein")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("Water")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("MacroId");

                    b.ToTable("Macro");
                });

            modelBuilder.Entity("NutriNow.Domains.Meal", b =>
                {
                    b.Property<int>("MealId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MealId"));

                    b.Property<int>("MacroGoalMacroId")
                        .HasColumnType("integer");

                    b.Property<string>("MealName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MealTime")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("MealId");

                    b.HasIndex("MacroGoalMacroId");

                    b.HasIndex("UserId");

                    b.ToTable("Meal");
                });

            modelBuilder.Entity("NutriNow.Domains.RegisteredFood", b =>
                {
                    b.Property<int>("RegisteredFoodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RegisteredFoodId"));

                    b.Property<int>("FoodId")
                        .HasColumnType("integer");

                    b.Property<int>("MealId")
                        .HasColumnType("integer");

                    b.Property<double>("Quantity")
                        .HasColumnType("double precision");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("RegisteredFoodId");

                    b.HasIndex("FoodId");

                    b.HasIndex("MealId");

                    b.ToTable("RegisteredFood");
                });

            modelBuilder.Entity("NutriNow.Domains.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("MacroGoalMacroId")
                        .HasColumnType("integer");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.HasIndex("MacroGoalMacroId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("NutriNow.Domains.UserInformation", b =>
                {
                    b.Property<int>("UserInformationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserInformationId"));

                    b.Property<string>("AgeInterval")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Goal")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HeightInterval")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("WeightInterval")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WorkingOutRoutine")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserInformationId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserInformation");
                });

            modelBuilder.Entity("NutriNow.Domains.Food", b =>
                {
                    b.HasOne("NutriNow.Domains.Macro", "Macro")
                        .WithMany()
                        .HasForeignKey("MacroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Macro");
                });

            modelBuilder.Entity("NutriNow.Domains.Meal", b =>
                {
                    b.HasOne("NutriNow.Domains.Macro", "MacroGoal")
                        .WithMany()
                        .HasForeignKey("MacroGoalMacroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NutriNow.Domains.User", null)
                        .WithMany("Meals")
                        .HasForeignKey("UserId");

                    b.Navigation("MacroGoal");
                });

            modelBuilder.Entity("NutriNow.Domains.RegisteredFood", b =>
                {
                    b.HasOne("NutriNow.Domains.Food", "Food")
                        .WithMany("RegisteredFoods")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NutriNow.Domains.Meal", null)
                        .WithMany("RegisteredFood")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");
                });

            modelBuilder.Entity("NutriNow.Domains.User", b =>
                {
                    b.HasOne("NutriNow.Domains.Macro", "MacroGoal")
                        .WithMany()
                        .HasForeignKey("MacroGoalMacroId");

                    b.Navigation("MacroGoal");
                });

            modelBuilder.Entity("NutriNow.Domains.UserInformation", b =>
                {
                    b.HasOne("NutriNow.Domains.User", null)
                        .WithOne("UserInformation")
                        .HasForeignKey("NutriNow.Domains.UserInformation", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NutriNow.Domains.Food", b =>
                {
                    b.Navigation("RegisteredFoods");
                });

            modelBuilder.Entity("NutriNow.Domains.Meal", b =>
                {
                    b.Navigation("RegisteredFood");
                });

            modelBuilder.Entity("NutriNow.Domains.User", b =>
                {
                    b.Navigation("Meals");

                    b.Navigation("UserInformation");
                });
#pragma warning restore 612, 618
        }
    }
}