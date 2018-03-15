﻿// <auto-generated />
using DatabaseConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace DatabaseConnect.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DatabaseConnect.Entities.Author", b =>
                {
                    b.Property<int>("AuthorID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("AuthorID");

                    b.ToTable("tblAuthor");
                });

            modelBuilder.Entity("DatabaseConnect.Entities.AuthorBook", b =>
                {
                    b.Property<int>("BookID");

                    b.Property<int>("AuthorID")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(3);

                    b.HasKey("BookID", "AuthorID");

                    b.HasIndex("AuthorID");

                    b.ToTable("tblAuthorBook");
                });

            modelBuilder.Entity("DatabaseConnect.Entities.Book", b =>
                {
                    b.Property<int>("BookID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("DeweyDecimal")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("n/a");

                    b.Property<string>("FicID")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("n/a");

                    b.Property<string>("ISBN");

                    b.Property<string>("ImagePath");

                    b.Property<int>("PageCount");

                    b.Property<string>("Title");

                    b.HasKey("BookID");

                    b.ToTable("tblBook");
                });

            modelBuilder.Entity("DatabaseConnect.Entities.Checkout", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<int>("BookID");

                    b.Property<DateTime>("CheckoutDate");

                    b.Property<DateTime>("DueDate");

                    b.Property<int>("Renewals");

                    b.Property<int>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("BookID");

                    b.HasIndex("UserID");

                    b.ToTable("tblCheckouts");
                });

            modelBuilder.Entity("DatabaseConnect.Entities.Reservation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<int>("BookID");

                    b.Property<DateTime>("Datetime");

                    b.Property<int>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("BookID");

                    b.HasIndex("UserID");

                    b.ToTable("tblReservations");
                });

            modelBuilder.Entity("DatabaseConnect.Entities.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("FineAmount");

                    b.Property<string>("FullName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("Salt");

                    b.Property<string>("SchoolID");

                    b.Property<int>("TokenVersion");

                    b.HasKey("UserID");

                    b.ToTable("tblUsers");
                });

            modelBuilder.Entity("DatabaseConnect.Entities.UserUType", b =>
                {
                    b.Property<int>("UserID");

                    b.Property<int>("UTypeID");

                    b.HasKey("UserID", "UTypeID");

                    b.HasIndex("UTypeID");

                    b.ToTable("tblUserUType");
                });

            modelBuilder.Entity("DatabaseConnect.Entities.UType", b =>
                {
                    b.Property<int>("UTypeID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CheckoutLimit");

                    b.Property<string>("UTypeName");

                    b.Property<bool>("WriteAccess");

                    b.HasKey("UTypeID");

                    b.ToTable("tblUType");
                });

            modelBuilder.Entity("DatabaseConnect.Entities.AuthorBook", b =>
                {
                    b.HasOne("DatabaseConnect.Entities.Author", "Author")
                        .WithMany("AuthorBooks")
                        .HasForeignKey("AuthorID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DatabaseConnect.Entities.Book", "Book")
                        .WithMany("AuthorBooks")
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DatabaseConnect.Entities.Checkout", b =>
                {
                    b.HasOne("DatabaseConnect.Entities.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DatabaseConnect.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DatabaseConnect.Entities.Reservation", b =>
                {
                    b.HasOne("DatabaseConnect.Entities.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DatabaseConnect.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DatabaseConnect.Entities.UserUType", b =>
                {
                    b.HasOne("DatabaseConnect.Entities.UType", "UType")
                        .WithMany()
                        .HasForeignKey("UTypeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DatabaseConnect.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
