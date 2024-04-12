﻿// <auto-generated />
using AppraisalManagentSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AppraisalManagentSystem.Migrations
{
    [DbContext(typeof(Db))]
    [Migration("20240306053719_wkanjakfcdas")]
    partial class wkanjakfcdas
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AppraisalManagentSystem.Models.Appraisal", b =>
                {
                    b.Property<int>("_detailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("_detailsId"), 1L, 1);

                    b.Property<int>("_appraisalId")
                        .HasColumnType("int");

                    b.Property<int>("_competency")
                        .HasColumnType("int");

                    b.Property<string>("_emp_comments")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("_emp_rating")
                        .HasColumnType("int");

                    b.Property<string>("_manager_comments")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("_manager_rating")
                        .HasColumnType("int");

                    b.HasKey("_detailsId");

                    b.ToTable("Appraisals");
                });

            modelBuilder.Entity("AppraisalManagentSystem.Models.AppraisalStatus", b =>
                {
                    b.Property<int>("AppraisalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppraisalId"), 1L, 1);

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("EndDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ManagerId")
                        .HasColumnType("int");

                    b.Property<string>("Objective")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AppraisalId");

                    b.ToTable("AppraisalStatus");
                });

            modelBuilder.Entity("AppraisalManagentSystem.Models.AppraisalWithCopm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AppraisalId")
                        .HasColumnType("int");

                    b.Property<int>("Compitency")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AppraisalWithCopms");
                });

            modelBuilder.Entity("AppraisalManagentSystem.Models.Competencies", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("CompetencyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Competencies");
                });

            modelBuilder.Entity("AppraisalManagentSystem.Models.Employees", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("_designation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("_emp_email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("_emp_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("_emp_password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("_emp_phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("_managerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("AppraisalManagentSystem.Models.LoginDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("user_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LoginDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
