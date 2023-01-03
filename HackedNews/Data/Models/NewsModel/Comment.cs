using System;
using System.ComponentModel.DataAnnotations;

namespace HackedNews.Data.Models.NewsModel
{
    public class Comment
    {
        public int Id { get; set; }
         
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public string Date { get; set; } = DateTime.Now.ToString();

        public int UserId { get; set; }

        //todo переделать на экземпляр объекта пользователь(оставляю пока что для упрощения реализации)
        public string UserName { get; set; }

        public Comment ResponseToComment { get; set; } 

        public int NewsId { get; set; }
        public virtual News News { get; set; }
    }
}
