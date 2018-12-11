﻿// <auto-generated />
using System;
using Aloha.Model.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Aloha.Migrations
{
    [DbContext(typeof(AlohaContext))]
    [Migration("20181211225810_XminAsConcurrencyToken")]
    partial class XminAsConcurrencyToken
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Aloha.Model.Entities.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<uint>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("xmin")
                        .HasColumnType("xid");

                    b.Property<byte[]>("Data")
                        .IsRequired();

                    b.Property<string>("MediaType")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Aloha.Model.Entities.Floor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<uint>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("xmin")
                        .HasColumnType("xid");

                    b.Property<int?>("ImageId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("OfficeId");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.HasIndex("OfficeId");

                    b.ToTable("Floors");
                });

            modelBuilder.Entity("Aloha.Model.Entities.Office", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<uint>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("xmin")
                        .HasColumnType("xid");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Offices");
                });

            modelBuilder.Entity("Aloha.Model.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<uint>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("xmin")
                        .HasColumnType("xid");

                    b.Property<string>("PasswordHash")
                        .IsRequired();

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("UserName");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            ConcurrencyToken = 0u,
                            PasswordHash = "8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918",
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("Aloha.Model.Entities.Worker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<uint>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("xmin")
                        .HasColumnType("xid");

                    b.Property<string>("Email");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Notes");

                    b.Property<int?>("PhotoId");

                    b.Property<string>("Surname");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PhotoId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("Aloha.Model.Entities.Workstation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<uint>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("xmin")
                        .HasColumnType("xid");

                    b.Property<int>("FloorId");

                    b.Property<int?>("WorkerId");

                    b.Property<decimal>("X");

                    b.Property<decimal>("Y");

                    b.HasKey("Id");

                    b.HasIndex("FloorId");

                    b.HasIndex("WorkerId")
                        .IsUnique();

                    b.ToTable("Workstations");
                });

            modelBuilder.Entity("Aloha.Model.Entities.Floor", b =>
                {
                    b.HasOne("Aloha.Model.Entities.File", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");

                    b.HasOne("Aloha.Model.Entities.Office", "Office")
                        .WithMany("Floors")
                        .HasForeignKey("OfficeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Aloha.Model.Entities.Worker", b =>
                {
                    b.HasOne("Aloha.Model.Entities.File", "Photo")
                        .WithMany()
                        .HasForeignKey("PhotoId");

                    b.HasOne("Aloha.Model.Entities.User", "User")
                        .WithOne("Worker")
                        .HasForeignKey("Aloha.Model.Entities.Worker", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Aloha.Model.Entities.Workstation", b =>
                {
                    b.HasOne("Aloha.Model.Entities.Floor", "Floor")
                        .WithMany("Workstations")
                        .HasForeignKey("FloorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Aloha.Model.Entities.Worker", "Worker")
                        .WithOne("Workstation")
                        .HasForeignKey("Aloha.Model.Entities.Workstation", "WorkerId");
                });
#pragma warning restore 612, 618
        }
    }
}
