﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhotographyAddicted.Web.Models;

namespace PhotographyAddicted.Data.Migrations
{
    [DbContext(typeof(PhotographyAddictedContext))]
    partial class PhotographyAddictedContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("PhotographyAddicted.Data.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<int>("ImageCategory");

                    b.Property<string>("PhotographyAddictedUserId");

                    b.Property<byte[]>("Picture");

                    b.Property<int>("Scores");

                    b.Property<string>("Title");

                    b.Property<DateTime>("UploadedDate");

                    b.HasKey("Id");

                    b.HasIndex("PhotographyAddictedUserId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("PhotographyAddicted.Data.Models.ImageComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<int?>("ImageId");

                    b.Property<string>("PhotographyAddictedUserId");

                    b.Property<string>("UserOpinion");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.HasIndex("PhotographyAddictedUserId");

                    b.ToTable("ImageComments");
                });

            modelBuilder.Entity("PhotographyAddicted.Data.Models.New", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ComentsCount");

                    b.Property<DateTime>("CreationDate");

                    b.Property<byte[]>("NewImage");

                    b.Property<string>("PhotographyAddictedUserId");

                    b.Property<string>("TextContent");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("PhotographyAddictedUserId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("PhotographyAddicted.Data.Models.NewComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<int?>("NewId");

                    b.Property<string>("PhotographyAddictedUserId");

                    b.Property<string>("UserOpinion");

                    b.HasKey("Id");

                    b.HasIndex("NewId");

                    b.HasIndex("PhotographyAddictedUserId");

                    b.ToTable("NewComments");
                });

            modelBuilder.Entity("PhotographyAddicted.Data.Models.PhotoStory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author");

                    b.Property<string>("Conclusion");

                    b.Property<string>("Introduction");

                    b.Property<string>("PhotographyAddictedUserId");

                    b.Property<bool>("Published");

                    b.Property<string>("Title");

                    b.Property<DateTime>("UploadedDate");

                    b.HasKey("Id");

                    b.HasIndex("PhotographyAddictedUserId");

                    b.ToTable("PhotoStories");
                });

            modelBuilder.Entity("PhotographyAddicted.Data.Models.PhotoStoryComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<int?>("PhotoStoryId");

                    b.Property<string>("PhotographyAddictedUserId");

                    b.Property<string>("UserOpinion");

                    b.HasKey("Id");

                    b.HasIndex("PhotoStoryId");

                    b.HasIndex("PhotographyAddictedUserId");

                    b.ToTable("PhotoStoryComments");
                });

            modelBuilder.Entity("PhotographyAddicted.Data.Models.PhotoStoryFragment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<int?>("PhotoStoryId");

                    b.Property<byte[]>("Picture");

                    b.Property<string>("Place");

                    b.HasKey("Id");

                    b.HasIndex("PhotoStoryId");

                    b.ToTable("PhotoStoryFragments");
                });

            modelBuilder.Entity("PhotographyAddicted.Data.Models.Theme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthorOpinion");

                    b.Property<int>("ComentsCount");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("PhotographyAddictedUserId");

                    b.Property<int>("ThemeCategory");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("PhotographyAddictedUserId");

                    b.ToTable("Themes");
                });

            modelBuilder.Entity("PhotographyAddicted.Data.Models.ThemeComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("PhotographyAddictedUserId");

                    b.Property<int?>("ThemeId");

                    b.Property<string>("UserOpinion");

                    b.HasKey("Id");

                    b.HasIndex("PhotographyAddictedUserId");

                    b.HasIndex("ThemeId");

                    b.ToTable("ThemeComments");
                });

            modelBuilder.Entity("PhotographyAddicted.Web.Areas.Identity.Data.PhotographyAddictedUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<int>("AverageScore");

                    b.Property<DateTime>("Blocked");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<int>("ImageCount");

                    b.Property<bool>("IsAgree");

                    b.Property<bool>("IsBanned");

                    b.Property<DateTime>("LastLogin");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<byte[]>("ProfilePicture");

                    b.Property<string>("Rang");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("SelfDescription");

                    b.Property<string>("Technique");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("PhotographyAddicted.Web.Areas.Identity.Data.PhotographyAddictedUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("PhotographyAddicted.Web.Areas.Identity.Data.PhotographyAddictedUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PhotographyAddicted.Web.Areas.Identity.Data.PhotographyAddictedUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("PhotographyAddicted.Web.Areas.Identity.Data.PhotographyAddictedUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PhotographyAddicted.Data.Models.Image", b =>
                {
                    b.HasOne("PhotographyAddicted.Web.Areas.Identity.Data.PhotographyAddictedUser", "PhotographyAddictedUser")
                        .WithMany("Images")
                        .HasForeignKey("PhotographyAddictedUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PhotographyAddicted.Data.Models.ImageComment", b =>
                {
                    b.HasOne("PhotographyAddicted.Data.Models.Image", "Image")
                        .WithMany("ImageComments")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PhotographyAddicted.Web.Areas.Identity.Data.PhotographyAddictedUser", "PhotographyAddictedUser")
                        .WithMany("ImageComments")
                        .HasForeignKey("PhotographyAddictedUserId");
                });

            modelBuilder.Entity("PhotographyAddicted.Data.Models.New", b =>
                {
                    b.HasOne("PhotographyAddicted.Web.Areas.Identity.Data.PhotographyAddictedUser", "PhotographyAddictedUser")
                        .WithMany("News")
                        .HasForeignKey("PhotographyAddictedUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PhotographyAddicted.Data.Models.NewComment", b =>
                {
                    b.HasOne("PhotographyAddicted.Data.Models.New", "New")
                        .WithMany("NewComments")
                        .HasForeignKey("NewId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PhotographyAddicted.Web.Areas.Identity.Data.PhotographyAddictedUser", "PhotographyAddictedUser")
                        .WithMany("NewComments")
                        .HasForeignKey("PhotographyAddictedUserId");
                });

            modelBuilder.Entity("PhotographyAddicted.Data.Models.PhotoStory", b =>
                {
                    b.HasOne("PhotographyAddicted.Web.Areas.Identity.Data.PhotographyAddictedUser", "PhotographyAddictedUser")
                        .WithMany("PhotoStorys")
                        .HasForeignKey("PhotographyAddictedUserId");
                });

            modelBuilder.Entity("PhotographyAddicted.Data.Models.PhotoStoryComment", b =>
                {
                    b.HasOne("PhotographyAddicted.Data.Models.PhotoStory", "PhotoStory")
                        .WithMany("PhotoStoryComments")
                        .HasForeignKey("PhotoStoryId");

                    b.HasOne("PhotographyAddicted.Web.Areas.Identity.Data.PhotographyAddictedUser", "PhotographyAddictedUser")
                        .WithMany("PhotoStoryComments")
                        .HasForeignKey("PhotographyAddictedUserId");
                });

            modelBuilder.Entity("PhotographyAddicted.Data.Models.PhotoStoryFragment", b =>
                {
                    b.HasOne("PhotographyAddicted.Data.Models.PhotoStory", "PhotoStory")
                        .WithMany("PhotoStoryFragments")
                        .HasForeignKey("PhotoStoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PhotographyAddicted.Data.Models.Theme", b =>
                {
                    b.HasOne("PhotographyAddicted.Web.Areas.Identity.Data.PhotographyAddictedUser", "PhotographyAddictedUser")
                        .WithMany("Themes")
                        .HasForeignKey("PhotographyAddictedUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PhotographyAddicted.Data.Models.ThemeComment", b =>
                {
                    b.HasOne("PhotographyAddicted.Web.Areas.Identity.Data.PhotographyAddictedUser", "PhotographyAddictedUser")
                        .WithMany("ThemeComments")
                        .HasForeignKey("PhotographyAddictedUserId");

                    b.HasOne("PhotographyAddicted.Data.Models.Theme", "Theme")
                        .WithMany("ThemeComments")
                        .HasForeignKey("ThemeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
