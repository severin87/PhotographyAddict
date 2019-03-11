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
        public PhotographyAddictedContext(DbContextOptions<PhotographyAddictedContext> options)
            : base(options)
        {
        }

        public DbSet<PhotographyAddictedUser> PhotographyAddictedUsers { get; set;}
        public DbSet<Theme> Themes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
