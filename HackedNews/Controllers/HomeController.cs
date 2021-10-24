using HackedNews.Data.Interfaces;
using HackedNews.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HackedNews.Data.Models.NewsModel;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

namespace HackedNews.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private INewsCategory repositroryCategory;
        private IAllNews repositoryNews;
        private int CountLastNews=3;//количество последних новостей на главной странице

        public HomeController(ILogger<HomeController> logger,INewsCategory repoCategory,IAllNews repoNews)
        {
            _logger = logger;
            this.repositroryCategory = repoCategory;
            this.repositoryNews = repoNews;
        }

        public IActionResult Index()=> View(
            new CategoryListViewModel { 
                Categories= repositroryCategory.AllCategories,
                LastNews=repositoryNews.News.TakeLast(CountLastNews)
            }
            );

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
