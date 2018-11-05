﻿// <auto-generated />
using Aloha.Models.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Aloha.Migrations
{
    [DbContext(typeof(AlohaContext))]
    partial class AlohaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Aloha.Model.Entities.Floor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Floors");
                });

            modelBuilder.Entity("Aloha.Model.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Aloha.Model.Entities.Worker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Notes");

                    b.Property<string>("PhotoUrl");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("Aloha.Model.Entities.Workstation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FloorId");

                    b.Property<int>("WorkerId");

                    b.Property<float>("X");

                    b.Property<float>("Y");

                    b.HasKey("Id");

                    b.HasIndex("FloorId");

                    b.ToTable("Workstations");
                });

            modelBuilder.Entity("Aloha.Model.Entities.Workstation", b =>
                {
                    b.HasOne("Aloha.Model.Entities.Floor")
                        .WithMany("Workstations")
                        .HasForeignKey("FloorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
