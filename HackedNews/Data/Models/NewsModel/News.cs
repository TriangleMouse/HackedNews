using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace HackedNews.Data.Models.NewsModel
{
    public class News
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "TitleRequired")]
        [StringLength(80, ErrorMessage = "TitleLength", MinimumLength = 10)]
        public string Title { get; set; } //главный заголовок новости

        [Required(ErrorMessage = "ShorttextCardRequired")]
        [StringLength(120, ErrorMessage = "ShorttextCardLength", MinimumLength = 20)]
        public string ShorttextCard { get; set; } //текст карточки

        [StringLength(3000, ErrorMessage = "NewsIntroductionLength", MinimumLength = 0)]
        public string NewsIntroduction { get; set; } //Вступление новости

        public string ImgLink { get; set; } // Превью новости(!Обязательно)

        public bool SwitchLoadImg { get; set; } //если тру, то ставим форму для дагрузки изображения. если фолсе,то ссылка

        public byte[] ImgLoad { get; set; }
        public List<NewsData> ListNewsDatas { get; set; }
        public string Date { get; set; } = DateTime.Now.ToString();

        [Required(ErrorMessage = "AuthorNewsRequired")]
        [StringLength(50, ErrorMessage = "AuthorNewsLength", MinimumLength = 3)]
        public string AuthorNews { get; set; }
        
        [NotMapped]
        public IFormFile File { get; set; }

        public List<Comment> CommentList { get; set; }

        public int CategoryId { set; get; }
        public virtual Category Category { set; get; }
    }
}