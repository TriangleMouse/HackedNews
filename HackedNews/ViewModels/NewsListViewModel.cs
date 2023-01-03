using System.Collections.Generic;
using HackedNews.Data.Models.NewsModel;

namespace HackedNews.ViewModels
{
    public class NewsListViewModel
    {
        public IEnumerable<News> News { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public int? CurrentCategory { get; set; }
    }
}