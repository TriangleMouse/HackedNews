using System.Collections.Generic;
using HackedNews.Data.Models.NewsModel;

namespace HackedNews.Data.Interfaces
{
    public interface INewsCategory
    {
        IEnumerable<Category> AllCategories { get; }
    }
}