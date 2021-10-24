using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackedNews.Data.Models.NewsModel;

namespace HackedNews.Data.Interfaces
{
    public interface INewsCategory
    {
        IEnumerable<Category> AllCategories { get; }
    }
}
