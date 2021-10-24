using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HackedNews.Data.Models.NewsModel
{
    public class News
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "TitleRequired")]
        [StringLength(80, ErrorMessage = "TitleLength", MinimumLength = 10)]
        public string Title { get; set; }//главный заголовок новости
        [Required(ErrorMessage = "ShorttextCardRequired")]
        [StringLength(120, ErrorMessage = "ShorttextCardLength", MinimumLength = 20)]
        public string ShorttextCard { get; set; }//текст карточки
        [StringLength(3000, ErrorMessage = "NewsIntroductionLength", MinimumLength = 0)]
        public string NewsIntroduction { get; set; }//Вступление новости
        public string Img_Link { get; set; }// Превью новости(!Обязательно)
        public bool Switch_Load_Img { get; set; } //если тру, то ставим форму для дагрузки изображения. если фолсе,то ссылка
        public byte[] Img_Load { get; set; }
        public List<NewsData> ListNewsDatas { get; set; }
        public string Date { get; set; } = DateTime.Now.ToString();
        [Required(ErrorMessage = "AuthorNewsRequired")]
        [StringLength(50, ErrorMessage = "AuthorNewsLength", MinimumLength = 3)]
        public string AuthorNews { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public IFormFile File { get; set; }



        public int CategoryID { set; get; }
        public virtual Category Category {set; get;  }

    }
}
