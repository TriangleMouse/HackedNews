using System.Linq;
using HackedNews.Data.Models.NewsModel;

namespace HackedNews.ViewModels
{
    public class NewsViewModels
    {
        public IQueryable<News> News { get; set; }
        public string Text { get; set; } //search
    }
}