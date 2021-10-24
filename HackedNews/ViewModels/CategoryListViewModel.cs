using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackedNews.Data.Models.NewsModel;

namespace HackedNews.ViewModels
{
    public class CategoryListViewModel
    {
        public IEnumerable<News> LastNews { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
