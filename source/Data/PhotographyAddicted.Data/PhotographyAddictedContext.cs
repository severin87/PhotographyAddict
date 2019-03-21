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
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ImageComment> ImageComments { get; set; }
        public DbSet<ThemeComment> ThemeComments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);

            builder.Entity<Image>()
            .HasOne(p => p.PhotographyAddictedUser)
            .WithMany(b => b.Images)
            .OnDelete(DeleteBehavior.Cascade);


            //builder.Entity<ImageComment>()
            //.HasOne(p => p.PhotographyAddictedUser)
            //.WithMany(b => b.ImageComments)
            //.OnDelete(DeleteBehavior.SetNull);

            builder.Entity<ImageComment>()
            .HasOne(p => p.Image)
            .WithMany(b => b.ImageCommnets)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Theme>()
            .HasOne(p => p.PhotographyAddictedUser)
            .WithMany(b => b.Themes)
            .OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<ThemeComment>()
            //.HasOne(p => p.PhotographyAddictedUser)
            //.WithMany(b => b.ThemeComments)
            //.OnDelete(DeleteBehavior.SetNull);

            builder.Entity<ThemeComment>()
            .HasOne(p => p.Theme)
            .WithMany(b => b.ThemeComments)
            .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
