using System;
using System.Diagnostics;
using System.Linq;
using HackedNews.Data.Interfaces;
using HackedNews.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HackedNews.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly int CountLastNews = 3; //количество последних новостей на главной странице
        private readonly IAllNews repositoryNews;
        private readonly INewsCategory repositroryCategory;

        public HomeController(ILogger<HomeController> logger, INewsCategory repoCategory, IAllNews repoNews)
        {
            _logger = logger;
            repositroryCategory = repoCategory;
            repositoryNews = repoNews;
        }

        public IActionResult Index()
        {
            return View(
                new CategoryListViewModel
                {
                    Categories = repositroryCategory.AllCategories,
                    LastNews = repositoryNews.News.TakeLast(CountLastNews)
                }
            );
        }

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