using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
