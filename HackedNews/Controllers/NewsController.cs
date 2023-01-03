﻿using System.Linq;
using HackedNews.Data.Interfaces;
using HackedNews.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HackedNews.Controllers
{
    public class NewsController : Controller
    {
        private readonly IAllNews allNews;
        public int PageSize = 10; //для разбиения на страницы


        public NewsController(IAllNews _allNews)
        {
            allNews = _allNews;
        }


        public ActionResult List(int? categoryId, int NewsPage = 1)
        {
            return View(new NewsListViewModel
            {
                News = allNews.News.Where(p => categoryId == null || p.CategoryId == categoryId)
                    .OrderBy(p => p.Id)
                    .Skip((NewsPage - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = NewsPage,
                    ItemsPerPage = PageSize,
                    TotalItems = categoryId == null
                        ? allNews.News.Count()
                        : allNews.News.Where(p => p.CategoryId == categoryId).Count()
                },
                CurrentCategory = categoryId
            });
        }

        public ActionResult InfNews(int? Newsid)
        {
            return View(
                new NewsListViewModel
                {
                    News = allNews.News.Where(p => p.Id == Newsid)
                }
            );
        }
    }
}