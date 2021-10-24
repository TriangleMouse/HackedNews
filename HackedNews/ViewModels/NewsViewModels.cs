using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 using HackedNews.Data.Models.NewsModel;
namespace HackedNews.ViewModels
{
    public class NewsViewModels
    {
       public IQueryable<News> News { get; set; }
        public string Text { get; set; }//search
    }
}
