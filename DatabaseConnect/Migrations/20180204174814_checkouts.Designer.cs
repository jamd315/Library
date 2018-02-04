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
    [Migration("20180204174814_checkouts")]
    partial class checkouts
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DatabaseConnect.Entities.Author", b =>
                {
                    b.Property<int>("AuthorID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BookID");

                    b.Property<string>("Name");

                    b.HasKey("AuthorID");

                    b.HasIndex("BookID");

                    b.ToTable("tblAuthor");
                });

            modelBuilder.Entity("DatabaseConnect.Entities.AuthorBook", b =>
                {
                    b.Property<int>("BookID");

                    b.Property<int>("AuthorID");

                    b.HasKey("BookID", "AuthorID");

                    b.HasIndex("AuthorID");

                    b.ToTable("tblAuthorBook");
                });

            modelBuilder.Entity("DatabaseConnect.Entities.Book", b =>
                {
                    b.Property<int>("BookID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DeweyDecimal");

                    b.Property<string>("FicID");

                    b.Property<string>("ISBN");

                    b.Property<int>("PageCount");

                    b.Property<string>("Title");

                    b.HasKey("BookID");

                    b.ToTable("tblBook");
                });

            modelBuilder.Entity("DatabaseConnect.Entities.Checkout", b =>
                {
                    b.Property<int>("UserID");

                    b.Property<int>("BookID");

                    b.HasKey("UserID", "BookID");

                    b.HasIndex("BookID");

                    b.ToTable("tblCheckouts");
                });

            modelBuilder.Entity("DatabaseConnect.Entities.Cover", b =>
                {
                    b.Property<int>("CoverID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Base64Encoded");

                    b.Property<int>("BookID");

                    b.HasKey("CoverID");

                    b.HasIndex("BookID")
                        .IsUnique();

                    b.ToTable("tblCovers");
                });

            modelBuilder.Entity("DatabaseConnect.Entities.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FullName");

                    b.Property<int>("SchoolID");

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

            modelBuilder.Entity("DatabaseConnect.Entities.Author", b =>
                {
                    b.HasOne("DatabaseConnect.Entities.Book")
                        .WithMany("Authors")
                        .HasForeignKey("BookID");
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

            modelBuilder.Entity("DatabaseConnect.Entities.Cover", b =>
                {
                    b.HasOne("DatabaseConnect.Entities.Book", "Book")
                        .WithOne("Cover")
                        .HasForeignKey("DatabaseConnect.Entities.Cover", "BookID")
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
