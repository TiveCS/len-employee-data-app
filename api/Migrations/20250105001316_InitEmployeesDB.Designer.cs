﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Data;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(EmployeesDbContext))]
    [Migration("20250105001316_InitEmployeesDB")]
    partial class InitEmployeesDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("api.Models.Employee", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("BirthCity")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EmploymentLevelId")
                        .HasColumnType("int");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("NIK")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UnitId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmploymentLevelId");

                    b.HasIndex("NIK")
                        .IsUnique();

                    b.HasIndex("UnitId");

                    b.ToTable("employees");
                });

            modelBuilder.Entity("api.Models.EmployeeWorkHistory", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("employees_work_histories");
                });

            modelBuilder.Entity("api.Models.EmploymentLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("employment_levels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Junior Backend Developer"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Junior Frontend Developer"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Chief Executive Officer"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Project Manager"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Chief Technical Officer"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Software Architect"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Senior Software Developer"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Lead Marketing"
                        });
                });

            modelBuilder.Entity("api.Models.WorkUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("work_units");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "IT"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Sales"
                        });
                });

            modelBuilder.Entity("api.Models.Employee", b =>
                {
                    b.HasOne("api.Models.EmploymentLevel", "EmploymentLevel")
                        .WithMany("Employees")
                        .HasForeignKey("EmploymentLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.WorkUnit", "Unit")
                        .WithMany("Employees")
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmploymentLevel");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("api.Models.EmployeeWorkHistory", b =>
                {
                    b.HasOne("api.Models.Employee", "Employee")
                        .WithMany("WorkHistories")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("api.Models.Employee", b =>
                {
                    b.Navigation("WorkHistories");
                });

            modelBuilder.Entity("api.Models.EmploymentLevel", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("api.Models.WorkUnit", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}