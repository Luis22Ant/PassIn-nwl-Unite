﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PassIn.Infrastructure;

#nullable disable

namespace PassIn.Infrastructure.Migrations
{
    [DbContext(typeof(PassInDbContext))]
    [Migration("20240404173455_passwordAttendee")]
    partial class passwordAttendee
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PassIn.Infrastructure.Entities.Attendee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Event_Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Event_Id");

                    b.ToTable("Attendee");
                });

            modelBuilder.Entity("PassIn.Infrastructure.Entities.CheckIn", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Attendee_Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Attendee_Id")
                        .IsUnique();

                    b.ToTable("CheckIns");
                });

            modelBuilder.Entity("PassIn.Infrastructure.Entities.Events", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Maximum_Attendees")
                        .HasColumnType("int");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("PassIn.Infrastructure.Entities.Attendee", b =>
                {
                    b.HasOne("PassIn.Infrastructure.Entities.Events", null)
                        .WithMany("Attendees")
                        .HasForeignKey("Event_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PassIn.Infrastructure.Entities.CheckIn", b =>
                {
                    b.HasOne("PassIn.Infrastructure.Entities.Attendee", "Attendee")
                        .WithOne("CheckIn")
                        .HasForeignKey("PassIn.Infrastructure.Entities.CheckIn", "Attendee_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attendee");
                });

            modelBuilder.Entity("PassIn.Infrastructure.Entities.Attendee", b =>
                {
                    b.Navigation("CheckIn");
                });

            modelBuilder.Entity("PassIn.Infrastructure.Entities.Events", b =>
                {
                    b.Navigation("Attendees");
                });
#pragma warning restore 612, 618
        }
    }
}
