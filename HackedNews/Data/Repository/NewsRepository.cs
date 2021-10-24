using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            this.appDBContext = appDBContent;
        }

        public IEnumerable<News> News => appDBContext.News.Include(c=>c.Category).Include(d=>d.ListNewsDatas);


        public void SaveNews(News news)
        {
            if (news.Id == 0)
            {
                appDBContext.News.Add(news);
            }
            else
            {
                News dbEntry = appDBContext.News.Include(c=>c.ListNewsDatas).FirstOrDefault(p => p.Id == news.Id);
                if (dbEntry != null)
                {
                    dbEntry.Title = news.Title;
                    dbEntry.ShorttextCard = news.ShorttextCard;
                    dbEntry.NewsIntroduction = news.NewsIntroduction;
                    dbEntry.Img_Link = news.Img_Link;
                    dbEntry.Switch_Load_Img = news.Switch_Load_Img;
                    dbEntry.Img_Load = news.Img_Load;
                    dbEntry.File = null;
                    dbEntry.AuthorNews = news.AuthorNews;
                    
                    var categoryId = appDBContext.Category.FirstOrDefault(p => p.Id == news.CategoryID);
                    if (categoryId!=null)
                    {
                        dbEntry.CategoryID = categoryId.Id;
                    }
                    if (news.ListNewsDatas.Count()>0)
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
                System.Diagnostics.Debug.WriteLine("Error Save Data");
            }
        }

        public News DeleteNews(int newsID)
        {
            
            News dbEntry = appDBContext.News.FirstOrDefault(p => p.Id == newsID);
            if (dbEntry != null)
            {
                try
                {
                    appDBContext.News.Remove(dbEntry);
                    appDBContext.SaveChanges();
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("Error Deleted Data");
                }
            }
            return dbEntry;
        }

    }
}
