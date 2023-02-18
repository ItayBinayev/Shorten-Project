using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShortenProject.Models;

namespace ShortenProject.Database
{
    public class UrlDbcontext : IdentityDbContext
    {
        public UrlDbcontext(DbContextOptions<UrlDbcontext> option) : base(option) 
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<UrlDbcontext, DataContextConfiguration>());

        }
        public virtual DbSet<URLModel> URLs { get; set; }
    }
}
