using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using HackedNews.Data.Interfaces;
using HackedNews.Data.Models.NewsModel;
using Microsoft.EntityFrameworkCore;

namespace HackedNews.Data.Repository
{
    public class NewsRepository : IAllNews
    {
        private readonly AppDBContext appDBContext;

        public NewsRepository(AppDBContext appDBContent)
        {
            appDBContext = appDBContent;
        }

        public IEnumerable<News> News => appDBContext.News
            .Include(news => news.Category)
            .Include(news => news.CommentList)
            .Include(news => news.ListNewsDatas);


        public void SaveNews(News news)
        {
            if (news.Id == 0)
            {
                appDBContext.News.Add(news);
            }
            else
            {
                var dbEntry = appDBContext.News.Include(c => c.ListNewsDatas).FirstOrDefault(p => p.Id == news.Id);
                if (dbEntry != null)
                {
                    dbEntry.Title = news.Title;
                    dbEntry.ShorttextCard = news.ShorttextCard;
                    dbEntry.NewsIntroduction = news.NewsIntroduction;
                    dbEntry.ImgLink = news.ImgLink;
                    dbEntry.SwitchLoadImg = news.SwitchLoadImg;
                    dbEntry.ImgLoad = news.ImgLoad;
                    dbEntry.File = null;
                    dbEntry.AuthorNews = news.AuthorNews;

                    var categoryId = appDBContext.Category.FirstOrDefault(p => p.Id == news.CategoryId);
                    if (categoryId != null) dbEntry.CategoryId = categoryId.Id;
                    if (news.ListNewsDatas.Count() > 0)
                    {
                        //очищаем чтобы не было дубликатов
                        dbEntry.ListNewsDatas.Clear();
                        //Сохраняем новый список
                        dbEntry.ListNewsDatas.AddRange(news.ListNewsDatas);
                    }
                }
            }

            try
            {
                appDBContext.SaveChanges();
            }
            catch
            {
                Debug.WriteLine("Error Save Data");
            }
        }

        public News DeleteNews(int newsID)
        {
            var dbEntry = appDBContext.News.FirstOrDefault(p => p.Id == newsID);
            if (dbEntry != null)
                try
                {
                    appDBContext.News.Remove(dbEntry);
                    appDBContext.SaveChanges();
                }
                catch
                {
                    Debug.WriteLine("Error Deleted Data");
                }

            return dbEntry;
        }
    }
}