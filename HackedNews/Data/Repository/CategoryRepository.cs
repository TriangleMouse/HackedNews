﻿using System.Collections.Generic;
using HackedNews.Data.Interfaces;
using HackedNews.Data.Models.NewsModel;

namespace HackedNews.Data.Repository
{
    public class CategoryRepository : INewsCategory
    {
        private readonly AppDBContext appDBContent;

        public CategoryRepository(AppDBContext appDBContent)
        {
            this.appDBContent = appDBContent;
        }

        public IEnumerable<Category> AllCategories => appDBContent.Category;
    }
}