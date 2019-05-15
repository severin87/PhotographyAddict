using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotographyAddicted.Data.Models;
using PhotographyAddicted.Web.Areas.Identity.Data;

namespace PhotographyAddicted.Web.Models
{
    public class PhotographyAddictedContext : IdentityDbContext<PhotographyAddictedUser>
    {

        public PhotographyAddictedContext()
        {}

        public PhotographyAddictedContext(DbContextOptions<PhotographyAddictedContext> options)
            : base(options)
        {}

        public DbSet<PhotographyAddictedUser> PhotographyAddictedUsers { get; set;}
        public DbSet<New> News { get; set; }
        public DbSet<NewComment> NewComments { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ImageComment> ImageComments { get; set; }
        public DbSet<ThemeComment> ThemeComments { get; set; }
        public DbSet<PhotoStory> PhotoStories { get; set; }
        public DbSet<PhotoStoryComment> PhotoStoryComments { get; set; }
        public DbSet<PhotoStoryFragment> PhotoStoryFragments { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<FavouriteImage> FavouriteImages { get; set; }
        public DbSet<VotedUser> VotedUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);

            builder.Entity<FavouriteImage>()
           .HasKey(bc => new { bc.FavouriteId, bc.ImageId });

            builder.Entity<VotedUser>()
           .HasOne(p => p.Image)
           .WithMany(b => b.VotedUsers)
           .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<FavouriteImage>()
           .HasOne(bc => bc.Image)
           .WithMany(c => c.FavouriteImages)
           .HasForeignKey(bc => bc.ImageId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<FavouriteImage>()
           .HasOne(bc => bc.Favorite)
           .WithMany(b => b.FavouriteImages)
           .HasForeignKey(bc => bc.FavouriteId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PhotographyAddictedUser>()
           .HasOne(x => x.Favourite)
           .WithOne(x => x.PhotographyAddictedUser).HasForeignKey<Favourite>(x => x.PhotographyAddictedUserId)
           .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PhotoStory>()
           .HasOne(p => p.PhotographyAddictedUser)
           .WithMany(b => b.PhotoStories)
           .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PhotoStoryFragment>()
           .HasOne(p => p.PhotoStory)
           .WithMany(b => b.PhotoStoryFragments)
           .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PhotoStoryComment>()
           .HasOne(p => p.PhotoStory)
           .WithMany(b => b.PhotoStoryComments)
           .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<New>()
           .HasOne(p => p.PhotographyAddictedUser)
           .WithMany(b => b.News)
           .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<NewComment>()
           .HasOne(p => p.New)
           .WithMany(b => b.NewComments)
           .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<Image>()
            .HasOne(p => p.PhotographyAddictedUser)
            .WithMany(b => b.Images)
            .OnDelete(DeleteBehavior.Cascade);           

            builder.Entity<ImageComment>()
            .HasOne(p => p.Image)
            .WithMany(b => b.ImageComments)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Theme>()
            .HasOne(p => p.PhotographyAddictedUser)
            .WithMany(b => b.Themes)
            .OnDelete(DeleteBehavior.Cascade);            

            builder.Entity<ThemeComment>()
            .HasOne(p => p.Theme)
            .WithMany(b => b.ThemeComments)
            .OnDelete(DeleteBehavior.Cascade);

            // builder.Entity<ImageComment>()
            //.HasOne(p => p.PhotographyAddictedUser)
            //.WithMany(b => b.ImageComments)
            //.OnDelete(DeleteBehavior.SetNull);

            // builder.Entity<ThemeComment>()
            // .HasOne(p => p.PhotographyAddictedUser)
            // .WithMany(b => b.ThemeComments)
            // .OnDelete(DeleteBehavior.SetNull);

            // builder.Entity<NewComment>()
            //.HasOne(p => p.PhotographyAddictedUser)
            //.WithMany(b => b.NewComments)
            //.OnDelete(DeleteBehavior.SetNull);
        }

    }
}
