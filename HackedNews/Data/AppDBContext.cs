using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackedNews.Data.Models.NewsModel;
namespace HackedNews.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options){}

        public DbSet<News> News { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}
