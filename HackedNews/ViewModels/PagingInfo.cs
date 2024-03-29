﻿using System;

namespace HackedNews.ViewModels
{
    public class PagingInfo
    {
        public int TotalItems { get; set; } // всего элементов
        public int ItemsPerPage { get; set; } // страница элементов
        public int CurrentPage { get; set; } // текущая страница
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    }
}