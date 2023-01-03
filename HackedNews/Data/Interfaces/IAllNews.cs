using System.Collections.Generic;
using HackedNews.Data.Models.NewsModel;

namespace HackedNews.Data.Interfaces
{
    public interface IAllNews
    {
        IEnumerable<News> News { get; }
        void SaveNews(News news);
        News DeleteNews(int newsID);
    }
}