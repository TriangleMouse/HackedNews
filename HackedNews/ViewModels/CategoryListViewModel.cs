using System.Collections.Generic;
using HackedNews.Data.Models.NewsModel;

namespace HackedNews.ViewModels
{
    public class CategoryListViewModel
    {
        public IEnumerable<News> LastNews { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}