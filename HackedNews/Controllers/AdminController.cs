using System.IO;
using System.Linq;
using HackedNews.Data.Interfaces;
using HackedNews.Data.Models.NewsModel;
using HackedNews.ViewModels;
using Korzh.EasyQuery.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;

namespace HackedNews.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IStringLocalizer<AdminController> _localizer;
        private readonly IAllNews _allNews;
        private readonly SelectList _category;

        public AdminController(IAllNews news, INewsCategory newsCategory, IStringLocalizer<AdminController> localizer)
        {
            _allNews = news;
            _localizer = localizer;
            _category = new SelectList(newsCategory.AllCategories, "Id", "Name");
        }


        public ActionResult Index()
        {
            return View(
                new NewsViewModels
                {
                    News = _allNews.News.AsQueryable()
                }
            );
        }

        [HttpPost]
        public ActionResult Index(NewsViewModels model)
        {
            if (!string.IsNullOrEmpty(model.Text))
                model.News = _allNews.News.AsQueryable().FullTextSearchQuery(model.Text);
            else
                model.News = _allNews.News.AsQueryable();
            return View(model);
        }

        public ActionResult Edit(int newsId)
        {
            ViewBag.Categories = _category;
            return View(_allNews.News.FirstOrDefault(p => p.Id == newsId));
        }


        [HttpPost]
        public ActionResult SwitchImgLoad(News news)
        {
            ViewBag.Categories = _category;
            return View("Edit", news);
        }


        //Создаем новую подтему в форме Edit
        [HttpPost]
        public ActionResult AddSubTopics(News news)
        {
            if (ModelState.IsValid)
                news.ListNewsDatas.Add(new NewsData
                    { Subtitle = "", ImgLink = "", Txt = "", ImgLoad = null, SwitchLoadImg = false });
            ViewBag.Categories = _category;
            return View("Edit", news);
        }

        //Удаляем подтему в форме Edit
        [HttpPost]
        public ActionResult DelSubtopics(News news)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Categories = _category;
                news.ListNewsDatas.Remove(news.ListNewsDatas.Last());
            }

            ViewBag.Categories = _category;
            return View("Edit", news);
        }


        private byte[] GetMassBinary(IFormFile img)
        {
            byte[] imageData = null;
            try
            {
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(img.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)img.Length);
                }
            }
            catch
            {
                imageData = null;
                TempData["err_message"] = _localizer["ErrReaderImg"] + img.FileName;
            }

            return imageData;
        }

        [HttpPost]
        public IActionResult Edit(News news)
        {
            // todo отрефакторить булщитный код написанный много лет назад на скорости из-за дедлайна(goto осуждаю)
            var err = false;
            var not_get_img_err = false; // проверка только для превью на главной страницы
            link:
            //Смог ли процесс привязки модели проверить достоверность отправленных пользователем данных
            if (ModelState.IsValid && !not_get_img_err && !err)
            {
                if (news.File != null)
                {
                    news.ImgLoad = GetMassBinary(news.File);
                    if (news.ImgLoad != null)
                    {
                        news.ImgLink = null;
                        news.File = null;
                    }
                    else
                    {
                        err = true;
                        goto link;
                    }
                }
                else if (news.File == null && news.ImgLink == null)
                {
                    not_get_img_err = true;
                    TempData["err_message"] = _localizer["NotGetPreview"];
                    goto link;
                }

                if (news.ListNewsDatas.Where(p => p.File != null).Count() > 0)
                    foreach (var item in news.ListNewsDatas)
                        if (item.File != null)
                        {
                            item.ImgLoad = GetMassBinary(item.File);
                            if (item.ImgLoad != null)
                            {
                                item.ImgLink = null;
                                item.File = null;
                            }
                            else
                            {
                                err = true;
                                goto link;
                            }
                        }

                _allNews.SaveNews(news);
                TempData["message"] = news.Title + " " + _localizer["SaveNews"];
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _category;
            //Что-то не так со значениями данных
            return View(news);
        }

        public ActionResult Create()
        {
            ViewBag.Categories = _category;
            return View("Edit", new News());
        }


        [HttpPost]
        public IActionResult Delete(int newsId)
        {
            var deletedNews = _allNews.DeleteNews(newsId);
            if (deletedNews != null) TempData["message"] = deletedNews.Title + " " + _localizer["DeletedNews"];
            return RedirectToAction("Index");
        }
    }
}