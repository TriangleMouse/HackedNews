using HackedNews.Data.Models.NewsModel;
using Microsoft.EntityFrameworkCore;

namespace HackedNews.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<News> News { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}