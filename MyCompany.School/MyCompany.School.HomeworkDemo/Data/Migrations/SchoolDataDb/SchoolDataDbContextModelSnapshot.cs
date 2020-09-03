﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyCompany.School.HomeworkDemo.Data;

namespace MyCompany.School.HomeworkDemo.Data.Migrations.SchoolDataDb
{
    [DbContext(typeof(SchoolDataDbContext))]
    partial class SchoolDataDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618

            modelBuilder.Entity("MyCompany.School.HomeworkDemo.Data.Homework", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("HomeworkDetails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LoadDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Homeworks");
                });
#pragma warning restore 612, 618
        }
    }
}