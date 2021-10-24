using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackedNews.ViewModels;
using HackedNews.Data.Repository;
using HackedNews.Data.Interfaces;
namespace HackedNews.Controllers
{
    public class NewsController : Controller
    {
        public int PageSize = 10;//для разбиения на страницы

        private readonly IAllNews allNews;


        public NewsController(IAllNews _allNews)
        {
            this.allNews = _allNews;
        }


        public ActionResult List(int? categoryId, int NewsPage = 1) =>
            View(new NewsListViewModel
            {
                News = allNews.News.Where(p => categoryId == null || p.CategoryID == categoryId)
                .OrderBy(p => p.Id)
                .Skip((NewsPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = NewsPage,
                    ItemsPerPage = PageSize,
                    TotalItems = categoryId == null ? allNews.News.Count() : allNews.News.Where(p => p.CategoryID == categoryId).Count()
                },
                CurrentCategory = categoryId
            });

        public ActionResult InfNews(int? Newsid) =>
            View(
                new NewsListViewModel
                {
                    News = allNews.News.Where(p => p.Id == Newsid)
                }
                );

    }
}
