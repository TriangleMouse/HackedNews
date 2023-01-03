using System.Collections.Generic;

namespace HackedNews.Data.Models.NewsModel
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } //имя категории
        public string Desc { get; set; } // описание категории
        public List<News> ListNews { get; set; }
    }
}