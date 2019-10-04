﻿// <auto-generated />
using System;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataLayer.Migrations
{
    [DbContext(typeof(LibationContext))]
    [Migration("20190114191724_NullableCategory")]
    partial class NullableCategory
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataLayer.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AudibleProductId");

                    b.Property<int?>("CategoryId");

                    b.Property<DateTime?>("DatePublished");

                    b.Property<string>("Description");

                    b.Property<bool>("HasBookDetails");

                    b.Property<bool>("IsAbridged");

                    b.Property<int>("LengthInMinutes");

                    b.Property<string>("PictureId");

                    b.Property<string>("Publisher");

                    b.Property<string>("Title");

                    b.HasKey("BookId");

                    b.HasIndex("AudibleProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("DataLayer.BookContributor", b =>
                {
                    b.Property<int>("BookId");

                    b.Property<int>("ContributorId");

                    b.Property<int>("Role");

                    b.Property<byte>("Order");

                    b.HasKey("BookId", "ContributorId", "Role");

                    b.HasIndex("BookId");

                    b.HasIndex("ContributorId");

                    b.ToTable("BookContributor");
                });

            modelBuilder.Entity("DataLayer.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AudibleCategoryId");

                    b.Property<string>("Name");

                    b.Property<int?>("ParentCategoryCategoryId");

                    b.HasKey("CategoryId");

                    b.HasIndex("AudibleCategoryId");

                    b.HasIndex("ParentCategoryCategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DataLayer.Contributor", b =>
                {
                    b.Property<int>("ContributorId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AudibleAuthorId");

                    b.Property<string>("Name");

                    b.HasKey("ContributorId");

                    b.HasIndex("Name");

                    b.ToTable("Contributors");
                });

            modelBuilder.Entity("DataLayer.LibraryBook", b =>
                {
                    b.Property<int>("BookId");

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("DownloadBookLink");

                    b.HasKey("BookId");

                    b.ToTable("Library");
                });

            modelBuilder.Entity("DataLayer.Series", b =>
                {
                    b.Property<int>("SeriesId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AudibleSeriesId");

                    b.Property<string>("Name");

                    b.HasKey("SeriesId");

                    b.HasIndex("AudibleSeriesId");

                    b.ToTable("Series");
                });

            modelBuilder.Entity("DataLayer.SeriesBook", b =>
                {
                    b.Property<int>("SeriesId");

                    b.Property<int>("BookId");

                    b.Property<float?>("Index");

                    b.HasKey("SeriesId", "BookId");

                    b.HasIndex("BookId");

                    b.HasIndex("SeriesId");

                    b.ToTable("SeriesBook");
                });

            modelBuilder.Entity("DataLayer.Book", b =>
                {
                    b.HasOne("DataLayer.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.OwnsMany("DataLayer.Supplement", "Supplements", b1 =>
                        {
                            b1.Property<int>("SupplementId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<int>("BookId");

                            b1.Property<string>("Url");

                            b1.HasKey("SupplementId");

                            b1.HasIndex("BookId");

                            b1.ToTable("Supplement");

                            b1.HasOne("DataLayer.Book", "Book")
                                .WithMany("Supplements")
                                .HasForeignKey("BookId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("DataLayer.UserDefinedItem", "UserDefinedItem", b1 =>
                        {
                            b1.Property<int>("BookId");

                            b1.Property<string>("Tags");

                            b1.HasKey("BookId");

                            b1.ToTable("UserDefinedItem");

                            b1.HasOne("DataLayer.Book", "Book")
                                .WithOne("UserDefinedItem")
                                .HasForeignKey("DataLayer.UserDefinedItem", "BookId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsOne("DataLayer.Rating", "Rating", b2 =>
                                {
                                    b2.Property<int>("UserDefinedItemBookId");

                                    b2.Property<float>("OverallRating");

                                    b2.Property<float>("PerformanceRating");

                                    b2.Property<float>("StoryRating");

                                    b2.HasKey("UserDefinedItemBookId");

                                    b2.ToTable("UserDefinedItem");

                                    b2.HasOne("DataLayer.UserDefinedItem")
                                        .WithOne("Rating")
                                        .HasForeignKey("DataLayer.Rating", "UserDefinedItemBookId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });
                        });

                    b.OwnsOne("DataLayer.Rating", "Rating", b1 =>
                        {
                            b1.Property<int>("BookId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<float>("OverallRating");

                            b1.Property<float>("PerformanceRating");

                            b1.Property<float>("StoryRating");

                            b1.HasKey("BookId");

                            b1.ToTable("Books");

                            b1.HasOne("DataLayer.Book")
                                .WithOne("Rating")
                                .HasForeignKey("DataLayer.Rating", "BookId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("DataLayer.BookContributor", b =>
                {
                    b.HasOne("DataLayer.Book", "Book")
                        .WithMany("ContributorsLink")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataLayer.Contributor", "Contributor")
                        .WithMany("BooksLink")
                        .HasForeignKey("ContributorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.Category", b =>
                {
                    b.HasOne("DataLayer.Category", "ParentCategory")
                        .WithMany()
                        .HasForeignKey("ParentCategoryCategoryId");
                });

            modelBuilder.Entity("DataLayer.LibraryBook", b =>
                {
                    b.HasOne("DataLayer.Book", "Book")
                        .WithOne()
                        .HasForeignKey("DataLayer.LibraryBook", "BookId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.SeriesBook", b =>
                {
                    b.HasOne("DataLayer.Book", "Book")
                        .WithMany("SeriesLink")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataLayer.Series", "Series")
                        .WithMany("BooksLink")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
