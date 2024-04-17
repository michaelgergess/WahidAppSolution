
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<ReportArticle> ReportArticle { get; set; }
        public DbSet<WorldChat> WorldChat { get; set; }



        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
 
    }
}
