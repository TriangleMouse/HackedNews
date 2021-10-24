using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackedNews.ViewModels;
using HackedNews.Data.Repository;
using HackedNews.Data.Interfaces;
using HackedNews.Data.Models.NewsModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using Korzh.EasyQuery.Linq;

namespace HackedNews.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IAllNews allNews;
        private INewsCategory category;
        private SelectList categor;
        private readonly IStringLocalizer<AdminController> _localizer;

        public AdminController(IAllNews news,INewsCategory newsCategory, IStringLocalizer<AdminController> localizer)
        {
            this.allNews = news;
            this.category = newsCategory;
            this._localizer = localizer;
            categor = new SelectList(category.AllCategories, "Id", "Name");
        }


        public ActionResult Index() => View(
                new NewsViewModels
                {
                    News=allNews.News.AsQueryable()
                }
            );

        [HttpPost]
        public ActionResult Index(NewsViewModels model)
        {
            if (!string.IsNullOrEmpty(model.Text))
            {
                model.News = allNews.News.AsQueryable().FullTextSearchQuery(model.Text);
            }
            else
            {
                model.News = allNews.News.AsQueryable();
            }
            return View(model);
        }

        public ActionResult Edit(int newsId) {
            ViewBag.Categories = categor;
            return View(allNews.News.FirstOrDefault(p => p.Id == newsId));
        }


        [HttpPost]
        public ActionResult SwitchImgLoad(News news)
        {
            ViewBag.Categories = categor;
            return View("Edit", news);
        }


        //Создаем новую подтему в форме Edit
        [HttpPost]
        public ActionResult AddSubTopics(News news)
        {
             if (ModelState.IsValid)
             {
                news.ListNewsDatas.Add(new NewsData { Subtitle = "", Img_Link = "", Txt = "",Img_Load=null,Switch_Load_Img=false});
             }
            ViewBag.Categories = categor;
            return View("Edit", news);
        }

        //Удаляем подтему в форме Edit
        [HttpPost]
        public ActionResult DelSubtopics(News news)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Categories = categor;
                news.ListNewsDatas.Remove(news.ListNewsDatas.Last());
            }
            ViewBag.Categories = categor;
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
                TempData["err_message"] =_localizer["ErrReaderImg"]+img.FileName;
            }
            
            return imageData;   
        }

        [HttpPost]
        public IActionResult Edit(News news)
        {
            bool err = false;
            bool not_get_img_err = false;// проверка только для превью на главной страницы
            link:
            //Смог ли процесс привязки модели проверить достоверность отправленных пользователем данных
            if (ModelState.IsValid && !not_get_img_err && !err)
            {
                if (news.File != null)
                {
                    news.Img_Load = GetMassBinary(news.File);
                    if (news.Img_Load != null)
                    {
                        news.Img_Link = null;
                        news.File = null;
                    }
                    else
                    {
                        err = true;
                        goto link;
                    }
                }
                else if(news.File == null && news.Img_Link==null) {
                    not_get_img_err = true;
                    TempData["err_message"] = _localizer["NotGetPreview"] ;
                    goto link;
                }

                if (news.ListNewsDatas.Where(p=>p.File!=null).Count()>0)
                {
                    foreach (var item in news.ListNewsDatas)
                    {

                        if (item.File != null)
                        {
                            item.Img_Load = GetMassBinary(item.File);
                            if (item.Img_Load != null)
                            {
                                item.Img_Link = null;
                                item.File = null;
                            }
                            else
                            {
                                err = true;
                                goto link;
                            }
                        }
                    }
                }

                allNews.SaveNews(news);
                TempData["message"] = news.Title+" "+_localizer["SaveNews"];
                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.Categories = categor;
                //Что-то не так со значениями данных
                return View(news);
            }
        }

        public ActionResult Create()
        {
            ViewBag.Categories = categor;
            return View("Edit", new News());
        }


        [HttpPost]
        public IActionResult Delete(int newsId)
        {
            News deletedNews = allNews.DeleteNews(newsId);
            if (deletedNews != null)
            {
                TempData["message"] = deletedNews.Title + " " + _localizer["DeletedNews"];
            }
            return RedirectToAction("Index");
        }

    }
}
