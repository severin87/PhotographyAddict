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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);

           // builder.Entity<Conversation>()
           // .HasOne(p => p.SenderPhotographyAddictedUser)
           // .WithMany(b => b.SenderConversation)
           // .OnDelete(DeleteBehavior.ClientSetNull);

           // builder.Entity<Conversation>()
           //.HasOne(p => p.RecepientPhotographyAddictedUser)
           //.WithMany(b => b.RecepientConversation)
           //.OnDelete(DeleteBehavior.ClientSetNull);

            //builder.Entity<Match>()
            //        .HasRequired(m => m.HomeTeam)
            //        .WithMany(t => t.HomeMatches)
            //        .HasForeignKey(m => m.HomeTeamId)
            //        .WillCascadeOnDelete(false);

            //builder.Entity<Match>()
            //            .HasRequired(m => m.GuestTeam)
            //            .WithMany(t => t.AwayMatches)
            //            .HasForeignKey(m => m.GuestTeamId)
            //            .WillCascadeOnDelete(false);

            // builder.Entity<Conversation>()
            //.HasOne(p => p.SenderPhotographyAddictedUser)
            //.WithMany(b => b.RecepientConversation)
            //.OnDelete(DeleteBehavior.Cascade);

            // builder.Entity<Message>()
            //.HasOne(p => p.Conversation)
            //.WithMany(b => b.Messages)
            //.OnDelete(DeleteBehavior.Cascade);

            // builder.Entity<Message>()
            //.HasOne(p => p.PhotographyAddictedUser)
            //.WithMany(b => b.Messages)
            //.OnDelete(DeleteBehavior.Cascade);

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
